using Polokus.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public ChromiumWindow BpmnioClient { get; }
        public MainWindow()
        {
            _instance = this;
            InitializeComponent();

            editor = new ChromiumWindow();
            editor.Dock = DockStyle.Fill;
            BpmnioClient = editor;
            editor.Parent = this.panelEditor;

            //ChromiumWindow graphProcessView = new ChromiumWindow();
            //graphProcessView.Parent = this.panelProcessesGraph;

            processesGraphView = new GraphView();
            processesGraphView.Dock = DockStyle.Fill;
            processesGraphView.BackColor = Color.Yellow;
            processesGraphView.Parent = this.panelProcessesGraph;

            var processesXmlView = new XmlView();
            processesXmlView.Dock = DockStyle.Fill;
            processesXmlView.BackColor = Color.Green;
            processesXmlView.Parent = this.panelProcessesXml;

            InitializeView();


            SizeChanged += MainWindow_SizeChanged;
            splitContainer1.SplitterMoved += MainWindow_SizeChanged;
        }

        public void SetInfo(string info)
        {
            this.labelInfo.Text = info;
        }

        private void MainWindow_SizeChanged(object? sender, EventArgs e)
        {
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

            int initHeight = this.panelProcesses.Height;
            for (int i=initHeight; i >= 0; i-=40)
            {
                await Task.Delay(1);
                this.panelProcesses.Height = i;
            }


            this.panelProcesses.Visible = false;
            this.buttonSettings.Dock = DockStyle.Top;
            _processPanelVisible = false;
        }

        async Task ShowProcessesPanel()
        {
            if (_processPanelVisible)
            {
                return;
            }

            this.panelProcesses.Height = 0;
            this.panelProcesses.Dock = DockStyle.Top;
            this.panelProcesses.Visible = true;

            this.buttonSettings.Dock = DockStyle.Top;

            int destHeight = this.panelSideMenu.Height
                - panelLogo.Height
                - buttonSettings.Height
                - buttonEditor.Height
                - buttonService.Height
                - buttonProcesses.Height;

            for (int i=0; i <= destHeight; i+= 40)
            {
                await Task.Delay(1);
                this.panelProcesses.Height = i;
            }

            this.panelProcesses.Dock = DockStyle.Fill;
            this.buttonSettings.Dock = DockStyle.Bottom;
            _processPanelVisible = true;
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

    }
}
