using System.Runtime.InteropServices;

namespace Polokus.App.Controls
{
    public class PolokusTrackBar : System.Windows.Forms.TrackBar
    {
        public Color TrackColor { get; set; }

        // custom draw item specs
        private const int TBCD_TICS = 0x1;
        private const int TBCD_THUMB = 0x2;
        private const int TBCD_CHANNEL = 0x3;

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMCUSTOMDRAW
        {
            public NMHDR hdr;
            public int dwDrawStage;
            public IntPtr hdc;
            public RECT rc;
            public IntPtr dwItemSpec;
            public uint uItemState;
            public IntPtr lItemlParam;
        }

        [Flags]
        public enum CDRF
        {
            CDRF_DODEFAULT = 0x0,
            CDRF_NEWFONT = 0x2,
            CDRF_SKIPDEFAULT = 0x4,
            CDRF_DOERASE = 0x8,
            CDRF_SKIPPOSTPAINT = 0x100,
            CDRF_NOTIFYPOSTPAINT = 0x10,
            CDRF_NOTIFYITEMDRAW = 0x20,
            CDRF_NOTIFYSUBITEMDRAW = 0x20,
            CDRF_NOTIFYPOSTERASE = 0x40
        }

        [Flags]
        public enum CDDS
        {
            CDDS_PREPAINT = 0x1,
            CDDS_POSTPAINT = 0x2,
            CDDS_PREERASE = 0x3,
            CDDS_POSTERASE = 0x4,
            CDDS_ITEM = 0x10000,
            CDDS_ITEMPREPAINT = (CDDS.CDDS_ITEM | CDDS.CDDS_PREPAINT),
            CDDS_ITEMPOSTPAINT = (CDDS.CDDS_ITEM | CDDS.CDDS_POSTPAINT),
            CDDS_ITEMPREERASE = (CDDS.CDDS_ITEM | CDDS.CDDS_PREERASE),
            CDDS_ITEMPOSTERASE = (CDDS.CDDS_ITEM | CDDS.CDDS_POSTERASE),
            CDDS_SUBITEM = 0x20000
        }

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int FillRect(IntPtr hDC, ref RECT lpRect, IntPtr hBR);

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_REFLECT + WM_NOFITY)
            {

                var pnmhdr = (NMHDR)m.GetLParam(typeof(NMHDR));
                if (pnmhdr.code == NM_CUSTOMDRAW)
                {
                    var pnmlv = (NMCUSTOMDRAW)m.GetLParam(typeof(NMCUSTOMDRAW));
                    switch (pnmlv.dwDrawStage)
                    {
                        case (int)CDDS.CDDS_PREPAINT:
                            {
                                m.Result = new IntPtr((int)CDRF.CDRF_NOTIFYITEMDRAW);
                                break;
                            }

                        case (int)CDDS.CDDS_ITEMPREPAINT:
                            {
                                if (((int)pnmlv.dwItemSpec == TBCD_THUMB))
                                {
                                    IntPtr hBrush = CreateSolidBrush(ColorTranslator.ToWin32(TrackColor));
                                    FillRect(pnmlv.hdc, ref pnmlv.rc, hBrush);
                                    DeleteObject(hBrush);

                                    m.Result = new IntPtr((int)CDRF.CDRF_SKIPDEFAULT);
                                }
                                else
                                    m.Result = new IntPtr((int)CDRF.CDRF_NOTIFYPOSTPAINT);
                                break;
                            }

                        case (int)CDDS.CDDS_ITEMPOSTPAINT:
                            {
                                m.Result = new IntPtr((int)CDRF.CDRF_DODEFAULT);
                                break;
                            }
                    }
                }
                return;
            }
            else
                base.WndProc(ref m);
        }

        private const int NM_FIRST = 0;
        private const int NM_CLICK = NM_FIRST - 2;
        private const int NM_CUSTOMDRAW = NM_FIRST - 12;
        private const int WM_REFLECT = 0x2000;
        private const int WM_NOFITY = 0x4E;
    }
}
