using Polokus.App.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Controls
{
    public class PolokusStyleImpl
    {
        public virtual Color MainWindowBackColor { get; } = Color.FromArgb(51, 65, 92);
        public virtual Color SideMenuColor { get; } = Color.Black;
        public virtual Color ForeColor { get; } = Color.White;
        public virtual Color BackColor { get; } = Color.FromArgb(00, 28, 55);
        public virtual Color HoverForeColor { get; } = Color.White;
        public virtual Color HoverBackColor { get; } = Color.FromArgb(0x023E7D);

        public virtual Color TaskBarColor1 { get; } = Color.FromArgb(0, 18, 51);
        public virtual Color TaskBarColor2 { get; } = Color.FromArgb(51, 65, 92);

        public virtual Color DefaultTrackColor { get; } = Color.FromArgb(0, 18, 51);
        public virtual Color DefaultViewColor { get; } = Color.FromArgb(151, 157, 172);

        public virtual Font MiniFont { get; } = new Font(FontsManager.Montserrat, 9, FontStyle.Regular);
        public virtual Font DefFont { get; } = new Font(FontsManager.Montserrat, 11, FontStyle.Regular);

        public virtual Font TextBoxFont { get; } = new Font(FontsManager.Montserrat, 9, FontStyle.Regular);
        public virtual Font MenuFont { get; } = new Font(FontsManager.Montserrat, 11, FontStyle.Bold);
        public virtual Font HeaderFont { get; } = new Font(FontsManager.Montserrat, 13, FontStyle.Bold);

        public virtual Brush PolokusLogoBrush { get; } = Brushes.AliceBlue;


        public virtual Image ButtonGraphImage { get; } = Properties.Resources.BtnGraph64_White;
        public virtual Image ButtonProcessesImage { get; } = Properties.Resources.BtnProcesses64_White;
        public virtual Image ButtonScriptImage { get; } = Properties.Resources.BtnScript64_White;
        public virtual Image ButtonSettingsImage { get; } = Properties.Resources.BtnSettings64_White;
        public virtual Image ButtonWrenchImage { get; } = Properties.Resources.BtnWrench64_White;
        public virtual Image ButtonXMLImage { get; } = Properties.Resources.BtnXML64_White;
    }

    public class PolokusLightStyle : PolokusStyleImpl
    {
        public override Color SideMenuColor { get; } = Color.FromArgb(230, 230, 230);
        public override Color MainWindowBackColor { get; } = Color.FromArgb(200, 200, 200);

        public override Color ForeColor { get; } = Color.Black;
        public override Color BackColor { get; } = Color.White;
        public override Color HoverForeColor { get; } = Color.FromArgb(30,30,30);
        public override Color HoverBackColor { get; } = Color.FromArgb(240, 240, 240);

        public override Color TaskBarColor1 { get; } = Color.FromArgb(230, 230, 230); 
        public override Color TaskBarColor2 { get; } = Color.FromArgb(170, 170, 170);


        public override Color DefaultTrackColor { get; } = Color.FromArgb(0, 18, 51);
        public override Color DefaultViewColor { get; } = Color.White;

        public override Brush PolokusLogoBrush { get; } = Brushes.Black;



        public override Image ButtonGraphImage { get; } = Properties.Resources.BtnGraph64;
        public override Image ButtonProcessesImage { get; } = Properties.Resources.BtnProcesses64;
        public override Image ButtonScriptImage { get; } = Properties.Resources.BtnScript64;
        public override Image ButtonSettingsImage { get; } = Properties.Resources.BtnSettings64;
        public override Image ButtonWrenchImage { get; } = Properties.Resources.BtnWrench64;
        public override Image ButtonXMLImage { get; } = Properties.Resources.BtnXML64;

    }




    public class PolokusStyle
    {
        private readonly static PolokusStyleImpl _style = PolokusApp.LightMode
            ? new PolokusLightStyle() : new PolokusStyleImpl();


        public static bool x = PolokusApp.TunnelWorks;

        public static readonly Color MainWindowBackColor = _style.MainWindowBackColor;
        public static readonly Color SideMenuColor = _style.SideMenuColor;

        // https://coolors.co/palette/0466c8-0353a4-023e7d-002855-001845-001233-33415c-5c677d-7d8597-979dac
        public static readonly Color ForeColor = _style.ForeColor;
        public static readonly Color BackColor = _style.BackColor;
        public static readonly Color HoverForeColor = _style.HoverForeColor;
        public static readonly Color HoverBackColor = _style.HoverBackColor;

        public static readonly Color TaskBarColor1 = _style.TaskBarColor1;
        public static readonly Color TaskBarColor2 = _style.TaskBarColor2;

        public static readonly Color DefaultTrackColor = _style.DefaultTrackColor;
        public static readonly Color DefaultViewColor = _style.DefaultViewColor;

        public static readonly Font MiniFont = _style.MiniFont;
        public static readonly Font DefFont = _style.DefFont;
        public static readonly Font TextBoxFont = _style.TextBoxFont;
        public static readonly Font MenuFont = _style.MenuFont;
        public static readonly Font HeaderFont = _style.HeaderFont;

        public static readonly Brush PolokusLogoBrush = _style.PolokusLogoBrush;

        public static readonly Image ButtonGraphImage = _style.ButtonGraphImage;
        public static readonly Image ButtonProcessesImage = _style.ButtonProcessesImage;
        public static readonly Image ButtonScriptImage = _style.ButtonScriptImage;
        public static readonly Image ButtonSettingsImage = _style.ButtonSettingsImage;
        public static readonly Image ButtonWrenchImage = _style.ButtonWrenchImage;
        public static readonly Image ButtonXMLImage = _style.ButtonXMLImage;


        public const int HideMenuPixels = 170;

    }
}
