# ArtMMI 專案指引

## 執行環境限制（重要）

**本專案 Target Framework 是 .NET Framework 4.0**——五個子專案(`ArtData`、`ArtEQ`、`ArtMMI`、`ArtSystem`、`ArtTeach`)的 `.csproj` 都寫死 `<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>`，`ToolsVersion="12.0"`(舊式 MSBuild 專案格式，無 `LangVersion` 設定)。

**寫或改 C# 程式碼時，不要使用超過 .NET Framework 4.0 版本的語法／API。** 常見地雷：

- ❌ `async` / `await`、`Task.Delay`、`Task.Run`——4.0 的 TPL 沒有 awaiter 支援(`INotifyCompletion` 等型別是 4.5 加入的)，直接用會編譯失敗。
- ❌ Tuple 語法 `(int, string)`、`System.ValueTuple`——4.0 沒有這個型別，沒裝對應 NuGet 套件就不能用。
- ❌ `IReadOnlyList<T>` / `IReadOnlyCollection<T>` / `IReadOnlyDictionary<TKey,TValue>`——4.5 才加入。
- ❌ `System.Net.Http.HttpClient`——4.5 才加入。
- ⚠️ C# 6 以後的語法糖(`nameof`、字串插值 `$"..."`、null 條件運算子 `?.`、`??=` 等)：能不能編譯取決於實際用來開此方案的 Visual Studio／編譯器版本，專案檔本身沒有鎖 `LangVersion`，**不確定就別用**，改用保守寫法(`string.Format` 取代插值、明確 `if (x != null)` 取代 `?.`)。

拿不準某個語法或 BCL API 在 .NET Framework 4.0 能不能用，先查證，不要假設「應該可以」就寫下去。

專案裡已經有 `ArtMMI/ArtMMI(主程式-不太需要維護).csproj` 等檔案針對 `TargetFrameworkVersion == v4.0` vs `v4.8` 切換不同版本的 `Newtonsoft.Json.dll`——代表曾經評估過升級到 4.8，但目前實際生效的設定仍是 v4.0，改動前留意這點，不要順手改掉 target framework。

## 完成定義(Definition of Done)

這個專案沒有自動化測試，「編譯成功」是唯一客觀、可以直接驗證的門檻。宣告一個 C# 改動「完成」之前：

1. **實際跑一次編譯，不要只憑閱讀判斷。** 本機已確認可用的 MSBuild：
   ```
   "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" <方案或專案路徑> /t:Build /p:Configuration=Debug
   ```
   貼出結尾「0 Error(s)」那幾行當證據；有 Error 就是沒完成，不要用「應該沒問題」帶過。
2. 改到 `Scenario()` 這類用 `iStepIndex` 驅動的狀態機檔案時，額外核對：每一處把 `iStepIndex` 設成某個值的地方，那個值都要有對應的 `case` 真的存在；每個 `case` 至少要有一條路徑會被走到。這類問題編譯器抓不出來，只能手動比對兩份清單（見 `LESSONS.md` L3，`BaseArm.cs` 就抓到過這種斷點）。
3. 動手前掃一眼 `LESSONS.md` 的標題行，避免重踩已知的坑。

## 卡住時的規則

同一個錯誤最多嘗試 **2 次**修正。第 2 次還是沒解決，停下來，把已經試過的方法和失敗證據列出來問，不要第 3 次原地重試同一招。

## CodeGraph（codegraphcontext MCP）的使用時機

專案已經被 CodeGraph 索引過，適合拿來做**粗略的結構性提問**：查有哪些 repo 被索引、function／class 名稱搜尋、大概統計。探索一個陌生檔案或子系統之前，可以先問一下省掉手動 Grep 的來回。

**但不要依賴它做完整或精確的分析——實測過，它對 C# 的支援明顯不完整：**

- `ArtMMI` 子專案有 49 個檔案，CodeGraph 只索引到 **9 個 function**；同一批檔案手動掃描找到的方法數是這個的幾十倍量級，代表它漏掉了大部分 C# method（解析器可能主要是為 Python／JS／TS 設計的，對 C# 覆蓋不全）。
- `find_most_complex_functions`／`cyclomatic_complexity` 這類分析功能對這個專案**完全失效**——查出來的複雜度值全部是預設的 `1`，不是真的算過。
- 這個 MCP server 曾在同一個 session 中途無預警斷線，不要當成一定可用的必要依賴。

一句話：「這個東西存在嗎、大概在哪」拿 CodeGraph 問可以省時間；「這個東西完整長怎樣、有多複雜、找不找得全」這種需要精確度或完整覆蓋率的問題，不要信 CodeGraph 的結果，改用 Grep／手動掃描驗證（見 `LESSONS.md` L5）。

## 踩坑紀錄

實戰中發現的專案特有陷阱記在 [LESSONS.md](LESSONS.md)，新條目加在最上面，固定格式：情境／錯誤做法／正確做法，每條 ≤6 行。
