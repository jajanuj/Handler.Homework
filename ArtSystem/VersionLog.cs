//[ArtSystem] 優勢
//1. DesignForm 被鎖定。 (開發者只能夠編輯 ucTitle 和 ucHotkey)
//2. DesignForm 共設計了兩個樣式，PC-Design 和 Panel-Design。
//3. 切換畫面時，會詢問權限。 (舉例：Recipe未存檔可以取消切換頁面之動作)
//4. 語言切換功能不需要開發，只要\\INI 內有對應的文件,則自動新增Button切換語言按鍵。對應文件範例：Language_Japan.ini
//5. 語言切換,原本只能自動翻譯架構在FormMain內的所有Controls，新增掛載Controls功能。
//6. 語言切換,可多載翻譯檔案,可以直接載入標準翻譯文件。例：Language_TC-Standard.ini (其中Standard可以隨意命名)
//7. 模擬功能不會忘記關閉 (在個人工作電腦中可以存放模擬文件D:\\ArtSimulate.ini，設備沒有模擬文件則不會變成模擬模式。
//8. 內建自動登出設定與功能(滑鼠不動超時後自動登出)。
//9. WarningMessage功能。
//10. 硬體配置設定參數介面化。


//[其他功能]
//1. 關閉程式的權限設定
//2. DebugForm開啟的權限設定
//3. 自動綁訂INI文件到clsArtSystem.strINIPath指定路徑內如果有對應的INI文件則,開啟/關閉程式時會進行載入與備份。
//4. 指定INI文件強制綁訂到clsArtSystem.strINIPath指定路徑內。




//2. Laser 模組,
//3. ArtLink
//4. 軸名稱 IO名稱如何處理
//5. Alarm如何處理





//2. ucParameterDisplay 過濾器搜尋器
//3. Login Form New Design (刷卡, 區域帳號)
//4. 切換Recipe
//7. Card設定要說明 Rate (TPM 和 L122 DLL之間的轉換)
//8. 測試一下7856能不能分開Setup
