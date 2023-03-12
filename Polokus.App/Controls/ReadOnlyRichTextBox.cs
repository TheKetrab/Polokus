using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Polokus.App.Controls
{
    public class ReadOnlyRichTextBox : System.Windows.Forms.RichTextBox
    {

        [DllImport("user32.dll")]
        private static extern int HideCaret(IntPtr hwnd);

        public ReadOnlyRichTextBox()
        {
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ReadOnlyRichTextBox_Mouse);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ReadOnlyRichTextBox_Mouse);
            base.ReadOnly = true;
            base.TabStop = false;
            HideCaret(this.Handle);
        }


        protected override void OnGotFocus(EventArgs e)
        {
            HideCaret(this.Handle);
        }

        protected override void OnEnter(EventArgs e)
        {
            HideCaret(this.Handle);
        }

        [DefaultValue(true)]
        public new bool ReadOnly
        {
            get { return true; }
            set { }
        }

        [DefaultValue(false)]
        public new bool TabStop
        {
            get { return false; }
            set { }
        }

        private void ReadOnlyRichTextBox_Mouse(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            HideCaret(this.Handle);
        }

        public void AppendFormattedText(string text, Color textColor, Boolean isBold, HorizontalAlignment alignment = HorizontalAlignment.Left)
        {
            // https://stackoverflow.com/a/35464653/12479893

            int start = this.TextLength;
            this.AppendText(text);
            int end = this.TextLength; // now longer by length of appended text

            // Select text that was appended
            this.Select(start, end - start);

            #region Apply Formatting
            this.SelectionColor = textColor;
            this.SelectionAlignment = alignment;
            this.SelectionFont = new Font(
                this.SelectionFont.FontFamily,
                this.SelectionFont.Size,
                (isBold ? FontStyle.Bold : FontStyle.Regular));
            #endregion

            // Unselect text
            this.SelectionLength = 0;


            this.ScrollToCaret();
        }
    }
}