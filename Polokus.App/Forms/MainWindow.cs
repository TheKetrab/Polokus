
using Polokus.App.Controls;
using Polokus.App.Fonts;
using Polokus.App.Utils;
using Polokus.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polokus.App.Forms
{
    public partial class MainWindow : Form
    {
        public static MainWindow? _instance = null;
        public static MainWindow Instance
        {
            get {
                return _instance ?? throw new Exception("Main window is not initialized yet.");
            }
        }

        public readonly MainWindowViewModel ViewModel;

        public ServiceView ServiceView { get; }
        public ChromiumWindow EditorView { get; }
        public GraphView GraphView { get; }
        public XmlView XmlView { get; }
        public SettingsView SettingsView { get; }


        private bool _processPanelVisible = false;


        void InitializeTreeView()
        {
            var files = Directory.GetFiles(ViewModel.MainDirPath).Select(x => new FileInfo(x).Name);
            this.treeView1.Nodes.AddRange(files.Select(x => new TreeNode(x)).ToArray());

            this.treeView1.AfterSelect += (s, e) =>
            {
                var node = treeView1.SelectedNode;
                TVIndexChanged?.Invoke(this, new TVIndexChangedEventArgs(Path.Combine(ViewModel.MainDirPath, node.Text)));
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
        public event EventHandler<TVIndexChangedEventArgs> TVIndexChanged;


        private IBpmnClient _client;
        public IBpmnClient? BpmnClient
        {
            get
            {
                if (!EditorView.chromeBrowser.CanExecuteJavascriptInMainFrame)
                {
                    return null;
                }
                else
                {
                    return _client;
                }
            }
        }


        private void InitializeSubViews()
        {
            EditorView.Dock = DockStyle.Fill;
            EditorView.Parent = this.panelEditor;
 
            GraphView.Dock = DockStyle.Fill;
            GraphView.BackColor = PolokusStyle.DefaultViewColor;
            GraphView.Parent = this.panelProcessesGraph;

            XmlView.Dock = DockStyle.Fill;
            XmlView.BackColor = PolokusStyle.DefaultViewColor;
            XmlView.Parent = this.panelProcessesXml;

            ServiceView.Dock = DockStyle.Fill;
            ServiceView.BackColor = PolokusStyle.DefaultViewColor;
            ServiceView.Parent = this.panelService;

            SettingsView.Dock = DockStyle.Fill;
            SettingsView.BackColor = PolokusStyle.DefaultViewColor;
            SettingsView.Parent = this.panelSettings;
        }

        private void InitializeView()
        {

            this.panelProcesses.Visible = false;
            this.buttonSettings.Dock = DockStyle.Top;
            _processPanelVisible = false;

            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Home;
            InitializeTreeView();

            this.DoubleBuffered = true;
        }

        public Panel PanelProcessesGraph => this.panelProcessesGraph;
        public Panel PanelProcessesXml => this.panelProcessesXml;
        public Panel PanelProcessesCSharp => this.panelProcessesCsharp;
        public Panel PanelEditor => this.panelEditor;
        public Panel PanelService => this.panelService;
        public Panel PanelSettings => this.panelSettings;
        public Panel PanelHome => this.panelHome;

        public MainWindow()
        {
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            _instance = this;

            ServiceView = new ServiceView();
            EditorView = new ChromiumWindow();
            GraphView = new GraphView();
            XmlView = new XmlView();
            SettingsView = new SettingsView();

            ViewModel = new MainWindowViewModel(this);
            InitializeComponent();
            InitializeSubViews();

            InitializeView();
            ViewModel.InitializeMap();

            _client = new BpmnioClient(EditorView);

            SizeChanged += MainWindow_SizeChanged;
            splitContainer1.SplitterMoved += MainWindow_SizeChanged;

            AdjustIconBtn();


            // panelSService kills performance while resizing, therefore its live-resizing is disabled
            this.ResizeBegin += (s, e) => {
                this.panelService.SuspendLayout();
            };
            this.ResizeEnd += (s, e) => { 
                this.panelService.ResumeLayout(true);
            };


        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Tab))
            {
                ShowHideInfoLabel();
                return true;
            }

            if (keyData == (Keys.Control | Keys.D1))
            {
                ToggleLeftPanelOff();
            }

            if (keyData == (Keys.Control | Keys.D2))
            {
                ToggleLeftPanelOnLittle();
            }

            if (keyData == (Keys.Control | Keys.D3))
            {
                ToggleLeftPanelOnBig();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowHideInfoLabel()
        {
            this.panelBottom.Visible = !panelBottom.Visible;
        }

        private void ToggleLeftPanelOff()
        {
            this.splitContainer1.Panel1Collapsed = true;
        }

        private void ToggleLeftPanelOnLittle()
        {
            this.splitContainer1.Panel1Collapsed = false;
            this.splitContainer1.SplitterDistance = 100;
        }

        private void ToggleLeftPanelOnBig()
        {
            this.splitContainer1.Panel1Collapsed = false;
            this.splitContainer1.SplitterDistance = 250;
        }

        private void AdjustIconBtn()
        {
            this.iconBtnSize.FontChar =
                (this.WindowState == FormWindowState.Maximized)
                ? FontsManager.SegMDL2.Restore
                : FontsManager.SegMDL2.Maximize;
        }

        public void SetInfo(string info)
        {
            this.labelInfo.Text = info;
        }


        async Task HideProcessesPanel()
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


        async Task ShowProcessesPanel()
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




        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: // Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: // Restored form (After)
                    if (this.Padding.Top != ViewModel.BorderSize)
                        this.Padding = new Padding(ViewModel.BorderSize);
                    break;
            }
        }




        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;

            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }

            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    ViewModel.FormSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = ViewModel.FormSize;
            }
            base.WndProc(ref m);
        }

        #region Events Functions
        private async void buttonService_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Service;
            await HideProcessesPanel();
        }

        private async void buttonProcesses_Click(object sender, EventArgs e)
        {
            await ShowProcessesPanel();
        }

        private async void buttonSettings_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Settings;
            await HideProcessesPanel();
        }

        private void buttonGraphView_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesGraph;
        }

        private void buttonXmlView_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesXml;
        }

        private void buttonScriptView_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.ProcessesScript;
        }

        private async void buttonEditor_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Editor;
            await HideProcessesPanel();
        }

        private void panelLogo_Click(object sender, EventArgs e)
        {
            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Home;
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            AdjustForm();
            AdjustIconBtn();
        }

        private void iconBtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconBtnSize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                ViewModel.FormSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = ViewModel.FormSize;
            }

            AdjustIconBtn();

        }

        private void iconBtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ViewModel.FormSize = this.ClientSize;
        }

        private void MainWindow_SizeChanged(object? sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object? sender, SplitterEventArgs e)
        {
            panelPolokusHeader.Invalidate();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

    }
}
