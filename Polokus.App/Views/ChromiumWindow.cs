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
        private readonly string bpmnioPage = Path.Combine(Application.StartupPath,"bpmnio.html");

        public ChromiumWindow()
        {
            InitializeComponent();
            InitializeChromium();
        }

        public ChromiumWebBrowser chromeBrowser;

        public void InitializeChromium()
        {
            chromeBrowser = new ChromiumWebBrowser(bpmnioPage);
            chromeBrowser.Dock = DockStyle.Fill;

            this.Controls.Add(chromeBrowser);
        }
    }
}
