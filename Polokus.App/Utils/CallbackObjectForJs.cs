using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    public class CallbackObjectForJs
    {
        public void showMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public void downloadPng(string svg)
        {
            ImageConverter.SavePngFromSvg(svg);
        }
    }
}
