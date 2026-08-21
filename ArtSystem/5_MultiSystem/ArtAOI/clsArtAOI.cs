using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using Euresys.Open_eVision_1_2;

namespace ArtSystem.MultiSystem
{
    public class clsArtAOI
    {
        public string sINIPath
        {
            get;
            private set;
        }
        public bool bActived = false;
        public ArtGrab.clsArtGrabCtrl mGrab = new ArtGrab.clsArtGrabCtrl();

        /// <summary> 儲存ArtAOI </summary>
        public void Save(string sPath)
        {
            try
            {
                string sDirectory = System.IO.Path.GetDirectoryName(sPath);
                if (System.IO.Directory.Exists(sDirectory) == false)
                {
                    System.IO.Directory.CreateDirectory(sDirectory);
                }
                if (System.IO.Directory.Exists(sDirectory) == true)
                {
                    sINIPath = sPath;
                    mGrab.SaveGrabParameter(sPath, sPath);
                    Load(sINIPath);
                }
                else
                {
                    formMessageBox.Show("Create Directory Fail.");
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        /// <summary> 寫入CardInfo </summary>
        public void Load(string sPath)
        {
            try
            {
                if (System.IO.File.Exists(sPath) == true)
                {
                    sINIPath = sPath;
                    mGrab.SaveGrabParameter(sINIPath, sINIPath);
                }
                else
                {
                    sINIPath = sPath;
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

    }
}
