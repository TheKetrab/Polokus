using CefSharp;
using Polokus.App.Forms;
using Polokus.App.Utils;
using Polokus.Core;
using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Remote;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace Polokus.App.Views
{
    public partial class ServiceView : UserControl
    {
        private MainWindow _mainWindow;
        //public PolokusMaster PolokusMaster;
        public ChromiumWindow chromiumWindow;

        public RemoteLogsService RemoteLogsService = new(); // TODO service
        public RemoteProcessInstancesService RemoteProcessInstancesService = new(); // TODO
        public RemotePolokusService RemotePolokusService = new(); // TODO
        public RemoteWorkflowsService RemoteWorkflowsService = new(); // TODO

        public ServiceView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            chromiumWindow = new ChromiumWindow(_mainWindow,"viewer");
            chromiumWindow.Parent = panelBpmnio;
            chromiumWindow.Dock = DockStyle.Fill;

            
            LoadBpmnFiles();

            this.listViewProcesses.SizeChanged += ListViewProcesses_SizeChanged;
            this.listViewProcesses.SelectedIndexChanged += ListViewProcesses_SelectedIndexChanged;

            this.listViewInstances.SizeChanged += ListViewInstances_SizeChanged;
            this.listViewInstances.SelectedIndexChanged += ListViewInstances_SelectedIndexChanged;
            this.listViewStarters.SizeChanged += ListViewStarters_SizeChanged;
            this.listViewWaiters.SizeChanged += ListViewWaiters_SizeChanged;

            AnyBpmnProcessSelected = false;
            AnyProcessInstanceSelected = false;

            InitializeComboBoxWorkflows();

            LoadViewForFirstWorkflow();
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
                this.buttonRestart.Enabled = value;
                this.buttonStop.Enabled = value;
                this.buttonDelete.Enabled = value;
                this.buttonStart.Enabled = value;
            }
        }

        private void LoadViewForFirstWorkflow()
        {
            var wfId = RemotePolokusService.GetWorkflowsIds().FirstOrDefault();
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

        private void LoadBpmnFiles()
        {
            string bpmnDir = Properties.Settings.Default.BpmnPath;
            if (!Directory.Exists(bpmnDir))
            {
                string msg = $"BpmnPath {bpmnDir} does not exists. Do you want to create this directory?";
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Directory.CreateDirectory(bpmnDir);
                }
                else
                {
                    return;
                }
            }

            var files = Directory.GetFiles(Properties.Settings.Default.BpmnPath);
            foreach (var file in files)
            {
                LoadXmlFile(file);
            }

        }

        private void LoadXmlFile(string file)
        {
            string str = File.ReadAllText(file);
            RemotePolokusService.LoadXmlString(str, new FileInfo(file).Name);
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


        private void InitializeComboBoxWorkflows()
        {
            // TODO: comboBoxWorkflows.DataSource = new BindingSource(PolokusMaster._workflows, null);
            comboBoxWorkflows.DisplayMember = "Key";
            comboBoxWorkflows.ValueMember = "Value";
            comboBoxWorkflows.SelectedIndexChanged += comboBoxWorkflows_SelectedIndexChanged;
        }

        private void comboBoxWorkflows_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxWorkflows.SelectedItem is KeyValuePair<string, IWorkflow> wf)
            {
                ActiveWorkflow = wf.Key;
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

            string rawString = RemoteWorkflowsService.GetRawString(wfId);
            LoadBpmnGraph(rawString);
        }

        private void UpdateBpmnProcessesList(string wfId)
        {
            listViewProcesses.Items.Clear();
            var processes = RemoteWorkflowsService.GetManualBpmnProcessesIds(wfId);
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

            var starters = RemoteWorkflowsService.GetProcessStarters(wfId);

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

            var waiters = RemoteWorkflowsService.GetNodeHandlerWaiters(wfId);
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
            var processInstances = RemoteWorkflowsService.GetProcessInstancesInfos(wfId);

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

            this.chromiumWindow.chromeBrowser.ExecuteScriptAsync(
                $"window.api.openInViewerAsync('{rawString}');");
        }

        public void LoadGraphForProcessInstance(string globalPiId)
        {
            Helpers.GetWfPiIDs(globalPiId, out string wfId, out string piId);
            string rawString = RemoteWorkflowsService.GetRawString(wfId);
            LoadBpmnGraph(rawString);

            HashSet<string> activeNodesIds = RemoteProcessInstancesService.GetActiveNodesIds(wfId, piId).ToHashSet();

            var allNodesIds = RemoteProcessInstancesService.GetAllNodesIds(wfId, piId);
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(chromiumWindow, activeNodesIds, inactiveNodesIds);
        }

        public void LoadLogsForProcessInstance(string globalPiId)
        {
            this.readOnlyRichTextBox1.Text = "";

            Helpers.GetWfPiIDs(globalPiId, out string wfId, out string piId);
            var messages = RemoteLogsService.GetMessages(wfId, piId);
            foreach (var message in messages)
            {
                string br = string.IsNullOrEmpty(this.readOnlyRichTextBox1.Text) ? "" : "\n";

                switch (message.Item1)
                {
                    case Logger.MsgType.Simple:
                        readOnlyRichTextBox1.AppendText(br + message.Item2);
                        break;
                    case Logger.MsgType.Warning:
                        readOnlyRichTextBox1.AppendFormattedText(br + message.Item2, Color.Orange, false);
                        break;
                    case Logger.MsgType.Error:
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


            string piId = RemoteWorkflowsService.StartProcessManually(wfId, bpmnProcessId);
            Task.Run(async () => // TODO: usunac to brzydkie rozwiazanie! musi byc to robione od razu jak sie item doda przez hooks providera
            {
                await Task.Delay(500);
                listViewInstances.BeginInvoke(() =>
                {
                    var item = this.listViewInstances.FindItemWithText(piId);
                    int idx = this.listViewInstances.Items.IndexOf(item);
                    if (idx != -1)
                    {
                        this.listViewInstances.Items[idx].Focused = true;
                        this.listViewInstances.Items[idx].Selected = true;
                    }
                });
            });
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
                    string newFilePath = Path.Combine(Properties.Settings.Default.BpmnPath, name);
                    File.Copy(filePath, newFilePath);
                    LoadXmlFile(newFilePath);
                }
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void buttonPingWaiter_Click(object sender, EventArgs e)
        {            
            if (ActiveWorkflow == null)
            {
                throw new Exception("None workflow is active.");
            }

            string listenerId = this.textBoxPingWaiter.Text;
            RemoteWorkflowsService.PingListener(ActiveWorkflow, listenerId);
        }

    }
}
