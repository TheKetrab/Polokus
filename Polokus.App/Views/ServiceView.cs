﻿using CefSharp;
using Polokus.App.Forms;
using Polokus.App.Utils;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Communication;
using System.Data;
using System.Reflection;

namespace Polokus.App.Views
{
    public partial class ServiceView : UserControl
    {
        private MainWindow _mainWindow;
        public ChromiumWindow chromiumWindow;

        IServicesProvider _services => PolokusApp.ServicesProvider;


        public ServiceView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            chromiumWindow = new ChromiumWindow(_mainWindow, "viewer");
            chromiumWindow.Parent = panelBpmnio;
            chromiumWindow.Dock = DockStyle.Fill;

            InitializeListsEvents();
            InitializeComboBoxWorkflows();
            SetDefaultTextForScripting();

            AnyBpmnProcessSelected = false;
            AnyProcessInstanceSelected = false;

            SetExperimentals();
        }

        private void SetExperimentals()
        {
            bool enabled = Settings.ExperimentalFunctions;

            this.panel7.Enabled = enabled;
            this.panel7.Visible = enabled;
        }

        private void InitializeListsEvents()
        {
            this.listViewProcesses.SizeChanged += ListViewProcesses_SizeChanged;
            this.listViewProcesses.SelectedIndexChanged += ListViewProcesses_SelectedIndexChanged;

            this.listViewInstances.SizeChanged += ListViewInstances_SizeChanged;
            this.listViewInstances.SelectedIndexChanged += ListViewInstances_SelectedIndexChanged;
            this.listViewStarters.SizeChanged += ListViewStarters_SizeChanged;
            this.listViewWaiters.SizeChanged += ListViewWaiters_SizeChanged;
        }


        private void ListViewProcesses_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.listViewProcesses.SelectedItems.Count == 1)
            {
                AnyBpmnProcessSelected = true;
            }
            else
            {
                AnyBpmnProcessSelected = false;
            }
        }

        private bool _anyBpmnProcessSelected;
        public bool AnyBpmnProcessSelected
        {
            get => _anyBpmnProcessSelected;
            set
            {
                _anyBpmnProcessSelected = value;
                this.buttonAdd.Enabled = value;
            }
        }

        private bool _anyProcessInstanceSelected;
        public bool AnyProcessInstanceSelected
        {
            get => _anyProcessInstanceSelected;
            set
            {
                _anyProcessInstanceSelected = value;
                this.buttonPause.Enabled = value;
                this.buttonStop.Enabled = value;
                this.buttonStart.Enabled = value;
            }
        }

        public void LoadViewForFirstWorkflowSafe()
        {
            this.chromiumWindow.BeginInvoke(() => LoadViewForFirstWorkflow());
        }

        private void LoadViewForFirstWorkflow()
        {
            var wfId = _services.PolokusService.GetWorkflowsIds().FirstOrDefault();
            if (wfId == null)
            {
                return;
            }

            ActiveWorkflow = wfId;
            LoadViewForWorkflow(wfId);
        }

        private void ListViewInstances_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewInstances.SelectedItems.Count == 1)
            {
                var item = this.listViewInstances.SelectedItems[0];
                ActiveProcessInstance = item.SubItems[0].Text;

                ActiveProcessChanged();
                AnyProcessInstanceSelected = true;
            }
            else
            {
                AnyProcessInstanceSelected = false;
            }
        }

        private void ListViewInstances_SizeChanged(object? sender, EventArgs e)
        {
            int maxWidth = this.listViewInstances.Width - 10;

            int c2Width = (int)(0.2 * maxWidth);
            this.listViewInstances.Columns[2].Width = c2Width;

            int c1Width = (int)(0.2 * maxWidth);
            this.listViewInstances.Columns[1].Width = c1Width;

            this.listViewInstances.Columns[0].Width = maxWidth - c2Width - c1Width;
        }

        private void ListViewWaiters_SizeChanged(object? sender, EventArgs e)
        {
            int maxWidth = this.listViewWaiters.Width - 10;

            int c2Width = (int)(0.25 * maxWidth);
            this.listViewWaiters.Columns[2].Width = c2Width;

            int c1Width = (int)(0.3 * maxWidth);
            this.listViewWaiters.Columns[1].Width = c1Width;

            this.listViewWaiters.Columns[0].Width = maxWidth - c2Width - c1Width;
        }

        private void ListViewStarters_SizeChanged(object? sender, EventArgs e)
        {
            int maxWidth = this.listViewStarters.Width - 10;

            int c2Width = (int)(0.25 * maxWidth);
            this.listViewStarters.Columns[2].Width = c2Width;

            int c1Width = (int)(0.3 * maxWidth);
            this.listViewStarters.Columns[1].Width = c1Width;

            this.listViewStarters.Columns[0].Width = maxWidth - c2Width - c1Width;
        }

        private void ListViewProcesses_SizeChanged(object? sender, EventArgs e)
        {
            int maxWidth = this.listViewProcesses.Width - 10;
            this.listViewProcesses.Columns[0].Width = maxWidth;
        }


        private void LoadXmlFile(string file)
        {
            string str = File.ReadAllText(file);
            string name = new FileInfo(file).Name;
            _services.PolokusService.LoadXmlString(str, name);
            _loadedWorkflows.Add(name);
        }

        /// <summary>
        /// Method refreshes logs and diagram views for current active process instance.
        /// </summary>
        private void ActiveProcessChanged()
        {
            string? activeGlobalPiId = GetOpenedProcessInstanceGlobalId();
            if (activeGlobalPiId == null)
            {
                return;
            }

            LoadLogsForProcessInstance(activeGlobalPiId);
            LoadGraphForProcessInstance(activeGlobalPiId);
        }

        private List<string> _loadedWorkflows = new();
        private void InitializeComboBoxWorkflows()
        {
            var workflows = _services.PolokusService.GetWorkflowsIds();
            foreach (var wf in workflows)
            {
                _loadedWorkflows.Add(wf);
            }

            comboBoxWorkflows.DataSource = new BindingSource(this._loadedWorkflows, null);
            comboBoxWorkflows.SelectedIndexChanged += comboBoxWorkflows_SelectedIndexChanged;
        }

        private void comboBoxWorkflows_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxWorkflows.SelectedItem is string wf)
            {
                ActiveWorkflow = wf;
            }

            if (ActiveWorkflow == null)
            {
                return;
            }

            LoadViewForWorkflow(ActiveWorkflow);
        }


        private void LoadViewForWorkflow(string wfId)
        {
            UpdateBpmnProcessesList(wfId);
            UpdateProcessInstancesList(wfId);
            UpdateProcessStartersList(wfId);
            UpdateNodeHandlerWaitersList(wfId);

            string rawString = _services.WorkflowsService.GetRawString(wfId);
            LoadBpmnGraph(rawString);
        }

        private void UpdateBpmnProcessesList(string wfId)
        {
            listViewProcesses.Items.Clear();
            var processes = _services.WorkflowsService.GetManualBpmnProcessesIds(wfId);
            foreach (var processId in processes)
            {
                this.listViewProcesses.Items.Add(processId);
            }
        }

        public void UpdateProcessStartersListSafe(string wfId)
        {
            this.listViewStarters.BeginInvoke(() => UpdateProcessStartersList(wfId));
        }

        private void UpdateProcessStartersList(string wfId)
        {
            listViewStarters.Items.Clear();

            var starters = _services.WorkflowsService.GetProcessStarters(wfId);

            foreach (var starter in starters)
            {
                var item = new ListViewItem(starter.Id);
                item.SubItems.Add(starter.StartNode);
                item.SubItems.Add(starter.StarterType);

                this.listViewStarters.Items.Add(item);
            }
        }

        public void UpdateNodeHandlerWaitersListSafe(string wfId)
        {
            this.listViewWaiters.BeginInvoke(() => UpdateNodeHandlerWaitersList(wfId));
        }

        private void UpdateNodeHandlerWaitersList(string wfId)
        {
            listViewWaiters.Items.Clear();

            var waiters = _services.WorkflowsService.GetNodeHandlerWaiters(wfId);
            foreach (var waiter in waiters)
            {
                var item = new ListViewItem(waiter.Id);
                item.SubItems.Add(waiter.NodeToCall);
                item.SubItems.Add(waiter.WaiterType);

                this.listViewWaiters.Items.Add(item);
            }
        }

        public void UpdateProcessInstancesListSafe(string wfId)
        {
            this.listViewInstances.BeginInvoke(() => UpdateProcessInstancesList(wfId));
        }

        private void UpdateProcessInstancesList(string wfId)
        {
            listViewInstances.Items.Clear();
            var processInstances = _services.WorkflowsService.GetProcessInstancesInfos(wfId);

            foreach (var instance in processInstances)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.Status);
                item.SubItems.Add(instance.ActiveTasks);

                this.listViewInstances.Items.Add(item);
            }
        }


        private string? GetSelectedSingleItem(System.Windows.Forms.ListView listView)
        {
            if (listView.SelectedItems.Count != 1)
            {
                return null;
            }

            return listView.SelectedItems[0].Text;
        }

        public string? ActiveWorkflow { get; private set; } = null;
        private string? ActiveProcessInstance { get; set; } = null;
        public string? GetOpenedProcessInstanceGlobalId()
        {
            if (ActiveWorkflow == null || ActiveProcessInstance == null)
            {
                return null;
            }

            return Helpers.GetGlobalProcessInstanceId(ActiveWorkflow, ActiveProcessInstance);
        }

        public void AppendLogLineSafe(string line)
        {
            this.BeginInvoke(() =>
            {
                string br = string.IsNullOrEmpty(this.readOnlyRichTextBox1.Text) ? "" : "\n";
                this.readOnlyRichTextBox1.AppendText(br + line);
            });
        }




        private void LoadBpmnGraph(string rawString)
        {
            if (!this.chromiumWindow.chromeBrowser.IsBrowserInitialized)
            {
                return;
            }

            Task.Run(async () =>
            {
                var res = await this.chromiumWindow.chromeBrowser.EvaluateScriptAsync(
                    $"window.api.openInViewerAsync('{rawString}');");

                if (!res.Success)
                {
                    Logger.Global.LogError(res.Message);
                }
            });
        }

        public void LoadGraphForProcessInstance(string globalPiId)
        {
            Helpers.GetWfPiIDs(globalPiId, out string wfId, out string piId);
            string rawString = _services.WorkflowsService.GetRawString(wfId);
            LoadBpmnGraph(rawString);

            HashSet<string> activeNodesIds = _services.ProcessInstancesService.GetActiveNodesIds(wfId, piId).ToHashSet();

            var allNodesIds = _services.ProcessInstancesService.GetAllNodesIds(wfId, piId);
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));
            Thread.Sleep(100); // strage behaviour: without this colours were not set
            BpmnioClient.SetColours(chromiumWindow, activeNodesIds, inactiveNodesIds);
        }

        public void LoadLogsForProcessInstance(string globalPiId)
        {
            this.readOnlyRichTextBox1.Text = "";

            Helpers.GetWfPiIDs(globalPiId, out string wfId, out string piId);
            var messages = _services.LogsService.GetMessages(wfId, piId);
            foreach (var message in messages)
            {
                string br = string.IsNullOrEmpty(this.readOnlyRichTextBox1.Text) ? "" : "\n";

                switch (message.Item1)
                {
                    case MsgType.Simple:
                        readOnlyRichTextBox1.AppendText(br + message.Item2);
                        break;
                    case MsgType.Warning:
                        readOnlyRichTextBox1.AppendFormattedText(br + message.Item2, Color.Orange, false);
                        break;
                    case MsgType.Error:
                        readOnlyRichTextBox1.AppendFormattedText(br + message.Item2, Color.Red, true);
                        break;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // add new process instance

            var wfId = ActiveWorkflow;
            var bpmnProcessId = GetSelectedSingleItem(listViewProcesses);

            if (wfId == null || bpmnProcessId == null)
            {
                throw new Exception("None BPMN process is selected.");
            }

            _services.WorkflowsService.StartProcessManually(wfId, bpmnProcessId);
        }

        private void buttonLoadWorkflow_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                openFileDialog.Filter = "Bpmn files (*.txt, *.bpmn)|*.txt,*.bpmn|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string name = Path.GetFileName(filePath);
                    string newFilePath = Path.Combine(Settings.BpmnPath, name);
                    File.Copy(filePath, newFilePath);
                    LoadXmlFile(newFilePath);
                }
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            var wfId = ActiveWorkflow;
            var piId = ActiveProcessInstance;

            if (wfId == null || piId == null)
            {
                throw new Exception("None process instance is selected.");
            }

            _services.WorkflowsService.PauseProcessInstance(wfId, piId);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            var wfId = ActiveWorkflow;
            var piId = ActiveProcessInstance;

            if (wfId == null || piId == null)
            {
                throw new Exception("None process instance is selected.");
            }

            _services.WorkflowsService.StopProcessInstance(wfId, piId);

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var wfId = ActiveWorkflow;
            var piId = ActiveProcessInstance;

            if (wfId == null || piId == null)
            {
                throw new Exception("None process instance is selected.");
            }

            _services.WorkflowsService.ResumeProcessInstance(wfId, piId);
        }

        private void buttonPingWaiter_Click(object sender, EventArgs e)
        {
            if (ActiveWorkflow == null)
            {
                throw new Exception("None workflow is active.");
            }

            string listenerId = this.textBoxPingWaiter.Text;
            _services.WorkflowsService.PingListener(ActiveWorkflow, listenerId);
        }




        private void buttonRaiseSignal_Click(object sender, EventArgs e)
        {
            if (ActiveWorkflow == null)
            {
                throw new Exception("None workflow is active.");
            }

            string signal = this.textBoxRaiseSignal.Text;
            _services.WorkflowsService.RaiseSignal(ActiveWorkflow, signal);

        }

        private void btnScriptingRunScript_Click(object sender, EventArgs e)
        {
            string script = this.textBoxScripting.Text;

            string funcname = script.Substring(0, script.IndexOf('('));
            string argspart = script[(script.IndexOf('(') + 1)..(script.IndexOf(')'))];

            string[][] args = argspart.Split(',')
                .Select(x => x.Split(':', StringSplitOptions.TrimEntries))
                .ToArray();

            switch (funcname)
            {
                case "StartProcessManually":
                    {
                        if (args[0][0] == "id" && args[1][0] == "count")
                        {
                            var wfId = ActiveWorkflow ?? throw new Exception();
                            string bpmnProcessId = args[0][1];
                            int count = int.Parse(args[1][1]);

                            for (int i = 0; i < count; i++)
                            {
                                _services.WorkflowsService.StartProcessManually(wfId, bpmnProcessId);
                            }

                        }
                        else
                        {
                            throw new Exception("Invalid arguments");
                        }
                        break;
                    }
                default:
                    throw new Exception();
            }
        }

        private void SetDefaultTextForScripting()
        {
            this.textBoxScripting.Text = "StartProcessManually(id:___,count:1)";
        }
    }
}
