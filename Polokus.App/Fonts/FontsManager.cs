using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
            Edit = 0xE70F,
            Save = 0xE74E,
            Setting = 0xE713,
            Open = 0xF12B,
            File = 0xE8A5,
            Repair = 0xE90F,
            ToggleLeft = 0xE970,
            Help = 0xE897,
            Close = 0xE8BB,
            Minimize = 0xE921,
            Maximize = 0xE922,
            Restore = 0xE923,

        }

        private static PrivateFontCollection pfc = new PrivateFontCollection();

        static int idFFSegMDL2 = -1;
        static int idMontserrat = -1;


        static FontsManager()
        {
            AddFontFromResources("Polokus.App.Fonts.segmdl2.ttf");
            AddfontFromProperties(Properties.Resources.Montserrat_VariableFont_wght);

            CalculateFontsIndices();
        }

        private static void CalculateFontsIndices()
        {
            idFFSegMDL2 = GetIndexOf("Segoe MDL2 Assets");
            idMontserrat = GetIndexOf("Montserrat");
        }

        private static int GetIndexOf(string name)
        {
            for (int i=0; i<pfc.Families.Length; i++)
            {
                if (pfc.Families[i].Name == name)
                {
                    return i;
                }
            }

            return -1;
        }


        private static void AddfontFromProperties(byte[] fontContent)
        {
            int fontLength = fontContent.Length;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontContent, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
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
            
            IntPtr memPointer = Marshal.AllocHGlobal(
                Marshal.SizeOf(typeof(byte)) * bytes.Length);
            Marshal.Copy(bytes, 0, memPointer, bytes.Length);

            pfc.AddMemoryFont(memPointer, bytes.Length);
        }


        public static FontFamily FFSegMDL2 => idFFSegMDL2 >= 0
            ? pfc.Families[idFFSegMDL2] : FontFamily.GenericSerif;
        public static FontFamily Montserrat => idMontserrat >= 0
            ? pfc.Families[idMontserrat] : FontFamily.GenericSerif;

    }
}
