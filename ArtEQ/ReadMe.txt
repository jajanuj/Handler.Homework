開發程式的流程：
1. 將編排好的IO表與軸數量,寫入clsEnum內。
2. clsEnum編輯好IO後，將 ...\\bin\\Debug\\INI\\artDioName.ini內的內容刪掉，讓系統重新建立新的翻譯檔案。
3. 建立對應的硬體宣告HardwareInit() 有多少張軸卡和IO卡。 (如果是ArtSystem,則到介面上設定)。
4. 確認設備流程單元，舉例：程式內有多少個Process
5. 現在2_Function(流程內)建立所有Process(空殼也沒關係)
6. 將所有Process與AR加入倒clsEditRunThread->CreatProc();內。
7. 建立手動測試介面，機台狀態介面。
8. 開始開發單動流程，與AR流程。
9. 模擬運作AutoRun。
10. 補充設備其他功能。






軟體架構說明
如何規劃AR, Process
PM模組的應用範例
SECS的應用範例
SPC的應用範例
