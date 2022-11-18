using Polokus.App.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
