using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Controls
{
    public class PolokusIconButton : System.Windows.Forms.Button
    {
        public PolokusIconButton()
        {
            this.FlatStyle = FlatStyle.Flat;
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
