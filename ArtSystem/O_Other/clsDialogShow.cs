using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem
{
    public class clsDialogShow
    {
        /// <summary> 取得一個String </summary>
        static public DialogResult InputString(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox2 = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = clsLanguage.GetTranslation(title, false);
            label.Text = clsLanguage.GetTranslation(promptText, false);
            textBox2.Text = value;

            buttonOk.Text = clsLanguage.GetTranslation("OK");
            buttonCancel.Text = clsLanguage.GetTranslation("Cancel");
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(14, 20, 372, 13);
            textBox2.SetBounds(12, 36, 372, 20);


            buttonOk.SetBounds(228, 70, 75, 23);
            buttonCancel.SetBounds(309, 70, 75, 23);

            label.AutoSize = true;
            textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;


            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox2, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox2.Text;
            return dialogResult;
        }


        /// <summary> 取得一個String </summary>
        static public DialogResult SelectItem(string title, List<string> promptItem, ref string value)
        {
            Form form = new Form();
            //Label label = new Label();
            ListBox textBox2 = new ListBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = clsLanguage.GetTranslation(title, false);
            textBox2.Items.Clear();
            for (int i = 0; i < promptItem.Count; i++)
            {
                textBox2.Items.Add(promptItem[i]);
            }
            if (textBox2.Items.Contains(value) == true)
            {
                textBox2.SelectedItem = value;
            }

            buttonOk.Text = clsLanguage.GetTranslation("OK");
            buttonCancel.Text = clsLanguage.GetTranslation("Cancel");
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            int iHeight = 13 * promptItem.Count + 30;
            int iMaxHeight = Convert.ToInt32( Screen.PrimaryScreen.Bounds.Height * 0.7);
            if (iHeight > iMaxHeight)
            {
                iHeight = iMaxHeight;
            }

            //label.SetBounds(14, 20, 372, 13);
            textBox2.SetBounds(12, 20, 372, iHeight);

            int iTop = textBox2.Top + textBox2.Height + 14;
            buttonOk.SetBounds(228, iTop, 75, 23);
            buttonCancel.SetBounds(309, iTop, 75, 23);

            //label.AutoSize = true;
            textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;


            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            int iFormHeight = buttonOk.Top + buttonOk.Height + 14;
            form.ClientSize = new Size(396, iFormHeight);
            form.Controls.AddRange(new Control[] {  textBox2, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, textBox2.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            if (textBox2.SelectedItem != null)
            {
                value = textBox2.SelectedItem.ToString();
            }
            else
            {
                value = "";
            }
            return dialogResult;
        }

        /// <summary> 取得一個String </summary>
        static public DialogResult SelectMultiItem(string title, List<string> promptItem, ref List<string> value)
        {
            Form form = new Form();
            //Label label = new Label();
            ListBox textBox2 = new ListBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = clsLanguage.GetTranslation(title, false);
            textBox2.Items.Clear();
            for (int i = 0; i < promptItem.Count; i++)
            {
                textBox2.Items.Add(promptItem[i]);
            }
            textBox2.SelectionMode = SelectionMode.MultiExtended;

            for (int i = 0; i < textBox2.Items.Count; i++)
            {
                if (value.Contains(textBox2.Items[i]) == true)
                {
                    textBox2.SelectedItems.Add(textBox2.Items[i]);
                }
            }

            buttonOk.Text = clsLanguage.GetTranslation("OK");
            buttonCancel.Text = clsLanguage.GetTranslation("Cancel");
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            int iHeight = 13 * promptItem.Count;
            int iMaxHeight = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.7);
            if (iHeight > iMaxHeight)
            {
                iHeight = iMaxHeight;
            }

            //label.SetBounds(14, 20, 372, 13);
            textBox2.SetBounds(12, 20, 372, iHeight);
            
            int iTop = textBox2.Top + textBox2.Height + 14;
            buttonOk.SetBounds(228, iTop, 75, 23);
            buttonCancel.SetBounds(309, iTop, 75, 23);

            //label.AutoSize = true;
            textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;


            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            int iFormHeight = buttonOk.Top + buttonOk.Height + 14;
            form.ClientSize = new Size(396, iFormHeight);
            form.Controls.AddRange(new Control[] { textBox2, /*labe2, textBox2,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, textBox2.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value.Clear();
            foreach (var Items in textBox2.SelectedItems)
            {
                value.Add(Items.ToString());
            }

            return dialogResult;
        }
    }
}
