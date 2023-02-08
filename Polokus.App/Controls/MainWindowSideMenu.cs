using Polokus.App.Forms;
using Polokus.App.Utils;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;

namespace Polokus.App.Controls
{
    public partial class MainWindowSideMenu : UserControl
    {
        private MainWindow _mainWindow;
        private bool _processPanelVisible = false;
        private MainWindowViewModel.PanelView _lastOpenedViewInProcessesView = MainWindowViewModel.PanelView.None;

        public PolokusHeader Header => this.panelPolokusHeader;

        public MainWindowSideMenu(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            SetStyle();

            this.panelProcesses.Visible = false;
            this.buttonSettings.Dock = DockStyle.Top;
            _processPanelVisible = false;
            InitializeTreeView();

        }

        private void SetStyle()
        {
            this.panelSideMenu.BackColor = PolokusStyle.SideMenuColor;

            this.buttonGraphView.Image = PolokusStyle.ButtonGraphImage;
            this.buttonGraphView.SetStyle();

            this.buttonProcesses.Image = PolokusStyle.ButtonProcessesImage;
            this.buttonProcesses.SetStyle();

            this.buttonScriptView.Image = PolokusStyle.ButtonScriptImage;
            this.buttonScriptView.SetStyle();

            this.buttonSettings.Image = PolokusStyle.ButtonSettingsImage;
            this.buttonSettings.SetStyle();

            this.buttonService.Image = PolokusStyle.ButtonWrenchImage;
            this.buttonService.SetStyle();

            this.buttonXmlView.Image = PolokusStyle.ButtonXMLImage;
            this.buttonXmlView.SetStyle();

            this.buttonEditor.Image = PolokusStyle.ButtonGraphImage;
            this.buttonEditor.SetStyle();
        }

        public void AdjustSize(Size size)
        {
            this.Size = size;
        }

        private void InitializeTreeView()
        {
            var files = Directory.GetFiles(PolokusApp.BpmnPath).Select(x => new FileInfo(x).Name);
            this.treeView1.Nodes.AddRange(files.Select(x => new TreeNode(x)).ToArray());

            this.treeView1.AfterSelect += (s, e) =>
            {
                var node = treeView1.SelectedNode;
                TVIndexChanged?.Invoke(this, new TVIndexChangedEventArgs(Path.Combine(PolokusApp.BpmnPath, node.Text)));
            };

        }

        public class TVIndexChangedEventArgs
        {
            public string FilePath { get; set; }

            public TVIndexChangedEventArgs(string filePath)
            {
                FilePath = filePath;

            }
        }
        public event EventHandler<TVIndexChangedEventArgs>? TVIndexChanged;


        private async Task HideProcessesPanel()
        {
            if (!_processPanelVisible)
            {
                return;
            }

            this.panelProcesses.Visible = true;
            this.panelProcesses.Dock = DockStyle.Top;

            await this.panelProcesses.AnimateProperty(
                (f) => { this.panelProcesses.Height = (int)f; },
                ProcessPanelHeight, 0, 100);

            this.panelProcesses.Visible = false;
            this.buttonSettings.Dock = DockStyle.Top;
            _processPanelVisible = false;
        }

        private int ProcessPanelHeight => this.panelSideMenu.Height
                - panelLogo.Height
                - buttonSettings.Height
                - buttonEditor.Height
                - buttonService.Height
                - buttonProcesses.Height;


        private async Task ShowProcessesPanel()
        {
            if (_processPanelVisible)
            {
                return;
            }

            this.panelProcesses.Dock = DockStyle.Top;
            this.panelProcesses.Visible = true;
            this.buttonSettings.Dock = DockStyle.Top;

            await this.panelProcesses.AnimateProperty(
                (f) => { this.panelProcesses.Height = (int)f; },
                0, ProcessPanelHeight, 100);

            this.panelProcesses.Dock = DockStyle.Fill;
            this.buttonSettings.Dock = DockStyle.Bottom;
            _processPanelVisible = true;
        }



        private async void buttonService_Click(object sender, EventArgs e)
        {
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Service;
            await HideProcessesPanel();
        }

        private async void buttonProcesses_Click(object sender, EventArgs e)
        {
            switch (_lastOpenedViewInProcessesView)
            {
                case MainWindowViewModel.PanelView.ProcessesGraph:
                    _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesGraph;
                    break;
                case MainWindowViewModel.PanelView.ProcessesXml:
                    _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesXml;
                    break;
                case MainWindowViewModel.PanelView.ProcessesScript:
                    _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesScript;
                    break;
            }
            await ShowProcessesPanel();
        }

        private async void buttonSettings_Click(object sender, EventArgs e)
        {
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Settings;
            await HideProcessesPanel();
        }

        private void buttonGraphView_Click(object sender, EventArgs e)
        {
            _lastOpenedViewInProcessesView = MainWindowViewModel.PanelView.ProcessesGraph;
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesGraph;
        }

        private void buttonXmlView_Click(object sender, EventArgs e)
        {
            _lastOpenedViewInProcessesView = MainWindowViewModel.PanelView.ProcessesXml;
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesXml;
        }

        private void buttonScriptView_Click(object sender, EventArgs e)
        {
            _lastOpenedViewInProcessesView = MainWindowViewModel.PanelView.ProcessesScript;
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesScript;
        }

        private async void buttonEditor_Click(object sender, EventArgs e)
        {
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Editor;
            await HideProcessesPanel();
        }

        private void panelLogo_Click(object sender, EventArgs e)
        {
            _mainWindow.ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Home;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelLogo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(PolokusApp.MainWindow.Handle, 0x112, 0xf012, 0);
        }
    }
}
