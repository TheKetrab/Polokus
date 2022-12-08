using Polokus.App.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Controls
{
    public static class PolokusStyle
    {
        // https://coolors.co/palette/0466c8-0353a4-023e7d-002855-001845-001233-33415c-5c677d-7d8597-979dac
        public static readonly Color ForeColor = Color.White;
        public static readonly Color BackColor = Color.FromArgb(00, 28, 55);
        public static readonly Color HoverForeColor = Color.White;
        public static readonly Color HoverBackColor = Color.FromArgb(0x023E7D);

        public static readonly Color TaskBarColor1 = Color.FromArgb(0, 18, 51);
        public static readonly Color TaskBarColor2 = Color.FromArgb(51, 65, 92);

        public static readonly Color DefaultTrackColor = Color.FromArgb(0, 18, 51);
        public static readonly Color DefaultViewColor = Color.FromArgb(151, 157, 172);

        public static readonly Font MiniFont = new Font(FontsManager.Montserrat, 9, FontStyle.Regular);
        public static readonly Font DefFont = new Font(FontsManager.Montserrat, 11, FontStyle.Regular);
        public static readonly Font TextBoxFont = MiniFont;
        public static readonly Font MenuFont = new Font(FontsManager.Montserrat, 11, FontStyle.Bold);
        public static readonly Font HeaderFont = new Font(FontsManager.Montserrat, 13, FontStyle.Bold);



        public const int HideMenuPixels = 170;

    }
}
