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

        public async void TestClick()
        {
            var result = await chromeBrowser.EvaluateScriptAsync("myTestAction", new object[] { });

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
