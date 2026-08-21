# AR / Proc 架構筆記

> 給新 session 讀的架構速覽：這個專案的自動化流程(AutoRun)分成兩層——**Proc**(設備本體的狀態機)跟
> **AR**(什麼時候該叫設備動作的決策層)。要碰 `AutoRun`／`Scenario`／`AR_*`／`Proc_*` 相關程式碼之前，先讀這份。
> 內容是 2026-08-21 實作 ASM Lane 組裝流程時，逐檔讀出來的架構，不是憑空整理的規格書——拿不準的地方請直接重讀對應檔案確認。

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
基底：`BaseArm`、`BaseLane`、`BaseMagazine`（都在 `ArtEQ/2_Function(流程)/BaseProc/`）

Proc 類別代表一個實體設備（一支手臂、一條流道、一個料盒），自己的 `Scenario()` 是這個設備的動作細節
（馬達怎麼移動、真空怎麼開關、DI/DO 怎麼讀寫）。子類別只覆寫「這個設備專屬」的部分：

| 基底 | 子類別要覆寫的 hook | 用途 |
|---|---|---|
| `BaseArm` | `BindHardwarePoint()` | 綁定這支手臂的軸／DI／DO |
| | `ReadyToPick()` / `ReadyToPlace()` | 判斷來源／目的流道是否就緒 |
| | `GetPickLane()` / `GetPlaceLane()` | 依 `PPStation` + `m_pickPlace` 動態決定這次要對哪條流道動作 |
| | `TransferToLane()`（abstract） | 放料完成後怎麼把帳過到目的流道 |
| `BaseLane` | `BindHardwarePoint()` | 綁定 Roller／Stopper 的 DI／DO |
| | `ReadyToLoad()` / `ReadyToUnloadToNext()` / `WaitPreviousDoneLoad()` / `WaitNextLoadDone()` | 跟上下游（Magazine 或 Lane）交握 |
| | `GetPreviousMagazineForBill()` / `GetNextLaneForBill()` / `GetNextMagazineForBill()` | 指定上下游是誰（視情況只覆寫需要的那個） |

公開介面（AR 層會呼叫的）：
- `RunInitial()`（全部都有）
- Arm：`RunPick(PPStation, col, row)` / `RunPlace(PPStation, col, row)`
- Lane：`RunLoad()` / `RunUnload()`（無參數，物件自己知道上下游是誰）
- Magazine：`RunLoad(slotNo)`
- 狀態查詢：`IsProcOK()`（`!bIsProcessing && bIsReady`）、`m_enuAction`（每個基底自己定義的 enum，
  例如 `BaseArm.enuAction.Pick_Done`）

都是 singleton，`GetSingleton()` 拿實例。

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

Arm 沒有現成模板可抄（`AR_ASM_Arm` 是第一個）：因為 Arm 是「Pick 一次 + Place 一次」而不是「Load/Unload」，
狀態機多一組 Pick→Place 的序列，條件判斷也不是查 `m_enuAction`，而是逐格比對兩條流道的 `clsTrayInfo`
（細節見下一節）。

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
`GetItemStatus(i)` 就知道某一格目前是什麼材料，不需要另外查 `AssyRecords.IsExist`。**

`clsTrayInfo.Clear()` 只清 `bIsExist`/`sTrayID`/`iRowID`/`iColumnID`，**不會清 `arrItemStatus`／`AssyRecords`／
`Materials`**——重用一個 tray 物件前如果沒有明確覆寫每一格，舊資料會殘留（`BaseMagazine.cs` 的幾個測試建帳
方法就是活生生的例子，見 `LESSONS.md`）。

## 四個容易漏掉的註冊點

每加一組新的 AR/Proc，以下四個地方要一起改，漏一個不會編譯錯，只會在跑起來後才發現流程卡住：

1. **`.csproj`**：舊式專案格式，新檔案不會自動被抓進編譯，要手動加 `<Compile Include="..." />`，不然
   `CS0103` 說類別不存在。
2. **`ProcAutoRun.cs` case 1000**：`AR_Xxx.GetSingleton().Run_AutoRun();`——沒加，這個 AR 永遠不會被啟動。
3. **`ProcInitial.cs` case 1000 + 1010**：`AR_Xxx.GetSingleton().RunInitial();` 和對應的
   `ProcInitialDone &= AR_Xxx.GetSingleton().IsProcOK();`——兩個要成對加，只加前面那個，`ProcInitialDone`
   不會真的反映這個 AR 的初始化狀態。
4. **`ProcInitial.cs` case 2000 + 2010**：同上，但是 `Proc_Xxx`（設備本體）那組。

## 實例：ASM 組裝流程（2026-08-21 做的）

當作範例參考，完整關係鏈：

```
IC Feed Magazine --(AR_Mag_IC_Feed)--> ASM Lane <--(AR_ASM_Arm 從這裡撿散熱片)-- HS Lane <--(AR_Mag_HS_Feed)-- HS Feed Magazine
                                          |
                                  (AR_ASM_Lane 卸料，需整盤組裝完成)
                                          v
                                      Press Lane
```

- `AR_ASM_Arm`：逐格比對 `HS_Lane().m_Temp_Tray_Info` 是不是 `HeatSink`、`ASM_Lane().m_Temp_Tray_Info` 同一格
  是不是 `Substrate`，兩個都成立才 `RunPick(PPStation.HeatSink, col, row)` → `RunPlace(PPStation.IC, col, row)`。
- `AR_ASM_Lane` 的 `CanUnload()` 比 `AR_HS_Lane` 多一個條件：`!AssyRecords.Any(v => v.IsExist && !v.IsAssembled)`，
  確保半成品不會被卸到 Press Lane。

檔案：[AR_ASM_Lane.cs](ArtEQ/2_Function(流程)/AutoRun/AR_ASM_Lane.cs)、
[AR_ASM_Arm.cs](ArtEQ/2_Function(流程)/AutoRun/AR_ASM_Arm.cs)
