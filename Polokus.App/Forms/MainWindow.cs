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
        private bool _processPanelVisible = false;


        private string mainDirPath = @"C:\Custom\BPMN\Polokus\Polokus.Tests\NodeHandlersTests\Bpmn";

        void InitializeTreeView()
        {
            var files = Directory.GetFiles(mainDirPath).Select(x => new FileInfo(x).Name);
            this.treeView1.Nodes.AddRange(files.Select(x => new TreeNode(x)).ToArray());
            
        }

        public MainWindow()
        {
            InitializeComponent();

            ChromiumWindow editor = new ChromiumWindow();
            editor.Parent = this.panelEditor;

            ChromiumWindow graphProcessView = new ChromiumWindow();
            graphProcessView.Parent = this.panelProcessesGraph;


            InitializeView();
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
