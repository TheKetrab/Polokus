using Polokus.App.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Controls
{
    public class PolokusMenuButton : System.Windows.Forms.Button
    {
        private string? _baseText;


        public PolokusMenuButton()
        {
            SetStyle();
        }

        public void SetFocused(bool focused)
        {
            this.FlatAppearance.BorderSize = focused ? 1 : 0;
        }

        public void SetStyle()
        {
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.ForeColor = PolokusStyle.ForeColor;
            this.ImageAlign = ContentAlignment.MiddleLeft;

            this.Margin = new Padding(0, 10, 0, 10);

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = PolokusStyle.ForeColor;
            this.BackColor = PolokusStyle.BackColor;
            this.ForeColor = PolokusStyle.ForeColor;

        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = PolokusStyle.HoverBackColor;
            this.ForeColor = PolokusStyle.HoverForeColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = PolokusStyle.BackColor;
            this.ForeColor = PolokusStyle.ForeColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SetGapBetweenImageAndText();
            SetFont();
            ShowHideText();
            AlignImage();
            base.OnPaint(e);
        }

        private void SetGapBetweenImageAndText()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return;
            }

            string gap = new string(' ', 3);
            if (this.Text.StartsWith(gap))
            {
                return;
            }

            this.Text = gap + this.Text.TrimStart();
        }

        private void ShowHideText()
        {
            if (this.Parent == null)
            {
                return;
            }

            if (this.Parent.Width < PolokusStyle.HideMenuPixels)
            {
                if (_baseText == null)
                {
                    _baseText = this.Text;
                    this.Text = "";
                }
            }
            else
            {
                if (_baseText != null)
                {
                    this.Text = _baseText;
                    _baseText = null;
                }
            }
        }

        private void AlignImage()
        {
            if (this.Image == null)
            {
                return;
            }
            int maxHeight = this.Height - this.Padding.Top - this.Padding.Bottom - this.Margin.Top - this.Margin.Bottom;
            if (this.Image.Height > maxHeight)
            {
                this.Image = new Bitmap(this.Image, new Size(maxHeight, maxHeight));
            }
        }

        private void SetFont()
        {
            this.Font = PolokusStyle.MenuFont;
        }
    }
}
