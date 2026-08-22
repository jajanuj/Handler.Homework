# AR / Proc 架構筆記

> 給新 session 讀的架構速覽：這個專案的自動化流程(AutoRun)分成兩層——**Proc**(設備本體的狀態機)跟
> **AR**(什麼時候該叫設備動作的決策層)。要碰 `AutoRun`／`Scenario`／`AR_*`／`Proc_*` 相關程式碼之前，先讀這份。
> 內容是逐檔讀出來的架構，不是憑空整理的規格書——拿不準的地方請直接重讀對應檔案確認。
> **這是活文件**：每次寫新的 AR/Proc 遇到新的架構模式或踩到新的坑，都要回來補進這份檔案，不要只留在對話紀錄裡。
> 建立：2026-08-21(ASM Lane 組裝流程)。更新：2026-08-22(HS Discharge Magazine、Press Lane/Station、AOI Lane/Station、
> Lane→Lane 交握 WaitPreviousDoneLoad() 不該覆寫的 bug、CanUnload() 下游狀態清單漏掉 Unload_Done 導致死結的 bug、
> OK Lane／NG Feed·Lane·Discharge Magazine、Sort Arm 分流機制與 NG 出料模式)。

## 核心機制：clsThreadProc + iStepIndex

專案裡幾乎所有流程物件(`BaseArm`/`BaseLane`/`BaseMagazine` 的子類別、所有 `AR_*`、`ProcAutoRun`、`ProcInitial`)
都繼承 `clsThreadProc`，用一個 `protected override void Scenario()` 裡的 `switch (iStepIndex)` 當狀態機。
外部有東西會每個 scan cycle 呼叫這些物件的 `Scenario()`（驅動細節不在這份文件範圍內，要查另外去看 `clsThreadProc`）。

**編號慣例**（不是鐵律，但目前檔案幾乎都照這個模式）：
- `X0000`：某個階段的起始
- `X0100`, `X0200`...：階段內的子步驟
- `X998` / `X9998`：失敗終點
- `X999` / `X9999`：完成終點
- `-1`：閒置／已停止
- `default:` 通常是 `iStepIndex = -1; Stop(); bIsProcessing = false;`

**每次改這類檔案，一定要核對：每一處 `iStepIndex = N` 的 N，都要有對應的 `case N:` 真的存在。**
編譯器完全抓不到這類問題，只能人工比對兩份清單（見 `LESSONS.md` L3，`BaseArm.cs` 就抓到過一次真的斷點）。

## 兩層架構

### Proc 層——設備本體

路徑：`ArtEQ/2_Function(流程)/Proc/{Arm,Lane,Magazine,Station}/Proc_*.cs`
基底：`BaseArm`、`BaseLane`、`BaseMagazine`、`BasePressStation`、`BaseAoiStation`（都在 `ArtEQ/2_Function(流程)/BaseProc/`）

Proc 類別代表一個實體設備（一支手臂、一條流道、一個料盒、一個站別），自己的 `Scenario()` 是這個設備的動作細節
（馬達怎麼移動、真空怎麼開關、DI/DO 怎麼讀寫）。子類別只覆寫「這個設備專屬」的部分：

**目前有四種設備原型**，差在「一次動作處理幾格」：

| 原型 | 一次動作處理範圍 | 例子 |
|---|---|---|
| `BaseArm` | 單一格，兩段式（Pick 一格、Place 一格，靠 col/row 指定） | `Proc_ASM_Arm` |
| `BaseLane` | 整盤 Tray（Load 進來一整盤、Unload 出去一整盤） | `Proc_HS_Lane`、`Proc_ASM_Lane`、`Proc_Press_Lane`、`Proc_AOI_Lane` |
| `BasePressStation` | **整盤裡「所有有料的格子」一次處理完**，不逐格觸發 | `Proc_Press_Station` |
| `BaseAoiStation` | **單一格，一段式**（鏡頭移到 col/row 檢測那一格，沒有 Pick/Place 兩段） | `Proc_AOI_Station` |

> ⚠️ **`BasePressStation` 跟 `BaseAoiStation` 長得像（都是「站別」，都對應一條 Lane），但一次處理的範圍不一樣，
> 不要看名字都是「XxxStation」就假設可以照抄同一套。加新站別前，先讀該站別 `Proc_*.SetTrayWork()` 的實作，
> 確認它是迴圈整盤（Press 那種）還是只動 `m_workRow/m_workColumn` 那一格（AOI 那種），這會直接決定 AR 要怎麼寫：**
> - **整盤一次處理完**（照 `BasePressStation`／`AR_Press_Station` 抄）：AR 只要「有沒有還沒處理的格子」判斷要不要觸發，
>   觸發一次 `RunXxx()` 就對整盤生效，不用管 col/row。
> - **逐格處理**（照 `BaseAoiStation`／`AR_AOI_Station` 抄）：AR 要照 `AR_ASM_Arm` 的邏輯逐格找「有料但沒處理完成」
>   的格子，一格一格觸發 `RunXxx(col, row)`，每次只推進一格。

各原型子類別要覆寫的 hook：

| 基底 | 子類別要覆寫的 hook | 用途 |
|---|---|---|
| `BaseArm` | `BindHardwarePoint()` | 綁定這支手臂的軸／DI／DO |
| | `ReadyToPick()` / `ReadyToPlace()` | 判斷來源／目的流道是否就緒 |
| | `GetPickLane()` / `GetPlaceLane()` | 依 `PPStation` + `m_pickPlace` 動態決定這次要對哪條流道動作 |
| | `TransferToLane()`（abstract） | 放料完成後怎麼把帳過到目的流道 |
| `BaseLane` | `BindHardwarePoint()` | 綁定 Roller／Stopper 的 DI／DO |
| | `ReadyToLoad()` / `ReadyToUnloadToNext()` / `WaitPreviousDoneLoad()` / `WaitNextLoadDone()` | 跟上下游（Magazine 或 Lane）交握 |
| | `GetPreviousMagazineForBill()` / `GetPreviousLaneForBill()` / `GetNextLaneForBill()` / `GetNextMagazineForBill()` | 指定上下游是誰（上游可能是 Magazine 或 Lane，下游同理，視情況只覆寫需要的那組） |
| `BasePressStation` | `BindHardwarePoint()` | 綁定壓合氣缸的 DI／DO |
| | `PressLane`（abstract 屬性） | 指定這個站別對應哪條 Lane |
| | `SetTrayWork()`（abstract，**整盤迴圈**） | 迴圈整盤，把每個 `IsExist` 的格子標記處理完成（例如 `IsPressed=true`） |
| `BaseAoiStation` | `BindHardwarePoint()` | 綁定檢測相機/馬達的軸 |
| | `AOILane`（abstract 屬性） | 指定這個站別對應哪條 Lane |
| | `SetTrayWork()`（abstract，**只動 `m_workRow`/`m_workColumn` 那一格**） | 用 `RunInspect(col,row)` 傳進來的座標算 index，只標記那一格（`IsAoiInspected=true`、寫入 `AoiResult`） |

公開介面（AR 層會呼叫的）：
- `RunInitial()`（全部都有）
- Arm：`RunPick(PPStation, col, row)` / `RunPlace(PPStation, col, row)`
- Lane：`RunLoad()` / `RunUnload()`（無參數，物件自己知道上下游是誰）
- Magazine：`RunLoad(slotNo)` / `RunUnload(slotNo)`——**方向名稱容易搞混，見下方警告**
- Station（整盤型）：`RunPress()`（`BasePressStation` 專屬）
- Station（逐格型）：`RunInspect(col, row)`（`BaseAoiStation` 專屬）
- 狀態查詢：`IsProcOK()`（`!bIsProcessing && bIsReady`）、`m_enuAction`（每個基底自己定義的 enum，
  例如 `BaseArm.enuAction.Pick_Done`）

都是 singleton，`GetSingleton()` 拿實例。

> ⚠️ **`BaseMagazine.RunLoad(slot)` / `RunUnload(slot)` 的方向跟直覺可能相反**：
> `RunLoad` = 「推料**給下游**」（dispense out，料盒 → Lane）；`RunUnload` = 「**收料回**料盒」（Lane → 料盒）。
> 不是「Load＝裝料進料盒」。寫 Discharge 類（收料方向）的 Magazine AR 之前，先去
> `BaseMagazine.cs` 對應的 `enuAction` 定義（`Magazine_Load` 區塊 vs `Magazine_Unload` 區塊）的中文註解確認一次，
> 不要憑方法名字猜（`Proc_HS_Discharge_Magazine` 的 hook 也取了容易誤導的名字 `ReadyToLoad`/`TransferBillAfterLoading`，
> 但實際上是被 `Magazine_Unload`（30000 系列）流程呼叫的——hook 命名跟它服務的方向不一致，見檔案本身求證）。

### AR 層——什麼時候該動

路徑：`ArtEQ/2_Function(流程)/AutoRun/AR_*.cs`，命名空間 `ArtEQ._2_Function_流程_.AutoRun`

AR 類別本身也是一個 `clsThreadProc`，但它的 `Scenario()` 管的是「決策」，不是設備細節：
閒置 → 條件判斷（`CanLoad()`/`CanUnload()`/自訂條件）→ 呼叫對應 Proc 物件的 `Run*()` → 等 `IsProcOK()` 變
true → 檢查 `m_enuAction` 是 `_Done` 還是 `_Fail` → 回閒置。

範例骨架（`AR_HS_Lane`／`AR_ASM_Lane` 都長這樣）：
```
case 1              → 100000（前置）
case 100000          → 依 CanLoad()/CanUnload() 決定去 200000 或 300000（閒置判別）
case 200000/300000   → 再次確認條件成立 → 進下一步
case 200100/300100   → 呼叫 Proc.RunLoad()/RunUnload()
case 200200/300200   → 等 m_enuAction 變 Done/Fail，回 100000
default              → Stop
```

一個 AR 通常對應「一段搬運關係」，不是一個設備。例如 `AR_Mag_HS_Feed` 管的是「HS Feed Magazine → HS Lane」
這段推料關係，物件本身持有兩個 Proc 的參照（`Mag_HS_Feed()`／`NextLane()`）。

> ⚠️ **`CanLoad()`/`CanUnload()` 檢查下游狀態時，OR 清單一定要同時包含「下游正在等待」跟「下游剛做完一輪、
> 閒置中」兩種狀態，只列前者會死結。** 例如檢查下游 Lane 準備好收料，不能只列 `Load_Waiting`（下游正在主動
> 等待進料），還要列 `Unload_Done`（下游剛完成上一輪、目前閒置）——不然下游剛做完一輪、還沒開始等待下一輪時，
> 上游看不到任何一個清單裡的狀態，永遠不觸發；下游那邊又要等上游先觸發才會開始等待，兩邊互相卡住。
> 2026-08-22 `AR_ASM_Lane`／`AR_Press_Lane`／`AR_AOI_Lane` 三個的下游檢查原本都只列 `Load_Waiting`／
> `Initial_Done`，第一輪靠 `Initial_Done`（只出現一次）僥倖跑過，第二輪就三個一起卡（見 `LESSONS.md` L9）。
> `AR_Mag_HS_Feed`／`AR_Mag_IC_Feed` 檢查目標 Lane 時是對的範例（`Load_Done || Unload_Done || Initial_Done`），
> 新寫一個 AR 的下游檢查時，直接照這三個狀態的組合抄，不要只抄看起來像「在等待」的那一個。

Arm 型的 AR 現在有模板可抄了（`AR_ASM_Arm`）：跟 Load/Unload 型不同，Arm 是「Pick 一次 + Place 一次」，
狀態機多一組 Pick→Place 的序列，條件判斷也不是查 `m_enuAction`，而是逐格比對兩條流道的 `clsTrayInfo`
（細節見下一節）。

Station 型的 AR 有**兩種模板**，對應上面「整盤型」跟「逐格型」兩種站別，不要混用：
- **整盤型**（`AR_Press_Station`）：比 Arm 簡單，只有「觸發一次 `RunXxx()` → 等 `_Done`/`_Fail`」兩步，不用算格位；
  「還要不要再觸發」的判斷靠檢查整盤裡「有料但還沒處理完成」的格子還在不在
  （`AssyRecords.Any(v => v.IsExist && !v.IsPressed)` 這種寫法），不是額外記一個「有沒有觸發過」的旗標。
- **逐格型**（`AR_AOI_Station`）：形狀介於 Arm 跟整盤型 Station 之間——像 Arm 一樣要逐格找「有料但還沒處理完成」
  的格子（`FindNextInspectCell`，邏輯跟 `AR_ASM_Arm` 找格位的迴圈幾乎一樣），但沒有 Pick/Place 兩段，只有
  「觸發 `RunXxx(col, row)` → 等 `_Done`/`_Fail`」一段，找到一格處理一格，回閒置後下一輪 scan 才會再找下一格。

> ⚠️ **抄別的 AR 檔案當模板時，物件參照(哪個 Lane／Magazine)要逐一核對，不能只信任變數名稱抄對了。**
> `AR_HS_Lane.CanUnload()` 曾經真的把「檢查下游 Magazine」那段誤植成呼叫上游的 `Mag_HS_Feed()`（應該是
> `Mag_HS_Discharge()`），編譯完全不會報錯，只有邏輯是錯的——因為兩個都是合法的 `Proc_XxxMagazine` 物件，
> 型別對得上。加新 AR 時，尤其是複製既有檔案再改的情況，每一個 helper method（`XxxLane()`／`Mag_Xxx()`）
> 實際指向誰、跟這個 AR 真正的上下游關係對不對得起來，要重新過一遍，不要只看變數名稱順眼就跳過。

## 帳料資料結構：clsTrayInfo / TrayItemStatus / clsAssyRecord

每條 Lane、每個 Magazine 槽位都有一個 `clsTrayInfo m_Temp_Tray_Info`，代表一個 Row×Col 的格子盤（預設 2×3）：

- `bIsExist`：這條 Lane／這個槽位「有沒有帳」（粗粒度，Lane/Magazine 層級）
- `arrItemStatus[i]`（`TrayItemStatus` enum：`Pending/OK/NG/Empty/Assembly/Substrate/HeatSink/Pressed/AoiInspected`）：
  **每一格**目前裝的是什麼材料／處於什麼階段（細粒度，格子層級）
- `AssyRecords[i]`（`clsAssyRecord`：`IsExist/IsAssembled/IsPressed/IsAoiInspected/AoiResult/CurrentStation`）：
  每一格的製程紀錄，**沒有唯一序號**，identity 完全靠格子 index（`GetIndexFromRowCol`/`GetRowColFromIndex`）

Lane 從上游接帳時（`BaseLane.ReceiveTrayBillFromPrevious`），依 `Materials[i].MaterialType` 把每一格的
`arrItemStatus` 設成對應的 `HeatSink`/`Substrate`。Arm 撿料／放料（`TransferToArm`/`TransferToLane`）會把撿走
的格子設回 `Empty`、放上去的格子設成 `Assembly`。**這代表兩條 Lane 之間要做「格子對格子」的比對，直接比
`GetItemStatus(i)` 就知道某一格目前是什麼材料。**

`clsTrayInfo.Clear()` 只清 `bIsExist`/`sTrayID`/`iRowID`/`iColumnID`，**不會清 `arrItemStatus`／`AssyRecords`／
`Materials`**——重用一個 tray 物件前如果沒有明確覆寫每一格，舊資料會殘留（`BaseMagazine.cs` 的幾個測試建帳
方法就是活生生的例子，見 `LESSONS.md`）。

### `AssyRecords[i].IsExist` 要自己維護，不會自動跟著 `arrItemStatus` 走

這個欄位**不是**自動衍生出來的，2026-08-21 做 ASM 那次漏了維護它，`CanUnload()` 裡引用它的完成判斷全部變成
「永遠是 true」的死開關（原因：`Any(v => v.IsExist)` 在 `IsExist` 從來沒被設過 true 的情況下永遠是 false，
取反後永遠通過，見 `LESSONS.md` 相關條目）。2026-08-22 補上維護，改成在下面三個既有動作點各加一行：

| 動作 | 檔案／方法 | 要做的事 |
|---|---|---|
| Lane 從上游接到整盤 Tray | `BaseLane.ReceiveTrayBillFromPrevious()` | 跟著 `arrItemStatus` 同一個 `if/else` 分支，占用的格子設 `IsExist=true`，空的設 `false` |
| Arm 從 Lane 撿走一格 | `BaseArm.TransferToArm()` | 撿走那格 `AssyRecords[index].IsExist = false`（跟 `SetItemStatus(index, Empty)` 同一行旁邊加） |
| Arm 把料放到 Lane 一格 | 各 Arm 子類別自己的 `TransferToLane()`（如 `Proc_ASM_Arm.cs`） | 放上去那格明確設 `IsExist = true`（不要依賴 `AssyRecord.CopyTo(...)` 的巧合順序） |

**「這個站別的作業有沒有整盤做完」的判斷，統一用這個寫法**（`AR_ASM_Lane`／`AR_Press_Lane` 的 `CanUnload()` 都這樣寫）：

```csharp
!laneOrStation.m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist && !v.<這個站別的完成旗標>);
```

`<完成旗標>` 依站別換：ASM 用 `IsAssembled`、Press 用 `IsPressed`、AOI 用 `IsAoiInspected`。
新增一個「會處理整盤、下一站要等它做完才能收料」的站別時，這個旗標要在該站別的 `SetTrayWork()`（或等效方法）
裡設成 `true`，並且確保 `IsExist` 已經照上表被正確維護，不然這個 gate 又會變成死開關。

### Lane→Lane 交握：`WaitPreviousDoneLoad()` 根本不用覆寫

一開始以為這是「輪詢到不穩定的瞬間狀態」的問題（`m_enuAction` 有些值是迴圈裡才會設、穩定可輪詢，有些是一次性
終點、設完馬上被下一輪覆寫，輪詢會賭運氣）——這個判斷方向沒錯，但換一個「比較穩定」的狀態繼續輪詢並沒有真正解決，
只是把賭輸的窗口變大。真正的答案是**這一步的輪詢本身就不該存在**：

- `BaseLane.WaitPreviousDoneLoad()` 的基底預設就是 `return true;`，`Proc_HS_Lane.cs`（唯一另一個實測正常運作的
  Lane）完全沒有覆寫它。
- 往回查 `NotifyPreviousLoadDone()` 的「情境 2：上游是 Lane」分支，裡面的註解自己講明白：「上游 Lane 在
  `Unload_Done` 後會自動清帳，這裡只需要確認上游已經完成 Unload 即可」——程式碼也真的什麼都沒檢查，直接標記完成。
- 更關鍵的保證：上游在自己的 `case 60500`（`Unload_Waiting_Sign`）偵測到下游到達 `Loading` 時，會**同一個 scan
  cycle 內**呼叫 `TransferTrayBillToNextLane()` → 下游的 `ReceiveTrayBillFromLane()`，把帳整包轉過去。下游要走到
  自己的 `WaitPreviousDoneLoad()` 檢查點（`case 50500`），中間還要經過 `50210→50300→50310→50400` 好幾個模擬
  sensor 延遲，結構上一定比上游轉帳晚到——所以「帳已經轉移完成」這件事，下游檢查的當下必然已經成立，
  不需要主動確認。

`Proc_Press_Lane.cs`／`Proc_AOI_Lane.cs` 原本都覆寫了這個 hook 去輪詢上游 `m_enuAction`，這段覆寫本身就是多餘
且會卡死的根源，2026-08-22 已經整段刪除（見 `LESSONS.md` L8）。**以後寫 Lane→Lane 交握，`WaitPreviousDoneLoad()`
不用覆寫，直接吃預設值。** 真正需要覆寫的是 `ReadyToLoad()`（決定上游準備好了沒，才能開始物理入料）跟
`ReadyToUnloadToNext()`／`WaitNextLoadDone()`（決定下游準備好了沒），這三個都是「迴圈裡持續檢查、成立前一直
停在原地」的用法，跟 `WaitPreviousDoneLoad()` 的「單次檢查、檢查完就往下走」用法不一樣，不要照樣造句加了不必要
的覆寫。

## 四個容易漏掉的註冊點

每加一組新的 AR/Proc，以下四個地方要一起改，漏一個不會編譯錯，只會在跑起來後才發現流程卡住：

1. **`.csproj`**：舊式專案格式，新檔案不會自動被抓進編譯，要手動加 `<Compile Include="..." />`，不然
   `CS0103` 說類別不存在。
2. **`ProcAutoRun.cs` case 1000**：`AR_Xxx.GetSingleton().Run_AutoRun();`——沒加，這個 AR 永遠不會被啟動。
3. **`ProcInitial.cs` case 1000 + 1010**：`AR_Xxx.GetSingleton().RunInitial();` 和對應的
   `ProcInitialDone &= AR_Xxx.GetSingleton().IsProcOK();`——兩個要成對加，只加前面那個，`ProcInitialDone`
   不會真的反映這個 AR 的初始化狀態。
4. **`ProcInitial.cs` case 2000 + 2010**：同上，但是 `Proc_Xxx`（設備本體）那組。

## 實例：目前完整生產線關係鏈

隨著新的 AR/Proc 加進來持續更新這張圖，當作查詢「這個 Proc 的上下游是誰、對應哪個 AR」的速查表：

```
HS Feed Magazine --(AR_Mag_HS_Feed)--> HS Lane --(AR_Mag_HS_Discharge)--> HS Discharge Magazine
                                          ^
                                          | AR_ASM_Arm 從這裡撿散熱片
                                          |
IC Feed Magazine --(AR_Mag_IC_Feed)--> ASM Lane
                                          |
                                  (AR_ASM_Lane 卸料，需整盤組裝完成)
                                          v
                                      Press Lane <--(AR_Press_Station 整盤壓合)
                                          |
                                  (AR_Press_Lane 卸料，需整盤壓合完成)
                                          v
                                      AOI Lane <--(AR_AOI_Station 逐格檢測)
                                          |
                                  (AR_AOI_Lane 卸料，需整盤檢測完成)
                                          v
                                      OK Lane --(AR_Mag_OK_Discharge，需 Sort_Arm 分完 NG)--> OK Discharge Magazine
                                          ^
                                          | AR_Sort_Arm：OK 判定留原地，NG 判定撿走
                                          v
NG Feed Magazine --(AR_Mag_NG_Feed，供應空Tray)--> NG Lane --(AR_Mag_NG_Discharge，依 Recipe 出料模式)--> NG Discharge Magazine
```

### ASM 組裝（2026-08-21）

- `AR_ASM_Arm`：逐格比對 `HS_Lane().m_Temp_Tray_Info` 是不是 `HeatSink`、`ASM_Lane().m_Temp_Tray_Info` 同一格
  是不是 `Substrate`，兩個都成立才 `RunPick(PPStation.HeatSink, col, row)` → `RunPlace(PPStation.IC, col, row)`。
- `AR_ASM_Lane` 的 `CanUnload()` 比 `AR_HS_Lane` 多一個條件：`!AssyRecords.Any(v => v.IsExist && !v.IsAssembled)`，
  確保半成品不會被卸到 Press Lane。

檔案：[AR_ASM_Lane.cs](ArtEQ/2_Function(流程)/AutoRun/AR_ASM_Lane.cs)、
[AR_ASM_Arm.cs](ArtEQ/2_Function(流程)/AutoRun/AR_ASM_Arm.cs)

### HS Discharge 收料（2026-08-22）

- `AR_Mag_HS_Discharge`：跟 `AR_Mag_HS_Feed`（推料方向）剛好相反，找的是**空**槽位，觸發的是 `RunUnload(slotNo)`
  （收料方向，見上面 `BaseMagazine` 方向警告）。
- 同一批順手修的：`AR_HS_Lane.CanUnload()` 原本誤指到上游 `Mag_HS_Feed()`，改成真正的下游 `Mag_HS_Discharge()`；
  `AR_Mag_HS_Feed.CanLoad()` 補上 `NextLane().m_enuAction == Unload_Done` 這個分支，不然 HS Lane 卸完第一輪
  之後永遠回不去 `CanLoad()` 會接受的狀態，整條線只能跑一輪就卡住。

檔案：[AR_Mag_HS_Discharge.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Mag_HS_Discharge.cs)

### Press 壓合（2026-08-22）

- 壓合站是**一次把整盤裡所有有料的格子壓完**，不是逐格 Pick/Place——`BasePressStation`／`AR_Press_Station`
  就是為了這種「整盤一次處理完」的站別設計的（跟 Arm 型的逐格模式不同，見上面三種原型的表）。
- `AR_Press_Station`：`Press_Lane().m_Temp_Tray_Info.bIsExist` + `ArrivalSignal` 都成立、且盤子裡「還有
  `IsExist` 但 `!IsPressed` 的格子」時才觸發 `RunPress()`；壓完之後這個條件自然不再成立，不用額外記狀態。
- `AR_Press_Lane`：跟 `AR_ASM_Lane` 同一個模板，Load 的上游從 Magazine 換成 Lane（`Proc_ASM_Lane`），
  `CanUnload()` 的完成旗標從 `IsAssembled` 換成 `IsPressed`。

檔案：[AR_Press_Lane.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Press_Lane.cs)、
[AR_Press_Station.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Press_Station.cs)

### AOI 檢測（2026-08-22）

- AOI 站是**逐格移動鏡頭檢測**，不是整盤一次做完——跟 Press 長得像（都是「XxxStation」對應一條 Lane）但一次處理
  範圍不同，是四種原型裡的第四種（`BaseAoiStation`）。查 `Proc_AOI_Station.SetTrayWork()` 才確認到這件事
  （它只動 `tray.GetIndexFromRowCol(m_workRow, m_workColumn)` 那一格，不是迴圈整盤），不要看到「XxxStation」
  就預設照 Press 抄。
- `AR_AOI_Station`：邏輯上更接近 `AR_ASM_Arm`（逐格找位）而不是 `AR_Press_Station`（整盤觸發一次）——逐格找
  `AssyRecords[i].IsExist && !IsAoiInspected` 的格子，一次只處理一格，`RunInspect(col, row)` → 等
  `AOI_Done`/`AOI_Fail` → 回閒置，下一輪 scan 才會找下一格。
- `AR_AOI_Lane`：跟 `AR_Press_Lane` 同一個模板整段複製，本站從 `Proc_Press_Lane` 換成 `Proc_AOI_Lane`，
  上游從 `Proc_ASM_Lane` 換成 `Proc_Press_Lane`，下游從 `Proc_AOI_Lane` 換成 `Proc_OK_Lane`，`CanUnload()`
  的完成旗標從 `IsPressed` 換成 `IsAoiInspected`。
- `Proc_AOI_Lane.GetNextLaneForBill()` 目前寫死指向 `Proc_OK_Lane`，NG 品目前不會在 Lane 層被分流。原本以為
  分流是靠 `Proc_Sort_Arm` 依每格 `AoiResult` 分別 Pick 到 OK/NG——**這個假設查證後發現是錯的**，見下面
  「Sort Arm 分流機制——還沒搞懂」。

檔案：[AR_AOI_Lane.cs](ArtEQ/2_Function(流程)/AutoRun/AR_AOI_Lane.cs)、
[AR_AOI_Station.cs](ArtEQ/2_Function(流程)/AutoRun/AR_AOI_Station.cs)

### OK Lane / NG Feed·Lane·Discharge Magazine（2026-08-22）

- `OK_Lane` 的下游是 `Proc_OK_Discharge_Magazine`（Lane→Magazine，跟 `AR_HS_Lane` 的卸料半邊同形狀）。
  第一版做的時候沒有站別在中間處理，以為不需要完成旗標 gate——後來確認 `Sort_Arm` 分流機制後，補上了
  「這盤 Tray 裡不能還有殘留 NG 判定格子」的 gate（見下面 Sort Arm 那節）。
- `NG_Feed_Magazine → NG_Lane → NG_Discharge_Magazine` 整條線跟 `HS_Feed → HS_Lane → HS_Discharge` **形狀
  完全一樣**，`AR_Mag_NG_Feed`／`AR_NG_Lane`／`AR_Mag_NG_Discharge` 直接照 HS 那三個檔案改名字複製。
- `Proc_OK_Lane.cs` 也踩了跟 `Proc_Press_Lane`/`Proc_AOI_Lane` 一樣的 `WaitPreviousDoneLoad()` 多餘覆寫，
  一併拿掉了（第三個中招的檔案，見 `LESSONS.md` L8）。
- **`AR_Mag_OK_Discharge.cs`／`AR_Mag_NG_Discharge.cs` 照抄 `AR_Mag_HS_Discharge.cs` 範本時，`CanUnload()`
  判斷上游 Lane 出料到位的狀態值抄錯**：寫成 `Unload_Waiting_Sign`（case 60500，料已送出只等下游 ACK）而非
  `Unload_Waiting`（case 60200，Lane 卡著等下游準備好的早期狀態）。因為 `Proc_OK_Lane`／`Proc_NG_Lane` 的
  `ReadyToUnloadToNext()` 要 Magazine 先進 `Magazine_Unload_Waiting` 才放行過 60200，而 Magazine 要靠這裡
  呼叫 `RunUnload()` 才會進那個狀態——等 `Unload_Waiting_Sign` 的話 Lane 永遠到不了，兩邊互相等死鎖，
  實測結果是 OK/NG Discharge Magazine 完全沒有動作。已修正為 `Unload_Waiting`。詳見 `LESSONS.md` L10。
  **複製 AR 檔案當範本時，凡是拿來跟 `enuAction` 狀態比較的常數都要逐一比對原檔，不能只看邏輯形狀像不像。**

檔案：[AR_OK_Lane.cs](ArtEQ/2_Function(流程)/AutoRun/AR_OK_Lane.cs)、
[AR_Mag_OK_Discharge.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Mag_OK_Discharge.cs)、
[AR_Mag_NG_Feed.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Mag_NG_Feed.cs)、
[AR_NG_Lane.cs](ArtEQ/2_Function(流程)/AutoRun/AR_NG_Lane.cs)、
[AR_Mag_NG_Discharge.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Mag_NG_Discharge.cs)

### Sort Arm 分流機制（2026-08-22 確認 + 做完）

`NG_Feed_Magazine` 供應的是**整盤標成 Empty 的空 Tray**（不是預先裝好的 NG 成品），推進 `NG_Lane` 停在那邊
等 `Sort_Arm` 逐格填。確認後的分流規則：

- `RunPick(PPStation.OK, col, row)`：從 `OK_Lane` 撿——但只挑「有料、`AoiResult=Ng`、還沒搬走」的格子，
  OK 判定的格子**完全不碰**，手臂不會對它做任何 Pick/Place 動作。
- `RunPlace(PPStation.NG, col, row)`：放到 `NG_Lane`——**找下一個空格循序塞**，不是跟 `OK_Lane` 同一個
  (row,col) 對應。位置對應在 `FullTray` 模式下會撞格(同一個位置可能被好幾輪不同批次的 NG 搶用)，所以
  Pick 跟 Place 用的 (col,row) 一開始就是兩組獨立算出來的，不是同一組數字重複用兩次(跟 `AR_ASM_Arm` 的
  「同位置」模式不一樣，這是本文件目前唯一一個 Pick/Place 位置不對應的 Arm)。

**NG 出料時機是可設定的**，Recipe 參數 `clsEnum.NGDischargeMode`(`ArtData/clsEnum.cs`)：
- `PerCycle`：這一輪(對應目前 `OK_Lane` 那盤)`Sort_Arm` 分完就出料，不等下一輪的 NG 湊在一起。
  (原本命名 `Immediate`，2026-08-22 改名——「即時」給人單顆分完馬上出的錯覺，實際是逐輪出料。)
- `FullTray`：`NG_Lane` 自己收滿整盤(跨好幾輪 `OK_Lane` 循環)才出料。

`AR_NG_Lane.GetNGDischargeMode()` 已經接上真的 Recipe 參數(`Rec_Sort_Type`，透過
`ucParameter.GetValueInt()` 讀值後轉型)，不再是寫死的佔位方法；`ucFunctionSetting.cs` 有對應的
`cboSortType` 下拉選單(`DataSource = Enum.GetNames(typeof(NGDischargeMode))`)。

**「這一輪分完了沒」用鎖定的旗標，不是即時查詢**——`AR_Sort_Arm.bIsSortDone`：`Sort_Arm` 自己在找不到
`OK_Lane` 裡下一個待搬 NG 格時設成 `true`，下次又找到新的待搬格才清回 `false`。`Immediate` 模式的
`AR_NG_Lane.CanUnload()` 讀這個旗標，**不直接查 `OK_Lane` 的即時帳**——原因是：如果改成每次都現查
「`OK_Lane` 現在還有沒有殘留 NG」，一旦查詢的時間點晚於 `OK_Lane` 自己已經出料、開始收下一輪新 Tray，
查到的「沒有殘留 NG」有可能只是新那盤還沒跑到 AOI 階段的假象，不是「我剛剛收的這批真的分完了」——這跟
`LESSONS.md` L8／L9 抓到的兩次死結是同一種病(輪詢一個會被別人動態改變的即時狀態，而不是鎖定某個時間點
的結果)，這次直接用旗標避開，不重蹈覆轍。

`AR_OK_Lane.CanUnload()` 也補上了對稱的 gate：`!AssyRecords.Any(v => v.IsExist && v.AoiResult == AoiResult.Ng)`，
確保 `OK_Lane` 要等 `Sort_Arm` 把 NG 都搬完才出料到 `OK_Discharge_Magazine`。

檔案：[AR_Sort_Arm.cs](ArtEQ/2_Function(流程)/AutoRun/AR_Sort_Arm.cs)

### Tray 格數 / Magazine Slot 數改成 Recipe 動態值（2026-08-22）

原本 `clsTrayInfo` 的 Row/Col 跟 `BaseMagazine` 的 Slot 數都是寫死的常數(2×3、5 個 Slot)。改成 Recipe 參數
驅動時，關鍵發現是：**格子計數的地方全部都已經用 `tray.iRows`/`tray.iCols`／`m_iUseSlotCount` 動態算，沒有
任何地方寫死字面數字**——`AR_ASM_Arm.cs`、`AR_AOI_Station.cs`、`BaseMagazine.cs`、`ucTrayDisplay.cs` 的畫面
渲染都已經是動態讀值。所以真正要改的地方很小：

- **`clsTrayInfo` 建構子**：改成 `SetGridSize(Math.Max(1, GetPmt(Rec_Tray_Row_Number)), Math.Max(1, GetPmt(Rec_Tray_Column_Number)))`，
  取代原本寫死的 `2`/`3`。外面包 `Math.Max(1, ...)` 防呆——`SetGridSize` 對 `<=0` 會直接 `throw`，Recipe
  還沒真的載入、`GetValueInt` 回傳預設 0 時會炸掉建構子，比原本寫死的舊行為風險更高，一定要擋下限。
- **`clsTrayInfo.CopyTo()`**：原本沒有同步 `iRows`/`iCols`(只複製 `Materials`/`AssyRecords` 內容)，格數固定
  2×3 時從沒被踩到。一旦格數可變，這是整條產線帳料傳遞的核心路徑(Lane→Lane、Magazine→Lane、Arm 都靠它)，
  來源跟目標建立時間點的 Recipe 值不同時，目標的 `arrItemStatus` 長度會跟複製過去的資料對不上，`SetItemStatus()`
  對超出舊長度的格子靜默不做事(不噴例外，畫面就是不會更新)。修法：`CopyTo()` 一開始先呼叫
  `p_Target.SetGridSize(this.iRows, this.iCols)`，把目標同步成來源的格數。
- **`BaseMagazine.RunInitial()`**：改成先 `SetMagazineSlotCount(GetPmt(Rec_Magazine_Slot_Number))` 再建帳。
  `NormalizeSlotCount()` 本來就會夾在 `[1, m_iSlotMax]`(`m_iSlotMax=5` 是硬體上限，維持寫死，Recipe 只能在
  這個上限內選，不會被 Recipe 蓋掉)，Recipe 沒載入回傳 0 也會被夾到 1，不需要額外防呆。
  既有的 `RunInitial(int p_iSlotCount)` overload(目前沒人呼叫，保留給未來手動指定用)拆成共用的
  `RunInitialCore()`，避免 `RunInitial()` 的 Recipe 讀值蓋掉 `RunInitial(int)` 的外部指定值。
  `InitialMagazineBill()` → `clsMagazineInfo.InitialSlot(slotMax)` 本來就是每次都 `Clear()` 整個 Dictionary
  重建 `1..slotMax`，所以 Recipe 調小 Slot 數會確實收斂 Magazine 的可用 Slot 數，不用額外處理。

**踩到的坑**：資料模型(`clsTrayInfo`)改成動態讀 Recipe 之後，`ucTrayDisplay` 畫面沒有跟著變——因為
`Initial(trayInfo)` 只在綁定當下同步一次 `iRows`/`iCols` 到控制項自己的欄位，`ReflashTimerFunc()`(定時器
持續呼叫)只有 `Invalidate()` 重繪，沒有重新同步格數。修法：`ReflashTimerFunc()` 裡也要重讀
`m_pTrayInfo.iRows`/`iCols`。詳見 `LESSONS.md` L11——**這是一個通用教訓：資料模型的某個屬性改成動態可變時，
要連著往下追每一個顯示/消費該屬性的地方，確認是「每次都重讀」還是「綁定當下讀一次就不會再變」。**

Recipe 改變 Row/Col/Slot 數之後，是「下一個新建的物件才套用新值」，不會追溯修改運轉中已存在的 Tray/Magazine
帳——跟 `MotorHighSpeed`、`CalculateSlotPosZ` 等其他 Recipe 參數的讀值方式一致(即時讀、不快取，但不主動
推播給已經存在的舊物件)。

檔案：[clsTrayInfo.cs](ArtEQ/4_Class(基本類別)/clsTrayInfo.cs)、
[ucTrayDisplay.cs](ArtEQ/C_Component(介面元件)/ucTrayDisplay.cs)、
[BaseMagazine.cs](ArtEQ/2_Function(流程)/BaseProc/BaseMagazine.cs)

**後續補完（Magazine Slot 數這條線，2026-08-22）**：`m_iSlotMax`(硬體上限，`BaseMagazine.cs`)後來又
調成 10。過程中發現 `ucManualForm.cs`／`ucAutoRun.cs` 的「Add Data」測試按鈕**各自宣告了一個同名但完全
獨立的 `m_iSlotMax`**(分別寫死 5、6)，只改 `BaseMagazine` 那份完全沒用——這是本文件目前唯一一個「改常數
要先搜尋整個專案有幾個同名宣告」的案例，詳見 `LESSONS.md` L13。
`ucMagazineDisplay.cs`(Magazine Slot 按鈕清單，跟 `ucTrayDisplay.cs` 是同一層級但獨立的顯示元件)本來就是
照 `m_trayInfo.Count` 動態產生按鈕，但一樣踩了 L11 那個「`ReflashTimerFunc()` 沒有重新同步」的坑(這次是
按鈕**顆數**沒跟著變，不是格子大小)，額外還踩到 `AutoScroll` 會在子控制項被點選時自動捲動、把更早的 Slot
推出畫面，以及 WinForms `Button.TextAlign` 在按鈕矮到一定程度後留白不隨控制項縮放的問題(L12)。
最終方案：按鈕高度／字型大小都在 `BuildSlots()` 當下依 `flpSlot` 實際可視高度現場計算(不再猜固定像素值)，
字型大小額外釘住「7 格時的高度」當上限、不再繼續縮小(操作者反饋 7 格字剛好，再縮太小不好讀)，文字改用
`TextRenderer.DrawText(..., TextFormatFlags.NoPadding)` 在 `Paint` 事件手動置中，不吃 Button 內建 `TextAlign`。

檔案：[ucManualForm.cs](ArtEQ/3_UI(介面管理)/2_Manual(手動模式)/ucManualForm.cs)、
[ucAutoRun.cs](ArtEQ/3_UI(介面管理)/1_Operator(操作模式)/ucAutoRun.cs)、
[ucMagazineDisplay.cs](ArtEQ/C_Component(介面元件)/ucMagazineDisplay.cs)
