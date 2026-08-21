# LESSONS — ArtMMI 實戰踩坑紀錄

> 新條目加在最上面。每條格式：`## L{編號} [日期] 一行標題`，內文固定三欄：情境／錯誤做法／正確做法，總長 ≤6 行。
> 動手改程式碼前，先掃一遍標題行。

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
