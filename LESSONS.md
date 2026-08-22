# LESSONS — ArtMMI 實戰踩坑紀錄

> 新條目加在最上面。每條格式：`## L{編號} [日期] 一行標題`，內文固定三欄：情境／錯誤做法／正確做法，總長 ≤6 行。
> 動手改程式碼前，先掃一遍標題行。

## L16 [2026-08-22] AR 層的 IsProcOK() 在 AutoRun 模式下永遠是 false，別拿來查「現在忙不忙」

情境：結批的 `case 2000` 淨空偵測，查手臂/Feed Magazine 忙不忙時查的是 `AR_Xxx.GetSingleton().IsProcOK()`。
實測六條流道都淨空了，`bAllDrained` 卻一直是 false——因為 AR 一旦被 `Run_AutoRun()` 啟動，就會在自己的
`case 100000`(閒置判斷)→執行→`100000`→...之間無限循環，整個 AutoRun 期間 `iStepIndex` 從來不會回到
`-1`，而 `IsProcOK()`(`!bIsProcessing && bIsReady`)只有流程真的走到 `-1` 才會是 true——AR 層這個旗標
天生就是死的 false，不代表設備現在忙不忙。
錯誤做法：看到 `IsProcOK()` 就當作「這個物件現在閒置」的通用旗標，沒有分清楚是查 AR 決策層還是
Proc 設備層。
正確做法：要查「設備現在有沒有正在動作」，要查底層 `Proc_Xxx`(被 AR 明確呼叫 `RunPick()`/`RunLoad()`
才啟動單一動作，做完會回到 `iStepIndex=-1`)的 `IsProcOK()`，不是 AR 層的。

## L15 [2026-08-22] 「這一輪完成」的旗標，早退出的 guard 在最後一輪會變成永久卡死

情境：`AR_Sort_Arm.MarkSortDoneIfNothingLeft()` 原本有 `if (!OK_Lane().bIsExist) return;`，用意是
「OK_Lane 還沒收到料時不要誤判成分完了」。但 Sort_Arm 搬完最後一顆 NG、回頭檢查的時間點如果晚於
OK_Lane 自己已經 Unload 完(帳被清掉)，這行判斷會提早跳出，`bIsSortDone` 永遠設不成 true。平常運轉
靠「下一輪 OK_Lane 又送料進來」蓋過去，不會被發現；結批的最後一輪沒有下一輪可以蓋過去，永久卡死
(NG_Lane 最後一盤出不去)。
錯誤做法：加一個「資料還沒來，先跳出」的 guard，看起來保守安全，但沒想過「資料已經來過又走了」
也會命中同一個 guard。
正確做法：拿掉這個 guard——`FindNextNGCell()` 對「沒有 Tray」跟「Tray 剛被清空」都會自然回傳
false，效果等同「沒有東西待搬」，不需要另外判斷來源是否還存在。**通用教訓：只在單一輪次內生效的
「完成」判斷，要想清楚在系統的最後一輪(沒有下一輪救援)會不會永久卡死，不能只驗證「連續運轉時被
蓋過去」的情況。**

## L14 [2026-08-22] 加急停/結批這類「全域模式旗標」時，不能直接套用到所有下游，要分清楚各自語意

情境：實作「結批」(bIsLotEnd)，直覺是「全部餵料點都要停」，把三個 Feed Magazine 的 `CanLoad()`
都加了 `!bIsLotEnd`。結果 NG_Feed 供應的是「空載具盤」不是新的生產原料，一結批就停，導致上游還在跑
的最後幾輪分出的 NG 沒有地方放。同一批改動裡，NG_Lane 的強制出料條件又只看 `bIsSortDone`，沒有另外
判斷「這盤有沒有東西」，結果空盤在 NG_Feed→NG_Lane→NG_Discharge 之間瘋狂進出；後來加了「有東西
才出」，又不小心把這個放寬套用到 `FullTray` 模式的所有情況，導致沒滿的盤子在上游都還沒流空時就被
提早出料。
錯誤做法：想到一個全域旗標(`bIsLotEnd`)，直接無差別套用到「看起來相關」的每一個下游判斷式，
沒有先想清楚每個下游各自的語意(進料 vs 供應載具、正常判斷 vs 強制出料的觸發時機)。
正確做法：全域模式旗標只在真正需要改變行為的地方加判斷，而且要想清楚**觸發時機**——結批的強制
出料只應該在「確定上游不會再有新東西」的那一刻才啟動，不是「旗標一設就無條件套用」。三次來回
才收斂，過程見本檔案 L14 commit 附近的對話記錄。

## L13 [2026-08-22] 同名但不同物件的常數散落在多個檔案，改一個以為全改了，其實漏兩個

情境：把 Magazine Slot 數改成 Recipe 動態值，只改了 `BaseMagazine.m_iSlotMax`。結果「Add Data」測試按鈕在
Manual 分頁跟 AutoRun 分頁各自灌資料到超過舊上限的 Slot 都沒有效果——追了兩輪(先懷疑資料層邏輯、又懷疑
UI 顯示元件)才發現 `ucManualForm.cs`、`ucAutoRun.cs` 各自都有一個**自己獨立宣告、剛好同名**的
`m_iSlotMax`(分別寫死 5 跟 6)，跟 `BaseMagazine.m_iSlotMax` 完全無關，是三個不同的常數。
錯誤做法：看到 `m_iSlotMax` 就當成同一個東西，只改定義的那個檔案，沒有全專案搜尋這個識別字有幾個宣告。
正確做法：改任何看起來像「全域上限/設定值」的常數之前，先 `Grep` 整個專案有幾個地方宣告同名識別字
(不是只搜尋用到的地方，是搜尋 `private/protected/public ... 型別 識別字 =` 這種宣告式)，同名不代表同一個。

## L12 [2026-08-22] WinForms Button 的 TextAlign 置中在按鈕很矮時會有固定留白，看起來像沒置中

情境：Magazine Slot 按鈕縮到 `~15px` 高之後，字型大小已經確認一致(用 debugger 量過 `Font.Height` 兩種
格數都是 11)，但文字看起來偏上或偏下、留白不均勻。原因是 `TextAlign = MiddleCenter` 靠 GDI 內建的文字
置中，會保留一段**不隨控制項高度縮放的固定內部留白**，控制項越矮這段固定留白占比就越明顯。
錯誤做法：以為「字放大/縮小」就能解決留白不均的觀感問題，一直調整字型大小公式。
正確做法：不要依賴 Button 內建 `TextAlign`，`Text` 留空，改接 `Paint` 事件用
`TextRenderer.DrawText(..., TextFormatFlags.NoPadding)` 自己量測文字範圍、手動算出正中央座標畫，
不受那段固定留白影響。

## L11 [2026-08-22] UI 顯示元件 Initial() 只綁定一次，資料模型改了格數畫面不會跟著動

情境：`clsTrayInfo` 的 Row/Col 改成讀 Recipe 動態值後，實測改 Recipe 為 3×4，debugger 證實 `clsTrayInfo` 建構子
確實讀到 3/4，但畫面還是照舊格式畫。追到 `ucTrayDisplay.Initial(trayInfo)` 只在綁定當下把 `iRows`/`iCols`
從 Tray 同步到控制項自己的欄位一次，`Initial()` 只在 `ucManualForm` 建構子跑一次；之後的 `ReflashTimerFunc()`
(定時器持續呼叫)只有 `Invalidate()` 重繪，沒有重新同步格數，畫面永遠停在第一次綁定當下的舊值。
錯誤做法：只確認資料模型(`clsTrayInfo`)讀到新值就結案，沒有連著往下追顯示元件是否也是「即時讀」還是「綁定當下讀一次」。
正確做法：`ReflashTimerFunc()` 裡也要重新讀 `m_pTrayInfo.iRows`/`iCols`，不能假設 `Initial()` 綁一次就永遠同步。
凡是「資料模型的某個屬性改成動態可變」，同時要檢查所有顯示/消費該屬性的地方是「每次都重讀」還是「只在綁定當下讀一次」。

## L10 [2026-08-22] 複製 AR_Mag_HS_Discharge.cs 當範本時把 Unload_Waiting 抄成 Unload_Waiting_Sign，Lane↔Magazine 互相卡死

情境：`AR_Mag_OK_Discharge.cs`／`AR_Mag_NG_Discharge.cs` 照 `AR_Mag_HS_Discharge.cs` 範本寫，`CanUnload()` 判斷
「上游 Lane 出料到位」卻寫成 `Unload_Waiting_Sign`（case 60500，料已送出只等下游 ACK）而非 `Unload_Waiting`
（case 60200，Lane 卡著等下游準備好的早期狀態）。`Proc_OK_Lane.ReadyToUnloadToNext()` 要 Magazine 先進
`Magazine_Unload_Waiting` 才放行過 60200，而 Magazine 要靠這裡呼叫 `RunUnload()` 才會進那個狀態——等
`Unload_Waiting_Sign` 的話 Lane 永遠到不了，兩邊互相等死鎖。實測 log：`OK_Lane` 卡在 `Case:60200` 不動，
`OK_Discharge_Magazine` 從頭到尾沒再進 `Magazine_Unload`(30000) 系列。
錯誤做法：複製範本檔案時只比對「大致邏輯像不像」，沒有逐行比對常數值是否一致。
正確做法：改回 `Unload_Waiting`，跟能動的 `AR_Mag_HS_Discharge.cs` 一致。以後複製 AR 檔案當範本，凡是拿來跟
`enuAction` 狀態做比較的常數，都要跟原檔逐一比對過一次，不能只看「看起來一樣」。

## L9 [2026-08-22] AR 的 CanUnload() 檢查下游狀態時漏掉「下游剛做完、閒置中」那個分支，兩條 Lane 互相等死鎖

情境：`AR_ASM_Lane.CanUnload()` 檢查 `Press_Lane().m_enuAction == Load_Waiting || Initial_Done` 才觸發卸料。
第一輪能跑是因為 `Initial_Done`（開機才有、只出現一次）讓 `Press_Lane` 提早卡位等待；第二輪開始 `Press_Lane`
做完會停在 `Unload_Done`，不在清單裡，`ASM_Lane` 永遠不觸發卸料，也就永遠到不了 `Press_Lane.CanLoad()` 要看的
`Unload_Waiting`——兩邊互相等對方先進入「正在等待」的姿勢，死結。`AR_Press_Lane`／`AR_AOI_Lane` 檢查各自下游
Lane 時也是同一個洞。
錯誤做法：`CanXxx()` 檢查下游 Lane 狀態時，只列「下游正在主動等待」的狀態（`Load_Waiting`），漏了「下游剛跑完
一輪、閒置中」的終點狀態（`Unload_Done`）。
正確做法：跟 `AR_Mag_HS_Feed`／`AR_Mag_IC_Feed` 檢查目標 Lane 時一樣，把「閒置中」的終點狀態也列進 OR 清單。
`AR_ASM_Lane.cs`／`AR_Press_Lane.cs`／`AR_AOI_Lane.cs` 三個的下游檢查都補上 `Unload_Done` 了。

## L8 [2026-08-22] Lane→Lane 交握的 WaitPreviousDoneLoad() 根本不該覆寫、不該輪詢上游狀態

情境：`Proc_Press_Lane.WaitPreviousDoneLoad()` 覆寫成輪詢 `ASM_Lane.m_enuAction`。第一次改成查 `Unload_Done`，
`AR_ASM_Lane` 一到 `Unload_Done` 就馬上搶著開下一輪 Load 把它蓋掉（實測 231ms），卡在 `case 50500`；改查更穩定
的 `Unload_Waiting_Sign` 後還是卡（窗口變大但沒歸零，`Press_Lane` 自己的模擬 sensor 延遲比 `ASM_Lane` 轉去跑
下一輪的速度慢，等它真正檢查時上游早跑遠了）。
錯誤做法：以為只是「輪詢到錯的狀態」，換一個看起來比較穩定的狀態繼續輪詢。
正確做法：查 `NotifyPreviousLoadDone()` 才發現 Lane→Lane 這段根本設計成不用回頭確認——上游在自己的
`case 60500` 就會**主動**把帳轉過來、清自己的帳，`ReceiveTrayBillFromLane()` 保證在下游走到 `WaitPreviousDoneLoad()`
之前就已經完成。`BaseLane` 的預設值本來就是 `return true`，`Proc_HS_Lane.cs` 也從未覆寫這個方法。
把 `Proc_Press_Lane.cs`／`Proc_AOI_Lane.cs` 的覆寫整段刪掉，回歸預設值即可。

## L7 [2026-08-21] 資料夾名稱撞到 .gitignore 的建置產出樣式，整個資料夾被靜默忽略

情境：`.gitignore`（GitHub 官方 VisualStudio 範本）裡有 `[Aa][Rr][Mm]/`、`[Aa][Rr][Mm]64/`，本來是要擋掉 ARM 平台的建置輸出資料夾。
但這兩個是沒有路徑錨定的裸資料夾名稱樣式，會在任何深度比對——專案裡剛好有一個真的原始碼資料夾叫 `Proc/Arm/`
（放 `Proc_ASM_Arm.cs`、`Proc_Sort_Arm.cs`），整個資料夾因此被 git 靜默忽略，`git status` 完全不會顯示它們，
兩個檔案從 `git init` 那次就沒有被 commit 過，一直沒人發現。
錯誤做法：假設範本 `.gitignore` 的樣式一定安全，不會跟專案自己的資料夾命名撞在一起。
正確做法：懷疑某個檔案「應該在但讀不到」時，先用 `git check-ignore -v <path>` 確認是不是被忽略規則擋住，而不是只確定路徑對不對。
這個專案已經把 `[Aa][Rr][Mm]/`、`[Aa][Rr][Mm]64/` 從 `.gitignore` 移除（本專案不會建置 ARM 平台，這兩條純粹是誤傷）。

## L6 [2026-08-21] 對話中間隔幾輪後，不能沿用稍早讀過的檔案內容

情境：稍早分析過 `BaseArm.cs`，指出 `case 30710` 導向不存在的 `case 30720`（斷點 bug）。幾輪對話之後（中間討論了別的主題），
規劃新功能時又拿同一個結論來講，但使用者其實已經在 Visual Studio 裡把這段改掉了（新增了 `case 30800`，`30710` 也改成
真空檢知判斷）。當時 `git status` 其實已經顯示 `BaseArm.cs` 是 modified，只是沒有先看就直接引用舊分析，被使用者當場抓到。
錯誤做法：沿用對話中稍早 `Read` 過的檔案內容做判斷，沒有重新讀取就下結論或繼續規劃。
正確做法：要引用某檔案的具體邏輯之前，先重新 `Read` 一次；動手前也先看 `git status`，modified 但不是自己改的檔案，
代表背後有人在動，一定要重讀最新內容再開口。

## L5 [2026-08-21] CodeGraph 對這個 C# 專案的索引明顯不完整，複雜度分析完全失效

情境：`ArtMMI` 子專案 49 個檔案，CodeGraph（`get_repository_stats`）只索引到 9 個 function；同一批檔案手動用正則掃描找到全專案約
2660 個 method，差了幾十倍。`find_most_complex_functions`／`cyclomatic_complexity` 查出來的值全部是預設的 `1`，代表沒有真的分析過 C#。
這個 MCP server 也曾在同一個 session 中途無預警斷線。
錯誤做法：直接把 CodeGraph 回報的「複雜度最高函式」或「查不到就代表沒有」當結論。
正確做法：CodeGraph 只拿來做粗略的結構性提問（有沒有索引到、名稱搜尋）；要精確結果（複雜度排名、完整函式清單、呼叫關係）一律用
Grep／手動掃描驗證，不要單靠這個工具的分析結果下結論。

## L4 [2026-08-21] 改完 C# 要實際跑一次 MSBuild，不能只憑閱讀判斷

情境：這個方案沒有自動化測試，唯一客觀的驗證手段是編譯。本機已確認裝了 VS2019 的 MSBuild，路徑：
`C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe`。
錯誤做法：改完程式碼只憑閱讀「應該沒問題」就交差。
正確做法：跑 `MSBuild.exe <方案或專案> /t:Build /p:Configuration=Debug`，貼出結尾「0 Error(s)」那幾行當證據。

## L3 [2026-08-21] 動 Scenario()／狀態機類檔案時，要逐一核對每個 iStepIndex 轉移目標

情境：`BaseArm.cs` 的 `case 30710` 把成功分支導向 `30720`，但整份檔案沒有 `case 30720`——流程會卡死在那個 `iStepIndex`，
永遠不失敗也不完成。這種問題編譯器完全抓不到，肉眼掃過去也很容易漏掉。
錯誤做法：只檢查語法對不對，沒有把每個「`iStepIndex = X`」的 X 拿去比對是否真的有對應的 `case X`。
正確做法：改動這類檔案後，列出「所有 case 標籤」和「所有賦值目標」兩份清單，互相比對：有沒有目標對不到任何 case（斷點），
或 case 對不到任何轉移（死代碼、永遠不會被執行到）。

## L2 [2026-08-21] .NET Framework 4.0 的語法邊界要查證，不能憑「這語法看起來很基本」的直覺猜

情境：五個子專案都鎖 `TargetFrameworkVersion=v4.0`，但專案檔沒有寫死 `LangVersion`。哪些新語法／BCL API 能編、哪些不能，
不是靠感覺就能判斷對的（例如 `async`/`await`、tuple、`IReadOnlyList<T>` 都是 4.5 才加入的 BCL 型別支援）。
錯誤做法：想著「反正是新版 C# 編譯器，這語法應該都支援」就直接寫下去。
正確做法：比照 `CLAUDE.md` 列出的黑名單先擋掉；遇到不在清單上、拿不準的 API，查證它是哪個 .NET 版本加入的再用。

## L1 [2026-08-21] 中文路徑／檔名在 Windows + Python 腳本環境容易亂碼，要顯式指定 UTF-8

情境：專案資料夾與檔名大量使用繁體中文（例如 `ArtEQ\2_Function(流程)`）。Python 腳本用 `print()` 或 shell 重導向
（`python script.py > out.txt`）寫檔時，若沒指定 encoding，會依系統 ANSI codepage（cp950）編碼，之後被當 UTF-8 讀回來就整段亂碼。
錯誤做法：直接用 stdout 重導向，或 `open()` 不指定 `encoding`。
正確做法：腳本內用 `open(path, 'w', encoding='utf-8')` 直接寫檔，不要依賴 stdout 重導向的預設編碼。
