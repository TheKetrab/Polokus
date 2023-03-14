using Polokus.App.Fonts;

namespace Polokus.App.Controls
{
    public class PolokusLabel : System.Windows.Forms.Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Font.FontFamily != FontsManager.Montserrat)
            {
                this.Font = new Font(FontsManager.Montserrat, this.Font.Size);
            }

            base.OnPaint(e);
        }

    }
}
