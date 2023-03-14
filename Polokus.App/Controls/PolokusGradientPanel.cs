using System.Drawing.Drawing2D;

namespace Polokus.App.Controls
{
    public class PolokusGradientPanel : System.Windows.Forms.Panel
    {
        public Color GradientBeginColor { get; set; }
        public Color GradientEndColor { get; set; }
        public float GradientAngle { get; set; }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle, GradientBeginColor, GradientEndColor, GradientAngle))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}
