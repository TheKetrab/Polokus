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
using Polokus.App.Forms;
using Polokus.App.Utils;

namespace Polokus.App.Views
{
    public partial class ChromiumWindow : UserControl
    {
        private MainWindow _mainWindow;
        private readonly string bpmnioPage = Path.Combine(Application.StartupPath,"editor/index.html");

        public ChromiumWindow(MainWindow mainWindow, string mode = "")
        {
            _mainWindow = mainWindow;
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

            DownloadHandler downloadHandler = new DownloadHandler();
            chromeBrowser.DownloadHandler = downloadHandler;



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
