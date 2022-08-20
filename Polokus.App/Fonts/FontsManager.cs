using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Fonts
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static class FontsManager
    {
        public enum SegMDL2 : int
        {
            Custom = 0,
            LastArrow = 0xe700,
            Edit = 0xE70F,
            Save = 0xE74E,
            Setting = 0xE713
        }

        private static PrivateFontCollection pfc = new PrivateFontCollection();

        static FontsManager()
        {
            AddFontFromResources("Polokus.App.Fonts.segmdl2.ttf");
        }

        private static void AddFontFromResources(string resourcesName)
        {
            Stream? stream = typeof(FontsManager)?.Assembly
                ?.GetManifestResourceStream(resourcesName);

            if (stream == null)
                return;

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);

            stream.Close();
            
            IntPtr memPointer = System.Runtime.InteropServices.Marshal.AllocHGlobal(
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(byte)) * bytes.Length);
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, memPointer, bytes.Length);

            pfc.AddMemoryFont(memPointer, bytes.Length);
        }


        public static FontFamily FFSegMDL2 => 
            (pfc.Families.Length > 0) ? pfc.Families[0] : FontFamily.GenericSerif;

    }
}
