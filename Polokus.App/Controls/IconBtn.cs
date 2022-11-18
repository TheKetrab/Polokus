using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Polokus.App.Fonts;

namespace Polokus.App.Controls
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]

    public class IconBtn : Button
    {
        public FontsManager.SegMDL2 FontChar { get; set; }
        public int FontCharCustom { get; set; }
        public float FontSize { get; set; } = 13f;
        public FontStyle FontStyle { get; set; } = FontStyle.Bold;

        protected int KeyVal => (FontChar == FontsManager.SegMDL2.Custom) 
            ? FontCharCustom : (int)FontChar;

        public IconBtn()
        {
            this.Padding = new Padding(5);
        }


        private string _text = "";
        public override string Text 
        {
            get => ""; 
            set => _text = value;
        }

        public void AlignToParent()
        {
            if (Parent is FlowLayoutPanel flp)
            {
                if (flp.FlowDirection == FlowDirection.LeftToRight
                    || flp.FlowDirection == FlowDirection.RightToLeft)
                {
                    this.Height = Parent.Height - this.Margin.Bottom - this.Margin.Top;
                    this.Width = this.Height;
                }
                else
                {
                    this.Width = Parent.Width - this.Margin.Left - this.Margin.Right;
                    this.Height = this.Width;

                }
            }
        }

        public void DrawIcon(PaintEventArgs e)
        {
            int w = this.Width - this.Padding.Left - this.Padding.Right;
            int h = this.Height - this.Padding.Bottom - this.Padding.Top;
            string iconChar = char.ConvertFromUtf32(KeyVal);

            float fontSize = this.FontSize;
            for (; fontSize > 5; fontSize--)
            {
                Font iconFont2 = new Font(FontsManager.FFSegMDL2, fontSize, FontStyle);
                var size = e.Graphics.MeasureString(iconChar, iconFont2);
                if (size.Width < w && size.Height < h)
                {
                    break;
                }

            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font iconFont = new Font(FontsManager.FFSegMDL2, fontSize, FontStyle);
            
            e.Graphics.DrawString(iconChar, iconFont,
                new SolidBrush(this.ForeColor),
                new Rectangle(0, 0, this.Width, this.Height),
                stringFormat);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            AlignToParent();
            DrawIcon(pevent);
        }
    }
}
