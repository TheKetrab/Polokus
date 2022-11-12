using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.SchemeHandler;
using CefSharp.WinForms;

namespace Polokus.App.Views
{
    public partial class ChromiumWindow : UserControl
    {
        private readonly string bpmnioPage = Path.Combine(Application.StartupPath,"public/index.html");

        public ChromiumWindow(string mode = "")
        {
            InitializeComponent();
            InitializeChromium(mode);
        }

        public ChromiumWebBrowser chromeBrowser;

        public void InitializeChromium(string mode)
        {
            string opts = "";
            if (mode == "viewer")
            {
                opts = "?mode=viewer";
            }
            chromeBrowser = new ChromiumWebBrowser(bpmnioPage + opts);
            chromeBrowser.Dock = DockStyle.Fill;

            this.Controls.Add(chromeBrowser);
        }




        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
