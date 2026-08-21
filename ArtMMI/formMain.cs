using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtSystem;
using ArtSystem.MultiSystem;
using ArtEQ;

namespace ArtMMI
{
    public partial class formMain : clsEditFunc
    {
        /// <summary> 建構式 </summary>
        public formMain()
        {

            InitializeComponent();

            //以下功能是為了更新程式時不會影像舊程式的原有設定，舉例：原本開啟的參數路徑，馬達速度參數等。
            //所以對應的參數檔案會被綁頂在D:\\Parameter\\INI內。
            clsArtSystem.lst_INIFile_To_strINIPath.Clear();
            clsArtSystem.lst_INIFile_To_strINIPath.Add("artEqParameter.ini");//...\\Bin\\Debug\\INI\\artEqParameter.ini (紀錄當前ucParameter的檔案存放路徑)
            //clsArtSystem.lst_INIFile_To_strINIPath.Add("artSystem.ini");
            //clsArtSystem.lst_INIFile_To_strINIPath.Add("AxisSetting.ini");
            //clsArtSystem.lst_INIFile_To_strINIPath.Add("MachineModel.ini");

            clsMultiSystem.InitialMultiSystem(this, "ArtMMI-空專案");//賦予設備名稱
            clsAlarmCodeBuilder.AlarmCodeBuilder();//AlarmCodeBuilder - 介與clsEnum.enuAlarm內的int異常代碼，將未定義的AlarmCode自動生成到...\\Bin\\Debug\\INI\\AlarmList.ini內。

            ucArtMain_Design.GetSingleton()._evt_ArtMainFunc += new ucArtMain_Design.evt_ArtMainFunc(formMain__evt_ArtMainFunc);
            ucArtMain_Design.GetSingleton()._Input_ucTitle(ucArtMain_Design.enuDesign.PC, ucTitle.GetSingleton());
            ucArtMain_Design.GetSingleton()._Input_ucHotkey(ucArtMain_Design.enuDesign.PC, ucHotkeyFunc.GetSingleton());
            ucArtMain_Design.GetSingleton()._Input_ucTitle(ucArtMain_Design.enuDesign.Panel, ucTitle_Panel.GetSingleton());
            ucArtMain_Design.GetSingleton()._Input_ucHotkey(ucArtMain_Design.enuDesign.Panel, ucHotkeyFunc_Panel.GetSingleton());
            this.Load += new EventHandler(ucArtMain_Design.GetSingleton()._formMain_Load);
        }

        static public void HandleUnhandledException(Object o)
        {
            Exception ex = o as Exception;

            if (ex == null)
                return;

            try
            {
                if (Application.OpenForms.OfType<Form>().Any())
                {
                    Application.OpenForms.OfType<Form>().First().Invoke(new Action(() =>
                    {
                        Application.OpenForms.OfType<Form>().First().Enabled = false;

                        ArtSystem.UnhandledExceptionMessageBox.Show(ex);
                    }));
                }
            }
            finally
            {
                Environment.Exit(Environment.ExitCode);
            }
        }
    }
}
