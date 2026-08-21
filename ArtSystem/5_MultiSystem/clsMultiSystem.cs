using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.Windows.Forms;

namespace ArtSystem.MultiSystem
{
    public class clsMultiSystem
    {
        #region //===================== 區域變數設置 =====================

        /// <summary> 是否使用多載系統 </summary>
        static public bool bIsMultiSystem
        {
            get;
            private set;
        }
        /// <summary> 系統名稱 </summary>
        static public string sSystemName
        {
            get;
            private set;
        }
        /// <summary> 系統存放INI的路徑 </summary>
        static public string strSystemINIPath
        {
            get;
            private set;
        }
        /// <summary> 讀取MachineModel的文件路徑(如果路徑文件不存在會讀取 相對路徑的文件...\\INI\\MachineModel.ini) </summary>
        static public string strMachineModelFilePath = "D:\\Parameter\\INI\\MachineModel.ini";
        /// <summary> 系統參數變更旗標(請重新啟動程式) </summary>
        static public bool bIsMultiSystem_SettingChanged
        {
            get;
            private set;
        }

        static public Dictionary<enuParameter, string> mPmt = new Dictionary<enuParameter, string>();

        #endregion

        #region//===================== Enum =====================

        public enum enuParameter
        {
            sSystemName,
            bRelativeFolder,
        }

        public enum enuMotionCard
        {
            /// <summary> 研華軸卡(4軸) </summary>
            ADVAN_1245,//脈波
            /// <summary> 股高軸卡(4軸) -實際6軸,底層寫4軸 </summary>
            GTS_4,//脈波
            /// <summary> Etel(2.X版本) 一張2軸 </summary>
            Etel,//線馬 - PCI直接連到控制器
            /// <summary> Etel(4.X版本) 一張2軸 </summary>
            Etel4,//線馬 - PCI直接連到控制器
            /// <summary> 先達SynTek控制卡(已經停止使用) </summary>
            L122,//脈波
            /// <summary> 研華EtherCAT控制卡 (PCI/PCIe 1203) </summary>
            EtherCatMasterAdv,//EtherCat
            /// <summary> 台灣控制卡(已經停產) </summary>
            ApsMasterTPM,//脈波
            /// <summary> 凌華控制卡 (4XMO軸卡,HSL-IO卡) (PCI/PCIe-7856) </summary>
            Card7856,//脈波
            /// <summary> 安川 </summary>
            YaskawaMP3000,
        }
        #endregion

        #region //===================== static public 函式設置 =====================

        /// <summary> 建立一軟多機系統 </summary>
        static public void InitialMultiSystem(Form pForm, string strSystemName = "")
        {
            try
            {
                ArtSystem.clsArtSystem.bIsProgramOpen = true;
                //開始記錄Log (如果Parent為Null,是不會顯示在介面上的)
                if (ucLogHistory.GetSingleton(clsArtSystem.g_strStartUpLogName).Parent == null)
                { ucLogHistory.GetSingleton(clsArtSystem.g_strStartUpLogName).Parent = pForm; }
                if (ucLogHistory.GetSingleton(clsArtSystem.g_strCatchLogName).Parent == null)
                { ucLogHistory.GetSingleton(clsArtSystem.g_strCatchLogName).Parent = pForm; }
                clsArtSystem.bIsCatchOccour();
                if (System.IO.File.Exists(strMachineModelFilePath) == true)
                {
                    System.IO.File.Copy(strMachineModelFilePath, System.IO.Directory.GetCurrentDirectory() + "\\INI\\MachineModel.ini", true);
                }
                else
                {
                    strMachineModelFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\MachineModel.ini";
                }
                if(System.IO.File.Exists(strMachineModelFilePath)  == false)
                {
                    System.IO.File.Create(strMachineModelFilePath);
                }
                if (System.IO.File.Exists(strMachineModelFilePath) == true)
                {
                    clsIniFile mFile = new clsIniFile(strMachineModelFilePath, false);
                    foreach (enuParameter ePmt in Enum.GetValues(typeof(enuParameter)))
                    {
                        if (mPmt.ContainsKey(ePmt) == false)
                        {
                            mPmt.Add(ePmt, "");
                        }
                        mPmt[ePmt] = mFile.GetString("MachineModel", ePmt.ToString(), "Default");
                    }
                    sSystemName = mPmt[enuParameter.sSystemName];
                    if (strSystemName.Length > 0)
                    {  sSystemName = strSystemName; }
                    if (mPmt[enuParameter.bRelativeFolder] == "1")
                    {
                        string strFolderPath = System.IO.Path.GetDirectoryName(strMachineModelFilePath) + "\\" + sSystemName;
                        strSystemINIPath = strFolderPath;
                    }
                    else
                    {
                        strSystemINIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\System\\" + sSystemName;
                    }
                    if (System.IO.Directory.Exists(strSystemINIPath) == false)
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(strSystemINIPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("[Cerate Directory Fail]\r\n" + strSystemINIPath);
                            clsArtSystem.CatchLog(ex);
                            return;
                        }
                    }
                    //載入系統參數
                    {
                        string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\CardInfo.ini";
                        if (System.IO.File.Exists("D:\\Parameter\\INI\\CardInfo.ini") == true)
                        { sPath = "D:\\Parameter\\INI\\CardInfo.ini"; }
                        ucCardSetting.GetSingleton().mCardInfo.Load(sPath);
                    }

                    //如果參數是相對關係
                    if (System.IO.File.Exists(strSystemINIPath + "\\artEqParameter.ini") == true
                        && mPmt[enuParameter.bRelativeFolder] == "1")
                    {
                        clsIniFile mEqParameter = new clsIniFile(strSystemINIPath + "\\artEqParameter.ini");
                        foreach (clsEnum.enuPmtType ePmtType in Enum.GetValues(typeof(clsEnum.enuPmtType)))
                        {
                            string sDefaultPath = strSystemINIPath + "\\Parameter\\" + ePmtType.ToString() + "\\Default.ini";
                            string sPath = mEqParameter.GetString("PmtFilePath", ePmtType.ToString(), sDefaultPath);
                            string sFileName = System.IO.Path.GetFileName(sPath);
                            string sNewPath = strSystemINIPath + "\\Parameter\\" + ePmtType.ToString() + "\\" + sFileName;
                            if (sFileName == "" || System.IO.File.Exists(sNewPath) == false)
                            { sNewPath = sDefaultPath; }
                            if (sPath != sNewPath)
                            {
                                mEqParameter.WriteValue("PmtFilePath", ePmtType.ToString(), sNewPath);
                            }
                        }
                    }
                    ReloadDll();//版本DLL複製
                    bIsMultiSystem = true;
                }
                else
                {
                    formMessageBox.Show("\"MachineModel.ini\" Not Exist.", "Multi System Initial Fail.");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 多機系統 : 硬體初始化 </summary>
        static public bool HardwareInitial()
        {
            bool CreateSuccess = true;
            if (bIsMultiSystem == false)
            {
                formMessageBox.Show("Please Initial Multi System.", "Hardware Initial Fail.");
            }
            else
            {
                clsDioCtrl.bIsActual = clsArtSystem.bIsSoftwareSimulate == false;
                clsMotionCtrl.bIsActual = clsArtSystem.bIsSoftwareSimulate == false;
                clsMotionCtrl.bIsDirectMode = clsArtSystem.bIsSoftwareSimulate == false;
                try
                {
                    #region//Card Setup
                    Dictionary<int, clsEtherCatMasterAdv> APS_EtherCAT = new Dictionary<int, clsEtherCatMasterAdv>();
                    clsApsMasterAdlink APS_7856 = null;
                    clsApsMasterSynTek APS_L122_DIO = null;
                    clsApsMasterSynTek APS_L122_Motion = null;
                    clsApsMasterTPM APS_TPM_DIO = null;
                    clsApsMasterTPM APS_TPM_Motion = null;
                    bool bHasYaskawa = false;
                    int iYaskawaMP3_AxisCount = 0;

                    string strInitialFailMessage = "";

                    #region//確認是否有7856
                    {
                        bool bHasDIO = false;
                        bool bHasMotion = false;
                        foreach (clsDIOCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstDIOCardInfo)
                        {
                            if (pSetup.eSysCard == enuMotionCard.Card7856)
                            {
                                bHasDIO = true;
                            }
                        }
                        foreach (clsMotionCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo)
                        {
                            if (pSetup.eMotionCard == enuMotionCard.Card7856)
                            {
                                bHasMotion = true;
                            }
                        }
                        if (bHasDIO == true && bHasMotion == true)
                        {
                            APS_7856 = new clsApsMasterAdlink(0, clsApsMasterAdlink.enuHslRate.Rate_6M, clsApsMasterAdlink.enuMnetRate.Rate_5M);
                        }
                        else if (bHasDIO == true)
                        {
                            APS_7856 = new clsApsMasterAdlink(0, clsApsMasterAdlink.enuHslRate.Rate_6M, null);
                        }
                        else if (bHasMotion == true)
                        {
                            APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M);
                        }
                    }
                    #endregion

                    #region//確認是否有Yaskawa (只有軸)
                    {
                        foreach (clsMotionCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo)
                        {
                            if (pSetup.eMotionCard == enuMotionCard.YaskawaMP3000)
                            {
                                iYaskawaMP3_AxisCount++;
                            }
                        }
                    }
                    #endregion

                    #region//建立 IO卡
                    foreach (clsDIOCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstDIOCardInfo)
                    {
                        bool bCreateSuccess = true;
                        switch (pSetup.eSysCard)
                        {
                            case clsMultiSystem.enuMotionCard.EtherCatMasterAdv:
                                #region//EtherCat DIO Create
                                try
                                {
                                    if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                    { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                    clsEnum.enuDi? eDI = ConvertToDI(pSetup.eStartDi);
                                    clsEnum.enuDo? eDO = ConvertToDO(pSetup.eStartDo);
                                    switch (pSetup.eDIOType)
                                    {
                                        case clsDIOCardSetup.enuDIOType.DI32:
                                            bCreateSuccess = APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_32DI, pSetup.iSlaveID, pSetup.bSimulate, eDI, null);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DO32:
                                            bCreateSuccess = APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_32DO, pSetup.iSlaveID, pSetup.bSimulate, null, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DI16DO16:
                                            bCreateSuccess = APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_16DI16DO, pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DI32DO32:
                                            bCreateSuccess = APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_32DI32DO, pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CreateSuccess = false;
                                    string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                                    formMessageBox.Show(sLog, "[Hardware Initial] Fail.");
                                    clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                                }
                                #endregion
                                break;
                            case clsMultiSystem.enuMotionCard.L122:
                                #region//L112 DIO Create
                                try
                                {
                                    if (APS_L122_DIO == null)
                                    { APS_L122_DIO = new clsApsMasterSynTek(clsApsMasterSynTek.enuMnetRate.Rate_10M, 0, 1); }
                                    clsEnum.enuDi? eDI = ConvertToDI(pSetup.eStartDi);
                                    clsEnum.enuDo? eDO = ConvertToDO(pSetup.eStartDo);
                                    switch (pSetup.eDIOType)
                                    {
                                        case clsDIOCardSetup.enuDIOType.DI32:
                                            bCreateSuccess &= APS_L122_DIO.AddIoModule(clsApsMasterSynTek.SlaveTypeIO.Di32, (ushort)pSetup.iSlaveID, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DO32:
                                            bCreateSuccess &= APS_L122_DIO.AddIoModule(clsApsMasterSynTek.SlaveTypeIO.Do32, (ushort)pSetup.iSlaveID, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DI16DO16:
                                            bCreateSuccess &= APS_L122_DIO.AddIoModule(clsApsMasterSynTek.SlaveTypeIO.Di16_Do16, (ushort)pSetup.iSlaveID, eDI, eDO);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CreateSuccess = false;
                                    string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                                    formMessageBox.Show(sLog, "[Hardware Initial] Fail.");
                                    clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                                }
                                #endregion
                                break;
                            case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                                #region//ApsMasterTPM DIO Create
                                try
                                {
                                    if (APS_TPM_DIO == null)
                                    { APS_TPM_DIO = new clsApsMasterTPM(clsApsMasterTPM.enuMnetRate.Rate_10M, 0, 1); }
                                    clsEnum.enuDi? eDI = ConvertToDI(pSetup.eStartDi);
                                    clsEnum.enuDo? eDO = ConvertToDO(pSetup.eStartDo);
                                    switch (pSetup.eDIOType)
                                    {
                                        case clsDIOCardSetup.enuDIOType.DI32:
                                            bCreateSuccess &= APS_TPM_DIO.AddIoModule(clsApsMasterTPM.SlaveTypeIO.Di32, (ushort)pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DO32:
                                            bCreateSuccess &= APS_TPM_DIO.AddIoModule(clsApsMasterTPM.SlaveTypeIO.Do32, (ushort)pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DI16DO16:
                                            bCreateSuccess &= APS_TPM_DIO.AddIoModule(clsApsMasterTPM.SlaveTypeIO.Di16_Do16, (ushort)pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CreateSuccess = false;
                                    string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                                    formMessageBox.Show(sLog, "[Hardware Initial] Fail.");
                                    clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                                }
                                #endregion
                                break;
                            case clsMultiSystem.enuMotionCard.Card7856:
                                #region//7856 DIO Create
                                try
                                {
                                    if (APS_7856 == null)
                                    { APS_7856 = new clsApsMasterAdlink(0, clsApsMasterAdlink.enuHslRate.Rate_6M, null); }
                                    clsEnum.enuDi? eDI = ConvertToDI(pSetup.eStartDi);
                                    clsEnum.enuDo? eDO = ConvertToDO(pSetup.eStartDo);
                                    switch (pSetup.eDIOType)
                                    {
                                        case clsDIOCardSetup.enuDIOType.DI32:
                                            bCreateSuccess &= APS_7856.AddHslModule(drvAdlinkAps168.enuAPS_HSL.SLAVE_NAME_HSL_DI32, (ushort)pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DO32:
                                            bCreateSuccess &= APS_7856.AddHslModule(drvAdlinkAps168.enuAPS_HSL.SLAVE_NAME_HSL_DO32, (ushort)pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;

                                        case clsDIOCardSetup.enuDIOType.DI16DO16:
                                            bCreateSuccess &= APS_7856.AddHslModule(drvAdlinkAps168.enuAPS_HSL.SLAVE_NAME_HSL_DI16DO16, (ushort)pSetup.iSlaveID, pSetup.bSimulate, eDI, eDO);
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CreateSuccess = false;
                                    string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                                    formMessageBox.Show(sLog, "[Hardware Initial] Fail.");
                                    clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                                }
                                #endregion
                                break;
                        }
                        clsEnum.enuDi[] DicDI = clsDioCtrl.lstDioDriver[clsDioCtrl.lstDioDriver.Count - 1].GetDiEnum();
                        for (int i = 0; i < DicDI.Length; i++)
                        {
                            clsDioMotion.mLst_DIEnum.Add(DicDI[i]);
                        }
                        clsEnum.enuDo[] DicDO = clsDioCtrl.lstDioDriver[clsDioCtrl.lstDioDriver.Count - 1].GetDoEnum();
                        for (int i = 0; i < DicDO.Length; i++)
                        {
                            clsDioMotion.mLst_DOEnum.Add(DicDO[i]);
                        }
                        CreateSuccess &= bCreateSuccess; 
                        if (bCreateSuccess == false)
                        {
                            strInitialFailMessage += "Create DI Card " + pSetup.eSysCard.ToString() + " Fail. (DI:" + pSetup.iSlaveID + ")" + "\r\n";
                        }
                    }
                    #endregion

                    #region//建立 軸卡
                    ////foreach (clsMotionCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo)
                    for(int iCardIndex = 0; iCardIndex < ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo.Count; iCardIndex++)
                    {
                        clsMotionCardSetup pSetup = ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo[iCardIndex];
                        bool bCreateSuccess = true;
                        try
                        {
                            if (clsArtSystem.bIsSoftwareSimulate == true
                                || pSetup.bSimulate == true)
                            {
                                #region//模擬建立
                                switch (pSetup.eMotionCard)
                                {
                                    case clsMultiSystem.enuMotionCard.ADVAN_1245:
                                        #region//模擬 ADVAN_1245
                                        if (APS_7856 == null)
                                        { APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M); }
                                        CreateSuccess &= APS_7856.AddMnetModule(drvAdlinkAps168.enuAPS_MNET.SLAVE_NAME_MNET_4XMO, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        #endregion
                                        break;
                                    case clsMultiSystem.enuMotionCard.GTS_4:
                                        #region//模擬 GTS_4
                                        if (APS_7856 == null)
                                        { APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M); }
                                        CreateSuccess &= APS_7856.AddMnetModule(drvAdlinkAps168.enuAPS_MNET.SLAVE_NAME_MNET_4XMO, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        #endregion
                                        break;
                                    case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                                        #region//模擬 ApsMasterTPM
                                        if (APS_7856 == null)
                                        { APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M); }
                                        CreateSuccess &= APS_7856.AddMnetModule(drvAdlinkAps168.enuAPS_MNET.SLAVE_NAME_MNET_4XMO, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        #endregion
                                        break;

                                    case clsMultiSystem.enuMotionCard.L122:
                                        #region//模擬 L122
                                        if (APS_7856 == null)
                                        { APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M); }
                                        CreateSuccess &= APS_7856.AddMnetModule(drvAdlinkAps168.enuAPS_MNET.SLAVE_NAME_MNET_4XMO, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        #endregion
                                        break;
                                    case clsMultiSystem.enuMotionCard.Card7856:
                                        #region//模擬 4XMO (7856)
                                        if (APS_7856 == null)
                                        { APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M); }
                                        CreateSuccess &= APS_7856.AddMnetModule(drvAdlinkAps168.enuAPS_MNET.SLAVE_NAME_MNET_4XMO, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        #endregion
                                        break;

                                    case clsMultiSystem.enuMotionCard.EtherCatMasterAdv:
                                        #region//模擬EtherCAT
                                        {
                                            #region//Simulate
                                            if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                            { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                            CreateSuccess &= APS_EtherCAT[pSetup.iCardID].AddMotionModule(pSetup.eSlaveType, pSetup.iSlaveID, pSetup.bSimulate);
                                            pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                            #endregion
                                        }
                                        #endregion
                                        break;

                                    case clsMultiSystem.enuMotionCard.Etel:
                                        #region//模擬Etel
                                        {
                                            for (int i = 0; i < pSetup.iETEL_SlaveNum; i++)
                                            {
                                                if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                                { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                                bCreateSuccess &= APS_EtherCAT[pSetup.iCardID].AddMotionModule(enuSlaveType_Motion.Motion_2xAxis, 100 + i, true);
                                            }
                                            pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                            for (int i = 0; i < pSetup.iETEL_SlaveNum; i++)
                                            {
                                                clsEnum.enuAxis Axis1 = clsMotionCtrl.lstMotionDriver[clsMotionCtrl.lstMotionDriver.Count - 1].dctAxisLib.ElementAt(0).Key;
                                                clsEnum.enuAxis Axis2 = clsMotionCtrl.lstMotionDriver[clsMotionCtrl.lstMotionDriver.Count - 1].dctAxisLib.ElementAt(1).Key;
                                                if (pSetup.LstGantryAxis.Contains(i) == true)
                                                {
                                                    clsAxisInfo pAxisInfo1 = new clsAxisInfo();
                                                    clsAxisInfo pAxisInfo2 = new clsAxisInfo();
                                                    clsMotionCtrl.GetAxisInfo(Axis1, ref pAxisInfo1);
                                                    clsMotionCtrl.GetAxisInfo(Axis2, ref pAxisInfo2);
                                                    pAxisInfo1.bIsGantry = true;
                                                    pAxisInfo1.enuGantryMainAxisName = Axis1;
                                                    pAxisInfo1.enuGantrySubAxisName = Axis2;
                                                    pAxisInfo2.bIsGantry = true;
                                                    pAxisInfo2.enuGantryMainAxisName = Axis1;
                                                    pAxisInfo2.enuGantrySubAxisName = Axis2;
                                                }
                                            }
                                        }
                                        #endregion
                                        break;
                                    case clsMultiSystem.enuMotionCard.Etel4:
                                        #region//模擬Etel4
                                        {
                                            for (int i = 0; i < pSetup.iETEL_SlaveNum; i++)
                                            {
                                                if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                                { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                                bCreateSuccess &= APS_EtherCAT[pSetup.iCardID].AddMotionModule(enuSlaveType_Motion.Motion_2xAxis, 100 + i, true);
                                            }
                                            pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                            for (int i = 0; i < pSetup.iETEL_SlaveNum; i++)
                                            {
                                                clsEnum.enuAxis Axis1 = clsMotionCtrl.lstMotionDriver[clsMotionCtrl.lstMotionDriver.Count - 1].dctAxisLib.ElementAt(0).Key;
                                                clsEnum.enuAxis Axis2 = clsMotionCtrl.lstMotionDriver[clsMotionCtrl.lstMotionDriver.Count - 1].dctAxisLib.ElementAt(1).Key;
                                                if (pSetup.LstGantryAxis.Contains(i) == true)
                                                {
                                                    clsAxisInfo pAxisInfo1 = new clsAxisInfo();
                                                    clsAxisInfo pAxisInfo2 = new clsAxisInfo();
                                                    clsMotionCtrl.GetAxisInfo(Axis1, ref pAxisInfo1);
                                                    clsMotionCtrl.GetAxisInfo(Axis2, ref pAxisInfo2);
                                                    pAxisInfo1.bIsGantry = true;
                                                    pAxisInfo1.enuGantryMainAxisName = Axis1;
                                                    pAxisInfo1.enuGantrySubAxisName = Axis2;
                                                    pAxisInfo2.bIsGantry = true;
                                                    pAxisInfo2.enuGantryMainAxisName = Axis1;
                                                    pAxisInfo2.enuGantrySubAxisName = Axis2;
                                                }
                                            }
                                        }
                                        #endregion
                                        break;
                                    case enuMotionCard.YaskawaMP3000:
                                        #region//YaskawaMP3000
                                        {
                                            for (int i = 0; i < pSetup.iETEL_SlaveNum; i++)
                                            {
                                                if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                                { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                                bCreateSuccess &= APS_EtherCAT[pSetup.iCardID].AddMotionModule(enuSlaveType_Motion.Motion_2xAxis, 150 + i, true);
                                            }
                                            pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        }
                                        #endregion
                                        break;
                                }
                                #endregion
                            }
                            else
                            {
                                #region//實際建立
                                switch (pSetup.eMotionCard)
                                {
                                    case clsMultiSystem.enuMotionCard.ADVAN_1245:
                                        #region//ADVAN_1245
                                        bCreateSuccess = clsMotionCtrl.AddDriver(new clsMotionDriverAdv().Initial(drvMotionAdvanTech.enuCardType.PCI_1245));
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.GTS_4:
                                        #region//GTS_4
                                        bCreateSuccess = clsMotionCtrl.AddDriver(new clsMotionDriverGts().Initial(clsMotionDriverGts.enuCardType.GTS_4));
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                                        #region//ApsMasterTPM
                                        if (APS_TPM_Motion == null)
                                        { APS_TPM_Motion = new clsApsMasterTPM(clsApsMasterTPM.enuMnetRate.Rate_10M, 0, 0); }
                                        APS_TPM_Motion.AddMnetModule(clsApsMasterTPM.SlaveType.AXIS_M304T, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.L122:
                                        #region//L122
                                        if (APS_L122_Motion == null)
                                        { APS_L122_Motion = new clsApsMasterSynTek(clsApsMasterSynTek.enuMnetRate.Rate_10M, 0, 0); }
                                        APS_L122_Motion.AddMotionModule(clsApsMasterSynTek.SlaveTypeMotion.AXIS_M224, (ushort)pSetup.iSlaveID);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.Card7856:
                                        #region//Card7856
                                        if (APS_7856 == null)
                                        { APS_7856 = new clsApsMasterAdlink(0, null, clsApsMasterAdlink.enuMnetRate.Rate_5M); }
                                        bCreateSuccess = APS_7856.AddMnetModule(drvAdlinkAps168.enuAPS_MNET.SLAVE_NAME_MNET_4XMO, (ushort)pSetup.iSlaveID, pSetup.bSimulate);
                                        pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.EtherCatMasterAdv:
                                        #region//EtherCatMasterAdv
                                        {
                                            if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                            { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                            bCreateSuccess &= APS_EtherCAT[pSetup.iCardID].AddMotionModule(pSetup.eSlaveType, pSetup.iSlaveID, pSetup.bSimulate);
                                            pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                        }
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.Etel:
                                        #region//Etel
                                        {
                                            bool bSuccess = clsMotionCtrl.AddDriver(new clsMotionDriverEtel().Initial(clsMotionDriverEtel.enuCtrlType.P2M300, pSetup.iETEL_SlaveNum));
                                            bCreateSuccess &= bSuccess;
                                            #region//Gantry 設定
                                            if (bSuccess == true)
                                            {
                                                clsMotionDriver pDriver = clsMotionCtrl.lstMotionDriver[clsMotionCtrl.lstMotionDriver.Count - 1];
                                                pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                                for (int iSlave = 0; iSlave < pSetup.iETEL_SlaveNum; iSlave++)
                                                {
                                                    if (pSetup.LstGantryAxis.Contains(iSlave) == true)
                                                    {
                                                        clsMotionDriverEtel.CreatGantry(pDriver.dctAxisLib.ElementAt(iSlave).Key, pDriver.dctAxisLib.ElementAt(iSlave + 1).Key);
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.Etel4:
                                        #region//Etel4
                                        {
                                            bool bSuccess = clsMotionCtrl.AddDriver(new clsMotionDriverEtel_V4().Initial(clsMotionDriverEtel_V4.enuCtrlType.P2M300, pSetup.iETEL_SlaveNum));
                                            bCreateSuccess = bSuccess;
                                            #region//Gantry 設定
                                            if (bSuccess == true)
                                            {
                                                clsMotionDriver pDriver = clsMotionCtrl.lstMotionDriver[clsMotionCtrl.lstMotionDriver.Count - 1];
                                                pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                                for (int iSlave = 0; iSlave < pSetup.iETEL_SlaveNum; iSlave++)
                                                {
                                                    if (pSetup.LstGantryAxis.Contains(iSlave) == true)
                                                    {
                                                        clsEnum.enuAxis MainAxis = pDriver.dctAxisLib.ElementAt(iSlave).Key;
                                                        clsEnum.enuAxis SubAxis = pDriver.dctAxisLib.ElementAt(iSlave + 1).Key;
                                                        clsMotionDriverEtel_V4.CreatGantry(MainAxis, SubAxis);
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        break;
                                        #endregion
                                    case clsMultiSystem.enuMotionCard.YaskawaMP3000:
                                        #region//YaskawaMP3000
                                        {
                                            bool bSuccess = true;
                                            if (bHasYaskawa == false)
                                            {
                                                bHasYaskawa = true;
                                                bSuccess &= clsMotionCtrl.AddDriver(new clsMotionDriverYaskawaMP3().Initial(clsMotionDriverYaskawaMP3.enuCtrlType.MP3100, pSetup.iETEL_SlaveNum * 2));
                                                pSetup.eStartAxis = SetAxisInfo(ref pSetup, ref clsDioMotion.mDic_AxisInfo);
                                            }
                                            bCreateSuccess = bSuccess;
                                        }
                                        break;
                                        #endregion
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            CreateSuccess = false;
                            string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                            MessageBox.Show(sLog, "[Hardware Initial] Fail.");
                            clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                        }

                        CreateSuccess &= bCreateSuccess;
                        if (bCreateSuccess == false)
                        {
                            strInitialFailMessage += "Create Motion Card " + pSetup.eMotionCard.ToString() + " Fail. (DI:" + pSetup.iSlaveID + ")" + "\r\n";
                        }
                    }
                    #endregion

                    #region//建立 AIO類比輸入輸出
                    foreach (clsAIOCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstAIOCardInfo)
                    {
                        try
                        {
                            clsEnum.enuDi? eDI = ConvertToDI(pSetup.eStartAi);
                            clsEnum.enuDo? eDO = ConvertToDO(pSetup.eStartAo);
                            switch (pSetup.eCardType)
                            {
                                case clsAIOCardSetup.enuCardType.PCI9112_2AO_8AI:
                                    clsDioCtrl.AddDriver(new clsDioDriverAdlink().Initial(8, 2, drvAdlinkIo.CardType.PCI_9112, (ushort)pSetup.iSlaveID, eDI, eDO));
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI9112_2AO_16AI:
                                    clsDioCtrl.AddDriver(new clsDioDriverAdlink().Initial(16, 2, drvAdlinkIo.CardType.PCI_9112, (ushort)pSetup.iSlaveID, eDI, eDO));
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI1203_8AI2AO:
                                    if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                    { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                    APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_8AI2AO, (ushort)pSetup.iSlaveID, clsArtSystem.bIsSoftwareSimulate, eDI, eDO);
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI1203_8AI:
                                    if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                    { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                    APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_8AI, (ushort)pSetup.iSlaveID, clsArtSystem.bIsSoftwareSimulate, eDI, eDO);
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI1203_4AO:
                                    if (APS_EtherCAT.ContainsKey(pSetup.iCardID) == false)
                                    { APS_EtherCAT.Add(pSetup.iCardID, new clsEtherCatMasterAdv(pSetup.iCardID)); }
                                    APS_EtherCAT[pSetup.iCardID].AddIoModule(enuSlaveType_IO.EtherCat_4AO, (ushort)pSetup.iSlaveID, clsArtSystem.bIsSoftwareSimulate, eDI, eDO);
                                    break;
                                default:
                                    clsDioCtrl.AddDriver(new clsDioDriverAdlink().Initial((uint)pSetup.iAICount, (uint)pSetup.iAOCount, drvAdlinkIo.CardType.PCI_9112, (ushort)pSetup.iSlaveID, eDI, eDO));
                                    break;
                            }
                            clsEnum.enuDi[] DicAI = clsDioCtrl.lstDioDriver[clsDioCtrl.lstDioDriver.Count - 1].GetDiEnum();
                            for (int i = 0; i < DicAI.Length; i++)
                            {
                                clsDioMotion.mLst_AIEnum.Add(DicAI[i]);
                            }
                            clsEnum.enuDo[] DicAO = clsDioCtrl.lstDioDriver[clsDioCtrl.lstDioDriver.Count - 1].GetDoEnum();
                            for (int i = 0; i < DicAO.Length; i++)
                            {
                                clsDioMotion.mLst_AOEnum.Add(DicAO[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            CreateSuccess = false;
                            string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                            formMessageBox.Show(sLog, "[Hardware Initial] Fail.");
                            clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                        }
                    }
                    #endregion


                    #region//Copy Axis Info
                    try
                    {
                        string SaveAxisSettingPath = ucCardSetting.GetSingleton().mCardInfo.sINIPath.Replace("CardInfo.ini", "AxisInfoSetting.ini");
                        Dictionary<clsEnum.enuAxis, clsAxisSetting> JsonData = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<Dictionary<clsEnum.enuAxis, clsAxisSetting>>(SaveAxisSettingPath, Encoding.Unicode);
                        if (JsonData != null)
                        {
                            foreach (clsEnum.enuAxis eAxis in JsonData.Keys)
                            {
                                if (clsDioMotion.mDic_AxisInfo.ContainsKey(eAxis) == true)
                                {
                                    clsClassFunc.Copy(JsonData[eAxis], clsDioMotion.mDic_AxisInfo[eAxis]);
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }
                    #endregion

                    #endregion

                    #region//建立 Devices
                    ucDevices.GetSingleton().HardwareInitial();
                    #endregion


                    if (CreateSuccess == false
                        && strInitialFailMessage != "")
                    {
                    }
                    //CreateSuccess = true;
                }
                catch (Exception ex)
                {
                    CreateSuccess = false;
                    string sLog = "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message;
                    MessageBox.Show(sLog, "[Hardware Initial] Fail.");
                    clsLog.Log(clsEnum.enuLogName.SystemLog, sLog);
                }
            }
            return CreateSuccess;
        }

        static public void HadrwareDestroy()
        {
            ucDevices.GetSingleton().HadrwareDestroy();
        }

        static public bool SetCardParameter()
        {

            bool rValue = false;
            try
            {
                foreach (clsMotionCardSetup pSetup in ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo)
                {
                    try
                    {
                        #region//實際建立
                        switch (pSetup.eMotionCard)
                        {
                            case clsMultiSystem.enuMotionCard.ADVAN_1245:
                                break;
                            case clsMultiSystem.enuMotionCard.GTS_4:
                                break;
                            case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                                #region//ApsMasterTPM
                                if (pSetup.mCartPmt_TPM.Count == 0)
                                {
                                    pSetup.mCartPmt_TPM.Add(new clsMotionCardPmt_TPM());
                                    pSetup.mCartPmt_TPM.Add(new clsMotionCardPmt_TPM());
                                    pSetup.mCartPmt_TPM.Add(new clsMotionCardPmt_TPM());
                                    pSetup.mCartPmt_TPM.Add(new clsMotionCardPmt_TPM());
                                }
                                for (int i = 0; i < 4; i++)
                                {
                                    clsEnum.enuAxis eAxis = (clsEnum.enuAxis)pSetup.eStartAxis + i;
                                    if (Enum.IsDefined(typeof(clsEnum.enuAxis), eAxis) == true)
                                    {
                                        if (i < pSetup.mCartPmt_TPM.Count)
                                        {
                                            clsMotionCardPmt_TPM.SetupAxis(eAxis, pSetup.mCartPmt_TPM[i]);
                                        }
                                    }
                                }
                                #endregion
                                break;
                            case clsMultiSystem.enuMotionCard.L122:
                                break;
                            case clsMultiSystem.enuMotionCard.Card7856:
                                if (pSetup.mCartPmt_7856.Count == 0)
                                {
                                    pSetup.mCartPmt_7856.Add(new clsMotionCardPmt_PCI7856());
                                    pSetup.mCartPmt_7856.Add(new clsMotionCardPmt_PCI7856());
                                    pSetup.mCartPmt_7856.Add(new clsMotionCardPmt_PCI7856());
                                    pSetup.mCartPmt_7856.Add(new clsMotionCardPmt_PCI7856());
                                }
                                for(int i=0;i<4;i++)
                                {
                                    clsEnum.enuAxis eAxis = (clsEnum.enuAxis)pSetup.eStartAxis+i;
                                    if(Enum.IsDefined(typeof(clsEnum.enuAxis),eAxis) == true)
                                    {
                                        if (i < pSetup.mCartPmt_7856.Count)
                                        {
                                            clsMotionCardPmt_PCI7856.SetupAxis(eAxis, pSetup.mCartPmt_7856[i]);
                                        }
                                    }
                                }
                                break;
                            case clsMultiSystem.enuMotionCard.EtherCatMasterAdv:
                                {
                                    int iAxisNum = clsMotionCardSetup.EtherCATMotionSlaveNum(pSetup.eSlaveType);
                                    if (pSetup.mCartPmt_AdvEtherCAT.Count == 0)
                                    {
                                        for (int i = 0; i < iAxisNum; i++)
                                        {
                                            pSetup.mCartPmt_AdvEtherCAT.Add(new clsMotionCardPmt_AdvEtherCAT());
                                        }
                                    }
                                    for (int i = 0; i < iAxisNum; i++)
                                    {
                                        clsEnum.enuAxis eAxis = (clsEnum.enuAxis)pSetup.eStartAxis + i;
                                        if (Enum.IsDefined(typeof(clsEnum.enuAxis), eAxis) == true)
                                        {
                                            if (i < pSetup.mCartPmt_AdvEtherCAT.Count)
                                            {
                                                clsMotionCardPmt_AdvEtherCAT.SetupAxis(eAxis, pSetup.mCartPmt_AdvEtherCAT[i]);
                                            }
                                        }
                                    }
                                }
                                break;
                            case clsMultiSystem.enuMotionCard.Etel:
                                break;
                            case clsMultiSystem.enuMotionCard.Etel4:
                                break;
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }

                }
                if (clsArtSystem.bIsSoftwareSimulate == false)
                {
                    foreach (clsEnum.enuAxis eAxisID in clsDioMotion.mDic_AxisInfo.Keys)
                    {
                        clsMotionCtrl.SetIsCheckINP(eAxisID, clsDioMotion.mDic_AxisInfo[eAxisID].bDisableINP == false);
                        if (clsDioMotion.mDic_AxisInfo[eAxisID].eMotionCard == enuMotionCard.EtherCatMasterAdv)
                        {
                            clsMotionDriverAdv.SetProperty(eAxisID, drvMotionAdvanTech.PropertyID.CFG_AxInpEnable, (uint)(clsDioMotion.mDic_AxisInfo[eAxisID].bDisableINP ? 0 : 1));
                        }
                    }
                }
                rValue = true;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        
        #endregion

        #region //===================== 備份INI =====================

        static private List<string> Lst_ReloadINI_FileName = new List<string>();
        /// <summary> 載入-指定路徑clsArtSystem.strINIPath內如果有對應的INI文件則進行載入 </summary>
        static public List<string> ReloadINI(List<string> lst_INIFile_To_strINIPath)
        {
            if (bIsMultiSystem == true)
            {
                List<string> Files = System.IO.Directory.GetFiles(strSystemINIPath).ToList<string>();
                foreach (string sFileName in lst_INIFile_To_strINIPath)
                {
                    string sFilePath = ArtSystem.clsArtSystem.strINIPath + "\\" + sFileName;
                    string sSource = clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "") + "\\" + sFileName;
                    if (System.IO.File.Exists(sFilePath) == false
                        && System.IO.File.Exists(sSource) == true)
                    {
                        System.IO.File.Copy(sSource, sFilePath, true);
                    }
                }
                foreach (string sFilePath in Files)
                {
                    string sFileName = System.IO.Path.GetFileName(sFilePath);
                    {
                        try
                        {
                            string sTarget = clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "") + sFileName;
                            System.IO.File.Copy(sFilePath, sTarget, true);
                            if (lst_INIFile_To_strINIPath.Contains(sFileName) == true
                                && Lst_ReloadINI_FileName.Contains(sFileName) == false)
                            {
                                Lst_ReloadINI_FileName.Add(sFileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                }
            }
            return Lst_ReloadINI_FileName;
        }

        static public List<string> BackupINI(List<string> lst_INIFile_To_strINIPath)
        {
            if (bIsMultiSystem == true)
            {
                List<string> Files = System.IO.Directory.GetFiles(clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "")).ToList<string>();
                foreach (string sFilePath in Files)
                {
                    string sFileName = System.IO.Path.GetFileName(sFilePath);
                    //if (Lst_ReloadINI_FileName.Contains(sFileName) == true)
                    {
                        if (System.IO.File.Exists(strSystemINIPath + "\\" + sFileName) == true
                            && System.IO.File.Exists(sFilePath) == true)
                        {
                            try
                            {
                                if (System.IO.File.GetLastWriteTime(strSystemINIPath + "\\" + sFileName) != System.IO.File.GetLastWriteTime(sFilePath))
                                {
                                    if (System.IO.File.GetLastWriteTime(strSystemINIPath + "\\" + sFileName) < System.IO.File.GetLastWriteTime(sFilePath))
                                    {
                                        System.IO.File.Copy(sFilePath, strSystemINIPath + "\\" + sFileName, true);
                                        clsLog.Log(clsCmData.enuLogType.SystemLog, "[Backup INI] \"" + sFilePath + "\" Copy To \"" + strSystemINIPath + "\\" + sFileName + "\"");
                                    }
                                    else
                                    {
                                        System.IO.File.Copy(strSystemINIPath + "\\" + sFileName, sFilePath, true);
                                        clsLog.Log(clsCmData.enuLogType.SystemLog, "[Backup INI] \"" + strSystemINIPath + "\\" + sFileName + "\"" + "\" Copy To \"" + sFilePath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                clsArtSystem.CatchLog(ex);
                            }
                        }
                    }
                }
            }
            return Lst_ReloadINI_FileName;
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 儲存任何多載功能都需要重新啟動軟體 </summary>
        static public void SetSettingChangeFlag()
        {
            bIsMultiSystem_SettingChanged = true;
        }

        /// <summary> 取得隱藏的Axis </summary>
        static public List<clsEnum.enuAxis> GetInvisibleAxis()
        {
            int iAxisCount = 0;
            foreach (clsMotionDriver mDriver in clsMotionCtrl.lstMotionDriver)
            {
                iAxisCount += mDriver.dctAxisLib.Count;
            }
            List<clsEnum.enuAxis> mLstAxis = new List<clsEnum.enuAxis>();
            foreach (clsEnum.enuAxis eAxis in Enum.GetValues(typeof(clsEnum.enuAxis)))
            {  mLstAxis.Add(eAxis); }
            if (iAxisCount >= mLstAxis.Count)
            {
                mLstAxis.Clear();
            }
            else
            {
                mLstAxis.RemoveRange(0, iAxisCount);
            }
            return mLstAxis;
        }

        /// <summary> 自動搜尋不同版本使用的DLL </summary>
        static public void ReloadDll()
        {
            try
            {
                GetDLLVersion();

                string sDll = System.IO.Directory.GetCurrentDirectory() + "\\DLLVersion";
                #region//Copy Pylon DLL
                if (iDLL_PylonVersion == 5
                    || iDLL_PylonVersion == 4.2)
                {
                    string sPath_Source = sDll + "\\Pylon" + iDLL_PylonVersion;
                    string sReplaceName = "\\DLLVersion"+"\\Pylon" + iDLL_PylonVersion;
                    if (System.IO.Directory.Exists(sPath_Source) == true)
                    {
                        string[] DllFiles = System.IO.Directory.GetFiles(sPath_Source);
                        for (int i = 0; i < DllFiles.Length; i++)
                        {
                            if (System.IO.File.Exists(DllFiles[i].Replace(sReplaceName, "")) == true)
                            {
                                System.IO.File.Copy(DllFiles[i], DllFiles[i].Replace(sReplaceName, ""), true);
                            }
                        }
                    }
                }
                #endregion
                #region//Copy EVision DLL
                if (iDLL_EvisionVersion == 9183
                    || iDLL_EvisionVersion == 7427)
                {
                    string sPath_Source = sDll + "\\EVision" + iDLL_EvisionVersion;
                    string sReplaceName = "\\DLLVersion" + "\\EVision" + iDLL_EvisionVersion;
                    if (System.IO.Directory.Exists(sPath_Source) == true)
                    {
                        string[] DllFiles = System.IO.Directory.GetFiles(sPath_Source);
                        for (int i = 0; i < DllFiles.Length; i++)
                        {
                            if (System.IO.File.Exists(DllFiles[i].Replace(sReplaceName, "")) == true)
                            {
                                System.IO.File.Copy(DllFiles[i], DllFiles[i].Replace(sReplaceName, ""), true);
                            }
                        }
                    }
                }
                #endregion
                #region//Copy MNet DLL (TPM/L122)
                {
                    string sFileName = bTPM? "MNet_TPM" : "MNet_L122";
                    string sPath_Source = sDll + "\\" + sFileName;
                    string sReplaceName = "\\DLLVersion" + "\\" + sFileName;
                    if (System.IO.Directory.Exists(sPath_Source) == true)
                    {
                        string[] DllFiles = System.IO.Directory.GetFiles(sPath_Source);
                        for (int i = 0; i < DllFiles.Length; i++)
                        {
                            if (System.IO.File.Exists(DllFiles[i].Replace(sReplaceName, "")) == true)
                            {
                                System.IO.File.Copy(DllFiles[i], DllFiles[i].Replace(sReplaceName, ""), true);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region //===================== private 函式設置 =====================

        static private clsEnum.enuDi? ConvertToDI(int iDI)
        {
            clsEnum.enuDi? rValue = null;
            rValue = (clsEnum.enuDi)Enum.ToObject(typeof(clsEnum.enuDi), iDI);
            if (Enum.IsDefined(typeof(clsEnum.enuDi), rValue) == false)
            { rValue = null;  }
            return rValue;
        }
        static private clsEnum.enuDo? ConvertToDO(int iDO)
        {
            clsEnum.enuDo? rValue = null;
            rValue = (clsEnum.enuDo)Enum.ToObject(typeof(clsEnum.enuDo), iDO);
            if (Enum.IsDefined(typeof(clsEnum.enuDo), rValue) == false)
            { rValue = null; }
            return rValue;
        }
        static public clsEnum.enuAxis? SetAxisInfo(ref clsMotionCardSetup p_MotionCardSetup, ref Dictionary<clsEnum.enuAxis, clsAxisSetting> p_DicAxisSetting)
        {
            clsEnum.enuAxis? eAxis = null;
            try
            {
                int iStartAxisID = p_DicAxisSetting.Count;
                eAxis = (clsEnum.enuAxis)Enum.ToObject(typeof(clsEnum.enuAxis), iStartAxisID);
                int AxisNum = p_MotionCardSetup.GetAxisNum();
                for (int i = 0; i < AxisNum; i++)
                {
                    if (i >= p_MotionCardSetup.mLst_AxisSetting.Count)
                    {
                        p_MotionCardSetup.mLst_AxisSetting.Add(new clsAxisSetting());
                    }
                    clsEnum.enuAxis eAxisID = (clsEnum.enuAxis)Enum.ToObject(typeof(clsEnum.enuAxis), iStartAxisID + i);
                    if (Enum.IsDefined(typeof(clsEnum.enuAxis), eAxisID) == true)
                    {
                        if (p_DicAxisSetting.ContainsKey(eAxisID) == false)
                        {
                            p_DicAxisSetting.Add(eAxisID, new clsAxisSetting(eAxisID, p_MotionCardSetup.eMotionCard
                                , p_MotionCardSetup.mLst_AxisSetting[i], clsArtSystem.bIsSoftwareSimulate || p_MotionCardSetup.bSimulate));
                        }
                    }
                    if (i < p_MotionCardSetup.mLst_AxisSetting.Count)
                    {
                        p_MotionCardSetup.mLst_AxisSetting[i] = p_DicAxisSetting[eAxisID];
                        if (p_MotionCardSetup.eMotionCard == enuMotionCard.YaskawaMP3000)
                        {
                            p_MotionCardSetup.mLst_AxisSetting[i].bABSEncoder = true;
                        }
                    }
                    else
                    { p_MotionCardSetup.mLst_AxisSetting.Add(p_DicAxisSetting[eAxisID]); }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return eAxis;
        }

        static private double iDLL_PylonVersion = 4;
        static private double iDLL_EvisionVersion = 9183;
        static private bool bTPM = false;
        static private void GetDLLVersion()
        {
            try
            {
                //開啟註冊列表解除安裝選項
                //SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
                Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                if (Key != null)//如果系統禁止訪問則返回null
                {
                    foreach (String SubKeyName in Key.GetSubKeyNames())
                    {
                        //開啟對應的軟體名稱
                        Microsoft.Win32.RegistryKey SubKey = Key.OpenSubKey(SubKeyName);
                        if (SubKey != null)
                        {
                            String SoftwareName = SubKey.GetValue("DisplayName", "Nothing").ToString();
                            //String Cop = SubKey.GetValue("", "Nothing").ToString();
                            //如果沒有取到，則不存入動態陣列
                            if (SoftwareName != "Nothing")
                            {
                                if (SoftwareName.Contains("pylon 5") == true)
                                {
                                    iDLL_PylonVersion = 5;
                                }
                                else if (SoftwareName.Contains("pylon 4") == true)
                                {
                                    iDLL_PylonVersion = 4.2;
                                }
                                if (SoftwareName.Contains("Euresys Open eVision") == true
                                    && SoftwareName.Contains("-bit Libraries") == true)
                                {
                                    if (SoftwareName.Contains(".") == true)
                                    {
                                        string sVersion = SoftwareName.Split('.')[SoftwareName.Split('.').Length - 1];
                                        iDLL_EvisionVersion = int.Parse(sVersion);
                                    }

                                }
                            }
                        }
                    }
                }
                bTPM = false;
                for (int i = 0; i < ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo.Count; i++)
                {
                    if (ucCardSetting.GetSingleton().mCardInfo.mLstMotionCardInfo[i].eMotionCard == clsMultiSystem.enuMotionCard.ApsMasterTPM)
                    {
                        bTPM = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion
    }
}

