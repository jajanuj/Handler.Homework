using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
namespace ArtTeach
{
    public partial class ucEditUITable : UserControl
    {
        public event System.EventHandler m_ExitEvent = null;
        private string strCopyDio_MasterName = "";
        private string strCopyDio_SlaveName = "";
        private string strCurrentMasterName = "";
        public List<clsProFile.clsMaster> m_lisMaster = null;
        private clsProFile.clsSlave m_clsSlave;
        /// <summary> 軸 Title </summary>
        private List<string> dgvJogAndTeachTitle = new List<string>();
        /// <summary> DIO Title </summary>
        private List<string> dgvDIOTitle = new List<string>();
        public ucEditUITable()
        {
            InitializeComponent();
            try
            {
                dgvJogAndTeach.Rows.Clear();
                dgvJogAndTeach.Columns.Clear();
                dgvDIO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvJogAndTeach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                for (int i = 0; i < 4; i++)
                {
                    dgvJogAndTeach.Columns.Add("JogTeach_" + (i + 1), "JogTeach_" + (i + 1));
                    dgvJogAndTeach.Columns[i].Width = 115;
                    dgvJogAndTeach.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                for (int i = 0; i < 8; i++)
                {
                    dgvDIO.Columns.Add("DIOTeach_" + (i + 1), "DIOTeach_" + (i + 1));
                    dgvDIO.Columns[i].Width = 115;
                    dgvDIO.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dgvDIO_AddTitle(new clsDataDIO());
                dgvDIO.RowHeadersWidth = 200;
                dgvJogAnd_AddTitle(new clsDataJogTeach());
                dgvJogAndTeach.RowHeadersWidth = 200;
            }
            catch
            {
            }
        }
        /// <summary> 更新DataTable </summary>
        public void UpdateDataTable(clsProFile.clsSlave p_clsSlave, string MasterName)
        {
            if (this.Visible == false)
            {
                return;
            }
            strCurrentMasterName = MasterName;
            m_clsSlave = p_clsSlave;
            dgvJogAnd_UpdateValue(0, m_clsSlave._lisJogAndTeachInfo[0]);
            dgvJogAnd_UpdateValue(1, m_clsSlave._lisJogAndTeachInfo[1]);
            dgvJogAnd_UpdateValue(2, m_clsSlave._lisJogAndTeachInfo[2]);
            dgvJogAnd_UpdateValue(3, m_clsSlave._lisJogAndTeachInfo[3]);
            //dgvJogAnd_UpdateValue(4, m_clsSlave._lisJogAndTeachInfo[4]) ;
            //dgvJogAnd_UpdateValue(5, m_clsSlave._lisJogAndTeachInfo[5]) ;
            dgvDIO_UpdateValue(0, m_clsSlave._lisDIOInfo[0]);
            dgvDIO_UpdateValue(1, m_clsSlave._lisDIOInfo[1]);
            dgvDIO_UpdateValue(2, m_clsSlave._lisDIOInfo[2]);
            dgvDIO_UpdateValue(3, m_clsSlave._lisDIOInfo[3]);
            dgvDIO_UpdateValue(4, m_clsSlave._lisDIOInfo[4]);
            dgvDIO_UpdateValue(5, m_clsSlave._lisDIOInfo[5]);
            dgvDIO_UpdateValue(6, m_clsSlave._lisDIOInfo[6]);
            dgvDIO_UpdateValue(7, m_clsSlave._lisDIOInfo[7]);
        }
        /// <summary> 更新軸 </summary>
        private void dgvJogAnd_UpdateValue(int ColumnIndex, object p_Object)
        {
            var Templist = p_Object.GetType().GetFields().ToList();
            for (int i = 0; i < Templist.Count; i++)
            {
                try
                {
                    dgvJogAndTeach[ColumnIndex, i].Value = Templist[i].GetValue(p_Object).ToString();
                }
                catch
                {
                    dgvJogAndTeach[ColumnIndex, i].Value = "";
                }
            }
        }
        /// <summary> 更新DIO </summary>
        private void dgvDIO_UpdateValue(int ColumnIndex, object p_Object)
        {
            var Templist = p_Object.GetType().GetFields().ToList();
            for (int i = 0; i < Templist.Count; i++)
            {
                try
                {
                    dgvDIO[ColumnIndex, i].Value = Templist[i].GetValue(p_Object).ToString();
                }
                catch
                {
                    dgvDIO[ColumnIndex, i].Value = "";
                }
            }
        }

        /// <summary> 軸添加 </summary>
        private void dgvJogAnd_AddTitle(object p_Object)
        {
            var Templist = p_Object.GetType().GetFields().ToList();
            for (int i = 0; i < Templist.Count; i++)
            {
                try
                {
                    string Temp = p_Object.GetType().GetField(Templist[i].Name).Name;
                    dgvJogAndTeach.Rows.Add();
                    dgvJogAndTeach.Rows[dgvJogAndTeach.Rows.Count - 2].HeaderCell.Value = ConvertNameJT(Temp);
                    dgvJogAndTeachTitle.Add(Temp);
                }
                catch
                {
                }
            }
        }

        /// <summary> DIO添加 </summary>
        private void dgvDIO_AddTitle(object p_Object)
        {
            var Templist = p_Object.GetType().GetFields().ToList();
            for (int i = 0; i < Templist.Count; i++)
            {
                try
                {
                    string Temp = Templist[i].Name;
                    dgvDIO.Rows.Add();
                    dgvDIO.Rows[dgvDIO.Rows.Count - 2].HeaderCell.Value = ConvertNameDIO(Temp);
                    dgvDIOTitle.Add(Temp);
                }
                catch
                {
                }
            }
        }

        private string ConvertNameJT(string Input)
        {
            try
            {
                clsDataJogTeach Temp = new clsDataJogTeach();
                var DescriptionName = Temp.GetType().GetField(Input).GetCustomAttributes(typeof(DescriptionAttribute), true)
                                  .FirstOrDefault() as DescriptionAttribute;
                if (DescriptionName != null)
                {
                    return DescriptionName.Description;
                }
                else
                {
                    return Input;
                }
            }
            catch
            {
                return Input;
            }
        }
        private string ConvertNameDIO(string Input)
        {
            try
            {
                clsDataDIO Temp = new clsDataDIO();
                var DescriptionName = Temp.GetType().GetField(Input).GetCustomAttributes(typeof(DescriptionAttribute), true)
                                  .FirstOrDefault() as DescriptionAttribute;
                if (DescriptionName != null)
                {
                    return DescriptionName.Description;
                }
                else
                {
                    return Input;
                }
            }
            catch
            {
                return Input;
            }
        }

        /// <summary> 選擇對應軸、IO、位置、點位、顯示模式 </summary>
        private DialogResult InputAxis(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            //Label labe2 = new Label() ;
            ComboBox textBox1 = new ComboBox();
            //TextBox textBox2 = new TextBox() ;
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            //labe2.Text = "Mark : ";
            //textBox1.Text = value;
            int SelectedIndex = 1;
            textBox1.Items.Add("");
            foreach (clsEnum.enuAxis AxisName in Enum.GetValues(typeof(clsEnum.enuAxis)))
            {
                textBox1.Items.Add(AxisName);
                if (AxisName.ToString() == value)
                {
                    textBox1.SelectedIndex = SelectedIndex;
                }
                SelectedIndex++;
            }
            textBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //textBox2.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(14, 20, 372, 13);
            textBox1.SetBounds(12, 36, 372, 20);

            //labe2.SetBounds(14, 70, 372, 13) ;
            //textBox2.SetBounds(12, 86, 372, 20) ;

            buttonOk.SetBounds(228, 70, 75, 23);
            buttonCancel.SetBounds(309, 70, 75, 23);

            label.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;

            //labe2.AutoSize = true;
            //textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox1, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox1.Text;
            //strMark = textBox2.Text;
            return dialogResult;
        }
        private DialogResult InputDI(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            //Label labe2 = new Label() ;
            ComboBox textBox1 = new ComboBox();
            //TextBox textBox2 = new TextBox() ;
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            //labe2.Text = "Mark : ";
            //textBox1.Text = value;
            int SelectedIndex = 1;
            textBox1.Items.Add("");
            foreach (clsEnum.enuDi DiName in Enum.GetValues(typeof(clsEnum.enuDi)))
            {
                textBox1.Items.Add(DiName);
                if (DiName.ToString() == value)
                {
                    textBox1.SelectedIndex = SelectedIndex;
                }
                SelectedIndex++;
            }
            textBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //textBox2.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(14, 20, 372, 13);
            textBox1.SetBounds(12, 36, 372, 20);

            //labe2.SetBounds(14, 70, 372, 13) ;
            //textBox2.SetBounds(12, 86, 372, 20) ;

            buttonOk.SetBounds(228, 70, 75, 23);
            buttonCancel.SetBounds(309, 70, 75, 23);

            label.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;

            //labe2.AutoSize = true;
            //textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox1, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox1.Text;
            //strMark = textBox2.Text;
            return dialogResult;
        }
        private DialogResult InputDO(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            //Label labe2 = new Label() ;
            ComboBox textBox1 = new ComboBox();
            //TextBox textBox2 = new TextBox() ;
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            //labe2.Text = "Mark : ";
            //textBox1.Text = value;
            int SelectedIndex = 1;
            textBox1.Items.Add("");
            foreach (clsEnum.enuDo DoName in Enum.GetValues(typeof(clsEnum.enuDo)))
            {
                textBox1.Items.Add(DoName);
                if (DoName.ToString() == value)
                {
                    textBox1.SelectedIndex = SelectedIndex;
                }
                SelectedIndex++;
            }
            textBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //textBox2.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(14, 20, 372, 13);
            textBox1.SetBounds(12, 36, 372, 20);

            //labe2.SetBounds(14, 70, 372, 13) ;
            //textBox2.SetBounds(12, 86, 372, 20) ;

            buttonOk.SetBounds(228, 70, 75, 23);
            buttonCancel.SetBounds(309, 70, 75, 23);

            label.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;

            //labe2.AutoSize = true;
            //textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox1, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox1.Text;
            //strMark = textBox2.Text;
            return dialogResult;
        }
        private DialogResult InputPosition(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            //Label labe2 = new Label() ;
            ComboBox textBox1 = new ComboBox();
            //TextBox textBox2 = new TextBox() ;
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            //labe2.Text = "Mark : ";
            //textBox1.Text = value;
            int SelectedIndex = 1;
            textBox1.Items.Add("");
            foreach (clsEnum.enuPosName PosName in Enum.GetValues(typeof(clsEnum.enuPosName)))
            {
                textBox1.Items.Add(PosName);
                if (PosName.ToString() == value)
                {
                    textBox1.SelectedIndex = SelectedIndex;
                }
                SelectedIndex++;
            }
            textBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //textBox2.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(14, 20, 372, 13);
            textBox1.SetBounds(12, 36, 372, 20);

            //labe2.SetBounds(14, 70, 372, 13) ;
            //textBox2.SetBounds(12, 86, 372, 20) ;

            buttonOk.SetBounds(228, 70, 75, 23);
            buttonCancel.SetBounds(309, 70, 75, 23);

            label.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;

            //labe2.AutoSize = true;
            //textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox1, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox1.Text;
            //strMark = textBox2.Text;
            return dialogResult;
        }
        private DialogResult InputPointInt(string title, string promptText, ref int value1, ref int value2)
        {
            Form form = new Form();
            Label label = new Label();
            Label label1 = new Label();
            Label label2 = new Label();
            //Label labe2 = new Label() ;
            NumericUpDown textBox1 = new NumericUpDown();
            NumericUpDown textBox2 = new NumericUpDown();
            //TextBox textBox2 = new TextBox() ;
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            int SelectedIndex = 0;
            textBox1.Maximum = 1000;
            textBox1.Minimum = -1000;
            textBox2.Maximum = 1000;
            textBox2.Minimum = -1000;
            textBox1.Value = value1;
            textBox2.Value = value2;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(35, 20, 372, 13);
            label1.SetBounds(12, 38, 20, 20);
            label2.SetBounds(12, 62, 20, 20);
            label1.Text = "X:";
            label2.Text = "Y:";
            textBox1.SetBounds(35, 36, 350, 20);
            textBox2.SetBounds(35, 60, 350, 20);

            //labe2.SetBounds(14, 70, 372, 13) ;
            //textBox2.SetBounds(12, 86, 372, 20) ;

            buttonOk.SetBounds(228, 90, 75, 23);
            buttonCancel.SetBounds(309, 90, 75, 23);

            label.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;
            textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            //labe2.AutoSize = true;
            //textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 130);
            form.Controls.AddRange(new Control[] { label, textBox1, textBox2, label1, label2, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value1 = (int)textBox1.Value;
            value2 = (int)textBox2.Value;
            //strMark = textBox2.Text;
            return dialogResult;
        }
        private DialogResult InputDisplayMode(string title, string promptText, ref clsDataJogTeach.enuDisplayMode value)
        {
            Form form = new Form();
            Label label = new Label();
            //Label labe2 = new Label() ;
            ComboBox textBox1 = new ComboBox();
            //TextBox textBox2 = new TextBox() ;
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            //labe2.Text = "Mark : ";
            //textBox1.Text = value;
            int SelectedIndex = 0;
            foreach (clsDataJogTeach.enuDisplayMode DisplayMode in Enum.GetValues(typeof(clsDataJogTeach.enuDisplayMode)))
            {
                textBox1.Items.Add(DisplayMode);
                if (DisplayMode == value)
                {
                    textBox1.SelectedIndex = SelectedIndex;
                }
                SelectedIndex++;
            }
            textBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //textBox2.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(14, 20, 372, 13);
            textBox1.SetBounds(12, 36, 372, 20);

            //labe2.SetBounds(14, 70, 372, 13) ;
            //textBox2.SetBounds(12, 86, 372, 20) ;

            buttonOk.SetBounds(228, 70, 75, 23);
            buttonCancel.SetBounds(309, 70, 75, 23);

            label.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;

            //labe2.AutoSize = true;
            //textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox1, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            foreach (clsDataJogTeach.enuDisplayMode DisplayMode in Enum.GetValues(typeof(clsDataJogTeach.enuDisplayMode)))
            {
                if (DisplayMode.ToString() == textBox1.Text)
                {
                    value = DisplayMode;
                }
            }
            //value = textBox1.Text;
            //strMark = textBox2.Text;
            return dialogResult;
        }


        private void dgvJogAndTeach_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                string RowTitle = dgvJogAndTeachTitle[e.RowIndex];
                clsDataJogTeach m_clsDatatJogTeach = m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex];
                switch (RowTitle)
                {
                    case "_AxisTitle_String":
                    case "_PosName_String":
                    case "_JogAText_String":
                    case "_JogBText_String":
                    case "_AxisUnit_String":
                        m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value);
                        break;
                    case "_TeachSpeed":
                        try
                        {
                            int iValue = Convert.ToInt32(dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value);
                            if (iValue <= 20 && iValue > 0)
                            {
                                m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, iValue);
                                dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = iValue;
                            }
                            else
                            {
                                dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                                MessageBox.Show("Value between 1~20 only.");
                            }
                        }
                        catch
                        {
                            dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                        }
                        break;
                    case "_SafePosition":
                        try
                        {
                            double dValue = Convert.ToDouble(dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value);
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, dValue);
                            dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = dValue;
                        }
                        catch
                        {
                            dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                        }
                        break;
                    default:
                        m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value);
                        break;
                }
            }
            catch
            {
            }
        }
        private void dgvJogAndTeach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
                string RowTitle = dgvJogAndTeachTitle[e.RowIndex];
                clsDataJogTeach m_clsDatatJogTeach = m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex];
                var Sourcelist = m_clsDatatJogTeach.GetType().GetFields().ToList();
                string strValue = "";
                int iValue1 = 0;
                int iValue2 = 0;
                switch (RowTitle)
                {
                    case "_JogMoveDir":
                        dgvJogAndTeach.ReadOnly = true;
                        if (dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value.ToString() == ArtControlLib.enuMoveDir.Positive.ToString())
                        {
                            dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = ArtControlLib.enuMoveDir.Negative.ToString();
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, ArtControlLib.enuMoveDir.Negative);
                        }
                        else
                        {
                            dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = ArtControlLib.enuMoveDir.Positive.ToString();
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, ArtControlLib.enuMoveDir.Positive);
                        }
                        break;
                    case "_JobAImg_String":
                    case "_JobBImg_String":
                        {
                            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                            {
                                dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = openFileDialog1.FileName;
                                m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value);
                            }
                        }
                        break;
                    case "_enuDisplayMode":
                        {
                            dgvJogAndTeach.ReadOnly = true;
                            clsDataJogTeach.enuDisplayMode enuValue = m_clsDatatJogTeach._enuDisplayMode;
                            if (InputDisplayMode("Select Display Mode", "Display Mode", ref enuValue) == DialogResult.OK)
                            {
                                m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, enuValue);
                                dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                            }
                        }
                        break;
                    case "_AxisEnum_String":
                        dgvJogAndTeach.ReadOnly = true;
                        strValue = m_clsDatatJogTeach._AxisEnum_String;
                        if (InputAxis("Select Axis", "Axis", ref strValue) == DialogResult.OK)
                        {
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, strValue);
                            string Tem = m_clsDatatJogTeach.GetType().GetField(RowTitle).GetValue(m_clsDatatJogTeach).ToString();
                            dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                        }
                        break;
                    case "_PosEnum_String":
                        dgvJogAndTeach.ReadOnly = true;
                        strValue = m_clsDatatJogTeach._PosEnum_String;
                        if (InputPosition("Select Position", "Position", ref strValue) == DialogResult.OK)
                        {
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, strValue);
                            dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                        }
                        break;
                    case "_SafePositionEnum_String":
                        dgvJogAndTeach.ReadOnly = true;
                        strValue = m_clsDatatJogTeach._SafePositionEnum_String;
                        if (InputPosition("Select Position", "Position", ref strValue) == DialogResult.OK)
                        {
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, strValue);
                            string Tem = m_clsDatatJogTeach.GetType().GetField(RowTitle).GetValue(m_clsDatatJogTeach).ToString();
                            dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                        }
                        break;
                    case "_JogAImageDirPoint":
                    case "_JogBImageDirPoint":
                        dgvJogAndTeach.ReadOnly = true;
                        try
                        {
                            Point Tem = (Point)m_clsDatatJogTeach.GetType().GetField(RowTitle).GetValue(m_clsDatatJogTeach);
                            iValue1 = Tem.X;
                            iValue2 = Tem.Y;
                        }
                        catch
                        {
                        }
                        if (InputPointInt("Select Position", "Position", ref iValue1, ref iValue2) == DialogResult.OK)
                        {
                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, new Point(iValue1, iValue2));
                            dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisJogAndTeachInfo[e.ColumnIndex]);
                        }
                        break;
                    default:
                        for (int i = 0; i < Sourcelist.Count; i++)
                        {
                            try
                            {
                                if (Sourcelist[i].Name == RowTitle)
                                {
                                    if (Sourcelist[i].GetValue(m_clsDatatJogTeach) is string)
                                    {
                                        dgvJogAndTeach.ReadOnly = false;
                                    }
                                    if (Sourcelist[i].GetValue(m_clsDatatJogTeach) is Color)
                                    {
                                        dgvJogAndTeach.ReadOnly = true;
                                        {
                                            ColorDialog m_colorDialog = new ColorDialog();
                                            if (m_colorDialog.ShowDialog() == DialogResult.OK)
                                            {
                                                m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, m_colorDialog.Color);
                                                dgvJogAnd_UpdateValue(e.ColumnIndex, m_clsSlave._lisDIOInfo[e.ColumnIndex]);
                                            }
                                        }
                                    }
                                    else if (Sourcelist[i].GetValue(m_clsDatatJogTeach) is bool)
                                    {
                                        dgvJogAndTeach.ReadOnly = true;
                                        if (dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value.ToString() == true.ToString())
                                        {
                                            dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = false.ToString();
                                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, false);
                                        }
                                        else
                                        {
                                            dgvJogAndTeach[e.ColumnIndex, e.RowIndex].Value = true.ToString();
                                            m_clsDatatJogTeach.GetType().GetField(RowTitle).SetValue(m_clsDatatJogTeach, true);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        break;
                }
            }
            catch
            {
            }
        }

        private void dgvDIO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
                string RowTitle = dgvDIOTitle[e.RowIndex];
                clsDataDIO m_clsDataDIO = m_clsSlave._lisDIOInfo[e.ColumnIndex];
                string strValue = "";
                var Sourcelist = m_clsDataDIO.GetType().GetFields().ToList();

                if (RowTitle == "_DIHomeEnum_String"
                    || RowTitle == "_DIReachEnum_String")
                {
                    dgvDIO.ReadOnly = true;
                    if (RowTitle == "_DIHomeEnum_String")
                    {
                        strValue = m_clsSlave._lisDIOInfo[e.ColumnIndex]._DIHomeEnum_String;
                    }
                    else if (RowTitle == "_DIReachEnum_String")
                    {
                        strValue = m_clsSlave._lisDIOInfo[e.ColumnIndex]._DIReachEnum_String;
                    }
                    if (InputDI("Select DI", "DI", ref strValue) == DialogResult.OK)
                    {
                        m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, strValue);
                        string Tem = m_clsDataDIO.GetType().GetField(RowTitle).GetValue(m_clsDataDIO).ToString();
                        dgvDIO_UpdateValue(e.ColumnIndex, m_clsSlave._lisDIOInfo[e.ColumnIndex]);
                    }
                }
                else if (RowTitle == "_DOTriggerEnum_String"
                    || RowTitle == "_DOTrigger2Enum_String")
                {
                    dgvDIO.ReadOnly = true;
                    if (RowTitle == "_DOTriggerEnum_String")
                    {
                        strValue = m_clsSlave._lisDIOInfo[e.ColumnIndex]._DOTriggerEnum_String;
                    }
                    else if (RowTitle == "_DOTrigger2Enum_String")
                    {
                        strValue = m_clsSlave._lisDIOInfo[e.ColumnIndex]._DOTrigger2Enum_String;
                    }
                    if (InputDO("Select DO", "DO", ref strValue) == DialogResult.OK)
                    {
                        m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, strValue);
                        string Tem = m_clsDataDIO.GetType().GetField(RowTitle).GetValue(m_clsDataDIO).ToString();
                        dgvDIO_UpdateValue(e.ColumnIndex, m_clsSlave._lisDIOInfo[e.ColumnIndex]);
                    }
                }
                else
                {
                    for (int i = 0; i < Sourcelist.Count; i++)
                    {
                        try
                        {
                            if (Sourcelist[i].Name == RowTitle)
                            {
                                if (Sourcelist[i].GetValue(m_clsDataDIO) is string)
                                {
                                    dgvDIO.ReadOnly = false;
                                }
                                if (Sourcelist[i].GetValue(m_clsDataDIO) is Color)
                                {
                                    dgvDIO.ReadOnly = true;
                                    {
                                        ColorDialog m_colorDialog = new ColorDialog();
                                        if (m_colorDialog.ShowDialog() == DialogResult.OK)
                                        {
                                            m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, m_colorDialog.Color);
                                            dgvDIO_UpdateValue(e.ColumnIndex, m_clsSlave._lisDIOInfo[e.ColumnIndex]);
                                        }
                                    }
                                }
                                else if (Sourcelist[i].GetValue(m_clsDataDIO) is bool)
                                {
                                    dgvDIO.ReadOnly = true;
                                    if (dgvDIO[e.ColumnIndex, e.RowIndex].Value.ToString() == true.ToString())
                                    {
                                        dgvDIO[e.ColumnIndex, e.RowIndex].Value = false.ToString();
                                        m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, false);
                                    }
                                    else
                                    {
                                        dgvDIO[e.ColumnIndex, e.RowIndex].Value = true.ToString();
                                        m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, true);
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void dgvDIO_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDIO.ReadOnly == false)
                {
                    string RowTitle = dgvDIOTitle[e.RowIndex];
                    clsDataDIO m_clsDataDIO = m_clsSlave._lisDIOInfo[e.ColumnIndex];
                    switch (RowTitle)
                    {
                        case "_DIOName":
                            m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, dgvDIO[e.ColumnIndex, e.RowIndex].Value.ToString());
                            break;
                        default:
                            m_clsDataDIO.GetType().GetField(RowTitle).SetValue(m_clsDataDIO, dgvDIO[e.ColumnIndex, e.RowIndex].Value.ToString());
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (m_ExitEvent != null)
            {
                m_ExitEvent(sender, e);
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            strCopyDio_MasterName = strCurrentMasterName;
            strCopyDio_SlaveName = m_clsSlave._strNodeName;
            MessageBox.Show("[Copy] DIO\r\nMaster : " + strCopyDio_MasterName + "\r\nSlave : " + strCopyDio_SlaveName);
        }

        private void BtnPaste_Click(object sender, EventArgs e)
        {
            clsProFile.clsMaster m_clsMaster_Source = null;
            clsProFile.clsSlave m_clsSlave_Source = null;
            clsProFile.clsMaster m_clsMaster_Target = null;
            clsProFile.clsSlave m_clsSlave_Target = null;
            if (m_lisMaster.Exists(x => x._strMaster == strCopyDio_MasterName))
            {
                m_clsMaster_Source = m_lisMaster.Find(x => x._strMaster == strCopyDio_MasterName);
                if (m_clsMaster_Source._lisSlave.Exists(x => x._strNodeName == strCopyDio_SlaveName))
                {
                    m_clsSlave_Source = m_clsMaster_Source._lisSlave.Find(x => x._strNodeName == strCopyDio_SlaveName);
                }
            }
            if (m_lisMaster.Exists(x => x._strMaster == strCurrentMasterName))
            {
                m_clsMaster_Target = m_lisMaster.Find(x => x._strMaster == strCurrentMasterName);
                if (m_clsMaster_Target._lisSlave.Exists(x => x._strNodeName == m_clsSlave._strNodeName))
                {
                    m_clsSlave_Target = m_clsMaster_Target._lisSlave.Find(x => x._strNodeName == m_clsSlave._strNodeName);
                }
            }
            if (m_clsSlave_Source != null
                && m_clsSlave_Target != null)
            {
                if (MessageBox.Show("Source : " + m_clsSlave_Source._strNodeName + "\r\nTarget : " + m_clsSlave_Target._strNodeName
                    , "[Copy DIO]", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (m_clsSlave_Source._lisDIOInfo.Count == m_clsSlave_Target._lisDIOInfo.Count)
                        {
                            for (int i = 0; i < m_clsSlave_Source._lisDIOInfo.Count; i++)
                            {
                                clsDataFunction.Copy<clsDataDIO>(m_clsSlave_Source._lisDIOInfo[i], m_clsSlave_Target._lisDIOInfo[i]);
                                dgvDIO_UpdateValue(i, m_clsSlave._lisDIOInfo[i]);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Fail");
                    }
                }
            }
        }

    }
}
