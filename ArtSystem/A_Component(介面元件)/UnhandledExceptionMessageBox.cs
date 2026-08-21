using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.IO;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem
{
    public class UnhandledExceptionMessageBox : Form
    {
        public static void Log(Exception ex)
        {
            var errMsg = ExceptionMessage.Build(ex);
            clsLog.Log(clsArtSystem.g_strCatchLogName, errMsg);
        }
        public static DialogResult Show(Exception ex)
        {
            using (UnhandledExceptionMessageBox box = new UnhandledExceptionMessageBox(ex))
            {
                return box.ShowDialog();
            }
        }

        private UnhandledExceptionMessageBox(Exception ex)
            : base()
        {
            InitializeComponent(ex);
        }

        private Label _messageLabel;
        private Button _okButton;
        private PictureBox _iconPictureBox;

        private void InitializeComponent(Exception ex)
        {
            var errMsg = ExceptionMessage.Build(ex);
            clsLog.Log(clsArtSystem.g_strCatchLogName, errMsg);
            Clipboard.SetData(DataFormats.Text, errMsg);
            File.AppendAllText("UnhandledException.txt", errMsg);

            Text = "Critical Error";
            StartPosition = FormStartPosition.CenterScreen;
            SizeGripStyle = SizeGripStyle.Hide;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            TopMost = true;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;

            _iconPictureBox = new PictureBox();
            _iconPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            _iconPictureBox.Image = SystemIcons.Error.ToBitmap();
            _iconPictureBox.Location = new Point(10, 20);
            Controls.Add(_iconPictureBox);

            _messageLabel = new Label();
            _messageLabel.Text = errMsg;
            _messageLabel.AutoSize = true;
            _messageLabel.Location = new Point(_iconPictureBox.Right + 10, 20);
            this.Controls.Add(_messageLabel);

            _okButton = new Button();
            _okButton.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase("Ok");
            _okButton.AutoSize = true;
            _okButton.Click += (sender, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
            this.Controls.Add(_okButton);

            this.ClientSize = new Size(Math.Max(_messageLabel.Right + 10, _okButton.Width + 20),
                Math.Max(_iconPictureBox.Bottom, _messageLabel.Bottom) + _okButton.Height + 30);

            _okButton.Location = new Point((this.ClientSize.Width - _okButton.Width) - 10, _messageLabel.Bottom + 10);
        }


        private class ExceptionMessage
        {
            public static string Build(Exception ex)
            {
                return new ExceptionMessage(ex).ToString();
            }

            private ExceptionMessage(Exception ex)
            {
                BuildInternal(ex);
            }

            private List<string> _lines = new List<string>();

            private void AppendLine(string text = "", int indent = 0)
            {
                if (text == null)
                    text = "";

                var lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length == 0)
                {
                    _lines.Add("");
                }
                else
                {
                    string tab = "";

                    for (int i = 0; i < indent; i++)
                    {
                        tab += "        ";
                    }

                    foreach (var line in lines)
                    {
                        _lines.Add(line.Insert(0, tab));
                    }
                }
            }

            private void BuildInternal(Exception ex)
            {
                AppendLine("Sorry! program was terminated");
                AppendLine();
                AppendLine("[" + (DateTime.Now).ToLongTimeString() + "] ");
                AppendLine("<Unhandled exception>");

                BuildRecursive(ex, 0);
            }

            private void BuildRecursive(Exception ex, int level)
            {
                AppendLine(ex.Message, level);
                AppendLine(ex.TargetSite.ToString(), level);

                if (ex.InnerException != null)
                {
                    BuildRecursive(ex.InnerException, level + 1);
                }

                AppendLine(ex.StackTrace, level);
            }

            public override string ToString()
            {
                var stringBuilder = new StringBuilder();

                foreach (var line in _lines)
                {
                    stringBuilder.AppendLine(line);
                }

                return stringBuilder.ToString();
            }
        }
    }
}
