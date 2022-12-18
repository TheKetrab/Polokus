using Polokus.App.Fonts;
using Polokus.App.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Controls
{
    public class PolokusHeader : System.Windows.Forms.Panel
    {
        Image polokusLogo;
        string text = "POLOKUS";
        const int imgSize = 64;
        const int spacing = 10;


        public PolokusHeader()
        {
            polokusLogo = Properties.Resources.Polokus128;
        }

        public bool Frozen { get; set; } = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Frozen)
            {
                if (this.Width >= PolokusStyle.HideMenuPixels)
                {
                    DrawNormal(e);
                }
                else
                {
                    DrawMini(e);
                }
            }

            base.OnPaint(e);
        }

        private void DrawNormal(PaintEventArgs e)
        {
            var textSize = e.Graphics.MeasureString(text, PolokusStyle.HeaderFont);

            float x1 = (this.Width - textSize.Width - spacing - imgSize) / 2;
            float y1 = (this.Height - textSize.Height) / 2;
            e.Graphics.DrawString(text, PolokusStyle.HeaderFont, Brushes.AliceBlue, new PointF(x1, y1));

            float x2 = x1 + textSize.Width + spacing;
            float y2 = (this.Height - imgSize) / 2;
            Bitmap bitmap = new Bitmap(polokusLogo, new Size(imgSize, imgSize));
            e.Graphics.DrawImage(bitmap, new PointF(x2, y2));
        }

        private void DrawMini(PaintEventArgs e)
        {
            float x1 = (this.Width - imgSize) / 2;
            float y1 = (this.Height - imgSize) / 2;

            Bitmap bitmap = new Bitmap(polokusLogo, new Size(imgSize, imgSize));
            e.Graphics.DrawImage(bitmap, new PointF(x1, y1));
        }

    }
}
