using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
using ArtCommonLib;
using ArtControlLib;

namespace ArtSystem.Login
{
    public partial class fromAutoLogout : Form
    {
        private int iRemainSecond = 3;
        static private fromAutoLogout m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public fromAutoLogout GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new fromAutoLogout();
            }
            return m_Singleton;
        }
        public fromAutoLogout()
        {
            InitializeComponent();
        }

        public void _Show(int iRemainSec = 10)
        {
            if (this.Visible == false)
            {
                StartPosition = FormStartPosition.CenterScreen;
                iRemainSecond = iRemainSec;
                timer1.Interval = 1000;
                timer1.Enabled = true;
                UpdateText();
                clsLanguage.SetLanguateToControls(this);
            }
            this.Show();
            this.BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            iRemainSecond--;
            UpdateText();
            if (iRemainSecond < 0)
            {
                this.timer1.Enabled = false;
                this.Hide();
            }
        }
        private void UpdateText()
        {
            label1.Text = clsLanguage.GetTranslation("Auto Logout In") + " " + iRemainSecond + " " + clsLanguage.GetTranslation("(sec)");
        }

    }
}
