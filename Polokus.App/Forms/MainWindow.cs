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
                return _instance;
            }
        }

        private ServiceView _serviceView;
        public ServiceView GetServiceView()
        {
            return _serviceView;
        }


        Size formSize;
        int borderSize = 2;
        //Overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
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
            #endregion
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
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        


  


        public enum PanelView
        {
            None,
            ProcessesGraph,
            ProcessesXml,
            ProcessesScript,
            Editor,
            Settings,
            Service,
            Home
        }

        private PanelView _activePanelView;
        public PanelView ActivePanelView => _activePanelView;
        private bool _processPanelVisible = false;


        private string mainDirPath = @"C:\Custom\BPMN\Polokus\Polokus.Tests\NodeHandlersTests\Bpmn";

        void InitializeTreeView()
        {
            var files = Directory.GetFiles(mainDirPath).Select(x => new FileInfo(x).Name);
            this.treeView1.Nodes.AddRange(files.Select(x => new TreeNode(x)).ToArray());


            this.treeView1.AfterSelect += (s, e) =>
            {
                var node = treeView1.SelectedNode;
                TVIndexChanged?.Invoke(this, new TVIndexChangedEventArgs(Path.Combine(mainDirPath, node.Text)));
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

        private GraphView processesGraphView;
        private ChromiumWindow editor;


        private IBpmnClient _client;
        public IBpmnClient? BpmnClient
        {
            get
            {
                if (!editor.chromeBrowser.CanExecuteJavascriptInMainFrame)
                {
                    return null;
                }
                else
                {
                    return _client;
                }
            }
        }


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public MainWindow()
        {

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;



            _instance = this;
            InitializeComponent();

            editor = new ChromiumWindow();
            editor.Dock = DockStyle.Fill;
            editor.Parent = this.panelEditor;
            _client = new BpmnioClient(editor);

            //ChromiumWindow graphProcessView = new ChromiumWindow();
            //graphProcessView.Parent = this.panelProcessesGraph;

            processesGraphView = new GraphView();
            processesGraphView.Dock = DockStyle.Fill;
            processesGraphView.BackColor = PolokusStyle.DefaultViewColor;
            processesGraphView.Parent = this.panelProcessesGraph;

            var processesXmlView = new XmlView();
            processesXmlView.Dock = DockStyle.Fill;
            processesXmlView.BackColor = PolokusStyle.DefaultViewColor;
            processesXmlView.Parent = this.panelProcessesXml;

            _serviceView = new ServiceView();
            _serviceView.Dock = DockStyle.Fill;
            _serviceView.BackColor = PolokusStyle.DefaultViewColor;
            _serviceView.Parent = this.panelService;

            InitializeView();


            SizeChanged += MainWindow_SizeChanged;
            splitContainer1.SplitterMoved += MainWindow_SizeChanged;


            //this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.ResizeRedraw, true);

            AdjustIconBtn();
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


        private void MainWindow_SizeChanged(object? sender, EventArgs e)
        {
            panelPolokusHeader.Invalidate();

            //this.labelInfo.Text = $"Window: {Width}x{Height} ; {30,' '} Left: {splitContainer1.Panel1.Width}x{splitContainer1.Panel1.Height} ; {30,' '} Right: {splitContainer1.Panel2.Width}x{splitContainer1.Panel2.Height}";

            //processesGraphView.Width = splitContainer1.Panel2.Width;
            //processesGraphView.Height = splitContainer1.Panel2.Height;
        }

        async Task HideProcessesPanel()
        {
            if (!_processPanelVisible)
            {
                return;
            }

            this.panelProcesses.Visible = true;
            this.panelProcesses.Dock = DockStyle.Top;

            await AnimateProperty(this.panelProcesses,
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

            await AnimateProperty(this.panelProcesses,
                (f) => { this.panelProcesses.Height = (int)f; },
                0, ProcessPanelHeight, 100);

            this.panelProcesses.Dock = DockStyle.Fill;
            this.buttonSettings.Dock = DockStyle.Bottom;
            _processPanelVisible = true;
        }

        private async Task AnimateProperty(Control control, Action<float> update, int from, int to, int ms)
        {
            const int timestep = 15; // depends on system tick time, Windows ~15ms
            float step = timestep * (float)(to - from) / (float)ms;

            update(from);

            Func<float,int,bool> relation = from < to
                ? (float f, int i) => f < i
                : (float f, int i) => f > i;

            for (float current = from; relation(current,to); current += step)
            {
                await Task.Delay(timestep);
                update(current);
            }
            update(to);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
        }

        void SetProcessesView(PanelView view)
        {

            switch (view)
            {
                case PanelView.ProcessesXml:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = true;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = false;
                    panelService.Visible = false;
                    panelSettings.Visible = false;
                    panelHome.Visible = false;
                    break;

                case PanelView.ProcessesScript:
                    panelProcessesCsharp.Visible = true;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = false;
                    panelService.Visible = false;
                    panelSettings.Visible = false;
                    panelHome.Visible = false;
                    break;

                case PanelView.ProcessesGraph:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = true;
                    panelEditor.Visible = false;
                    panelService.Visible = false;
                    panelSettings.Visible = false;
                    panelHome.Visible = false;
                    break;

                case PanelView.None:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = false;
                    panelService.Visible = false;
                    panelSettings.Visible = false;
                    panelHome.Visible = false;
                    break;

                case PanelView.Editor:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = true;
                    panelService.Visible = false;
                    panelSettings.Visible = false;
                    panelHome.Visible = false;
                    break;

                case PanelView.Service:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = false;
                    panelService.Visible = true;
                    panelSettings.Visible = false;
                    panelHome.Visible = false;
                    break;

                case PanelView.Settings:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = false;
                    panelService.Visible = false;
                    panelSettings.Visible = true;
                    panelHome.Visible = false;
                    break;

                case PanelView.Home:
                    panelProcessesCsharp.Visible = false;
                    panelProcessesXml.Visible = false;
                    panelProcessesGraph.Visible = false;
                    panelEditor.Visible = false;
                    panelService.Visible = false;
                    panelSettings.Visible = false;
                    panelHome.Visible = true;
                    break;
            }

            _activePanelView = view;
        }

        void InitializeView()
        {
            this.panelProcesses.Visible = false;
            this.buttonSettings.Dock = DockStyle.Top;
            _processPanelVisible = false;

            SetProcessesView(PanelView.Home);
            InitializeTreeView();
        }

        private async void buttonService_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.Service);
            await HideProcessesPanel();
        }

        private async void buttonProcesses_Click(object sender, EventArgs e)
        {
            await ShowProcessesPanel();
        }

        private async void buttonSettings_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.Settings);
            await HideProcessesPanel();
        }

        private void buttonGraphView_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.ProcessesGraph);
        }

        private void buttonXmlView_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.ProcessesXml);
        }

        private void buttonScriptView_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.ProcessesScript);
        }

        private async void buttonEditor_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.Editor);
            await HideProcessesPanel();
        }

        private void panelLogo_Click(object sender, EventArgs e)
        {
            SetProcessesView(PanelView.Home);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            AdjustForm();
            AdjustIconBtn();
        }
        //Private methods
        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Restored form (After)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void iconBtn3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconBtn2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
            }

            AdjustIconBtn();

        }

        private void iconBtn1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
