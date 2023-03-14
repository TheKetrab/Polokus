namespace Polokus.App.Controls
{
    public class PolokusIconButton : System.Windows.Forms.Button
    {
        public PolokusIconButton()
        {
            SetStyle();
        }

        public void SetStyle()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = PolokusStyle.ForeColor;
        }

        public void SetFocused(bool focused)
        {
            this.FlatAppearance.BorderSize = focused ? 1 : 0;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (Image != null)
            {
                SetImage();
            }

            base.OnPaint(pevent);
        }

        private void SetImage()
        {
            int w = this.Width - this.Padding.Right - this.Padding.Left;
            int h = this.Height - this.Padding.Top - this.Padding.Bottom;

            if (this.Image.Width > w && this.Image.Height > h)
            {
                int a = Math.Min(w, h) - 5;
                this.Image = new Bitmap(this.Image, new Size(a, a));
            }


        }
    }
}
