using Polokus.App.Controls;
using Polokus.App.Fonts;
using Polokus.App.Utils;
using System.Runtime.InteropServices;

namespace Polokus.App.Forms
{
    public partial class MainWindow : Form
    {
        public readonly MainWindowViewModel ViewModel;


        public MainWindowSideMenu Menu { get; }

        public MainWindowMainPanel MainPanel { get; }

        public Panel PanelProcessesGraph => this.panelProcessesGraph;
        public Panel PanelProcessesXml => this.panelProcessesXml;
        public Panel PanelProcessesCSharp => this.panelProcessesCsharp;
        public Panel PanelEditor => this.panelEditor;
        public Panel PanelService => this.panelService;
        public Panel PanelSettings => this.panelSettings;
        public Panel PanelHome => this.panelHome;

        public Size FormSize;

        public MainWindow()
        {
            ViewModel = new MainWindowViewModel(this);
            InitializeComponent();

            Menu = new MainWindowSideMenu(this);
            Menu.Parent = this.splitContainer1.Panel1;

            MainPanel = new MainWindowMainPanel(this);


            ViewModel.ActivePanelView = MainWindowViewModel.PanelView.Home;
            this.DoubleBuffered = true;


            ViewModel.InitializeMap();


            AdjustIconBtn();

            //this.SizeChanged += (s, e) => Menu.AdjustSize(this.splitContainer1.Panel1.Size);




            // panelSService kills performance while resizing, therefore its live-resizing is disabled
            this.ResizeBegin += (s, e) => {
                this.panelService.SuspendLayout();
                this.Menu.Header.Frozen = true;
                this.MainPanel.SuspendLayout();
            };
            this.ResizeEnd += (s, e) => { 
                this.panelService.ResumeLayout(true);
                this.Menu.Header.Frozen = false;
                this.Menu.Header.Invalidate();
                this.MainPanel.ResumeLayout();
                Menu.AdjustSize(this.splitContainer1.Panel1.Size);
            };

            this.splitContainer1.SplitterMoved += (s, e) =>
            {
                Menu.AdjustSize(this.splitContainer1.Panel1.Size);
            };

            Menu.AdjustSize(this.splitContainer1.Panel1.Size);


        }

        public void SetConnectionToLabelText(string text)
        {
            this.labelConnectionTo.Text = text;
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
            if (m.Msg == Win32Manager.WM_NCHITTEST)
            {
                base.WndProc(ref m);
                Win32Manager.HandleMsgResize(this, ref m);                
                return;
            }

            if (m.Msg == Win32Manager.WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                // Remove border and keep snap window
                return;
            }

            if (m.Msg == Win32Manager.WM_SYSCOMMAND)
            {
                Win32Manager.HandleRestoringSize(this, ref m);
                base.WndProc(ref m);
                return;
            }

            base.WndProc(ref m);

        }

        #region Events Functions
        
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
                this.FormSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = this.FormSize;
            }

            AdjustIconBtn();

        }

        private void iconBtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.FormSize = this.ClientSize;
        }

        private void splitContainer1_SplitterMoved(object? sender, SplitterEventArgs e)
        {
            this.Menu.Invalidate();
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
