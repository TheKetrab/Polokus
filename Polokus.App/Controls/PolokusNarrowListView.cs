using System.Runtime.InteropServices;

namespace Polokus.App.Controls
{
    public class PolokusNarrowListView : System.Windows.Forms.ListView
    {
        [DllImport("user32")]
        private static extern long ShowScrollBar(long hwnd, long wBar, long bShow);
        long SB_HORZ = 0;
        long SB_VERT = 1;
        long SB_BOTH = 3;


        protected override void OnPaint(PaintEventArgs e)
        {
            ShowScrollBar(Handle.ToInt64(), SB_HORZ, 0);
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            ShowScrollBar(Handle.ToInt64(), SB_HORZ, 0);

            base.OnSizeChanged(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                CopySelectedValuesToClipboard();

            base.OnKeyDown(e);
        }

        private void CopySelectedValuesToClipboard()
        {
            if (this.SelectedItems.Count != 1)
            {
                return;
            }

            Clipboard.SetText(this.SelectedItems[0].Text);
        }
    }
}
