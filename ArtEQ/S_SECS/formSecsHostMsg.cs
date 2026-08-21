using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArtEQ
{
    public partial class formSecsHostMsg : Form
    {
        public delegate void dlgShowMessage(string text);
        static formSecsHostMsg m_Singleton;

        private formSecsHostMsg()
        {
            InitializeComponent();
            this.Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width / 2) - this.Size.Width / 2, (int)(Screen.PrimaryScreen.Bounds.Height / 2) - this.Size.Height / 2);
        }

        public static formSecsHostMsg GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new formSecsHostMsg();
            }
            return m_Singleton;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void ShowMessage(string msg)
        {
            try
            {
                if (txtMsg.InvokeRequired)
                {
                    dlgShowMessage t = new dlgShowMessage(ShowMessage);
                    txtMsg.Invoke(t, msg);
                }
                else
                {
                    this.TopMost = true;
                    this.Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width / 2) - this.Size.Width / 2, (int)(Screen.PrimaryScreen.Bounds.Height / 2) - this.Size.Height / 2);
                    this.Show();
                    txtMsg.Text = "";
                    txtMsg.Text = msg;
                }
            }
            catch
            {

            }

        }

    }
}
