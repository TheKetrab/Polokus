using CefSharp.WinForms;
using Polokus.App.Forms;
using Polokus.App.Utils;

namespace Polokus.App.Views
{
    public partial class ChromiumWindow : UserControl
    {
        private MainWindow _mainWindow;
        private readonly string bpmnioPage = Path.Combine(Application.StartupPath, "editor/index.html");

        public ChromiumWindow(MainWindow mainWindow, string mode = "")
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            chromeBrowser = CreateAndInitializeChromium(mode);
        }

        public ChromiumWebBrowser chromeBrowser;

        private ChromiumWebBrowser CreateAndInitializeChromium(string mode)
        {
            string opts = "";
            if (mode == "viewer")
            {
                opts = "?mode=viewer";
            }
            var chromeBrowser = new ChromiumWebBrowser(bpmnioPage + opts);
            chromeBrowser.Dock = DockStyle.Fill;

            DownloadHandler downloadHandler = new DownloadHandler();
            chromeBrowser.DownloadHandler = downloadHandler;

            this.Controls.Add(chromeBrowser);

            return chromeBrowser;
        }

    }
}
