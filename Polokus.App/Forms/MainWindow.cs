using Polokus.App.Utils;
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

        private ServiceView _serviceView;
        public ServiceView GetServiceView()
        {
            return _serviceView;
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
        public MainWindow()
        {
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
            processesGraphView.BackColor = Color.Yellow;
            processesGraphView.Parent = this.panelProcessesGraph;

            var processesXmlView = new XmlView();
            processesXmlView.Dock = DockStyle.Fill;
            processesXmlView.BackColor = Color.Green;
            processesXmlView.Parent = this.panelProcessesXml;

            _serviceView = new ServiceView();
            _serviceView.Dock = DockStyle.Fill;
            _serviceView.BackColor = Color.Orange;
            _serviceView.Parent = this.panelService;

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
