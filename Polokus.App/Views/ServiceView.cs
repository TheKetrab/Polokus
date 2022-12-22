using CefSharp;
using Polokus.App.Forms;
using Polokus.App.Utils;
using Polokus.Core;
using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using System.Data;
using System.Reflection;

namespace Polokus.App.Views
{
    public partial class ServiceView : UserControl
    {
        private MainWindow _mainWindow;
        public PolokusMaster PolokusMaster;
        public ChromiumWindow chromiumWindow;
        
        public ServiceView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            PolokusMaster = new PolokusMaster();
            PolokusMaster.HooksManager.RegisterHooksProvider(new AppHooksProvider(this));

            chromiumWindow = new ChromiumWindow(_mainWindow,"viewer");
            chromiumWindow.Parent = panelBpmnio;
            chromiumWindow.Dock = DockStyle.Fill;

            
            LoadBpmnFiles();

            this.listViewProcesses.SizeChanged += ListViewProcesses_SizeChanged;
            this.listViewInstances.SizeChanged += ListViewInstances_SizeChanged;
            this.listViewInstances.SelectedIndexChanged += ListViewInstances_SelectedIndexChanged;

            InitializeComboBoxWorkflows();

            LoadViewForFirstWorkflow();
        }

        private void LoadViewForFirstWorkflow()
        {
            var wf = PolokusMaster.GetWorkflows().FirstOrDefault() as Workflow;
            if (wf != null)
            {
                ActiveWorkflow = wf.Id;
                LoadViewForWorkflow(wf);
            }
        }

        private void ListViewInstances_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewInstances.SelectedItems.Count == 1)
            {
                var item = this.listViewInstances.SelectedItems[0];
                ActiveProcessInstance = item.SubItems[0].Text;

                ActiveProcessChanged();
            }
        }

        private void ListViewInstances_SizeChanged(object? sender, EventArgs e)
        {
            this.listViewInstances.Columns[0].Width = this.listViewInstances.Width - this.listViewInstances.Columns[1].Width - 25;
        }

        private void ListViewProcesses_SizeChanged(object? sender, EventArgs e)
        {
            this.listViewProcesses.Columns[0].Width = this.listViewProcesses.Width - 25;
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
            PolokusMaster.LoadXmlString(str, new FileInfo(file).Name);

        }


        public Logger? GetLoggerForOpenedInstance()
        {
            string? id = GetOpenedProcessInstanceGlobalId();
            if (id == null)
            {
                return null;
            }

            return PolokusMaster.GetOrCreateLogger(id);
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
            comboBoxWorkflows.DataSource = new BindingSource(PolokusMaster._workflows, null);
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

            var workflow = GetActiveWorkflow();
            LoadViewForWorkflow(workflow);
        }


        private void LoadViewForWorkflow(Workflow workflow)
        {
            UpdateBpmnProcessesList(workflow);
            UpdateProcessInstancesList(workflow);
            UpdateProcessStartersList(workflow);
            UpdateNodeHandlerWaitersList(workflow);
        }

        private void UpdateBpmnProcessesList(Workflow workflow)
        {
            listViewProcesses.Items.Clear();
            foreach (var process in workflow.BpmnWorkflow.BpmnProcesses)
            {
                if (process.IsManualProcess())
                {
                    this.listViewProcesses.Items.Add(process.Id);
                }
            }
        }

        public void UpdateProcessStartersListSafe(Workflow workflow)
        {
            this.listViewStarters.BeginInvoke(() => UpdateProcessStartersList(workflow));
        }

        private void UpdateProcessStartersList(Workflow? workflow)
        {
            listViewStarters.Items.Clear();
            if (workflow == null)
            {
                return;
            }

            List<Tuple<string,IProcessStarter>> starters = new();
            starters.AddRange(workflow.TimeManager.GetStarters().Select(x => Tuple.Create("Time",x)));
            starters.AddRange(workflow.MessageManager.GetStarters().Select(x => Tuple.Create("Message", x)));

            foreach (var starter in starters)
            {
                var item = new ListViewItem(starter.Item2.Id);
                item.SubItems.Add(starter.Item2.StartNode.Id);
                item.SubItems.Add(starter.Item1);

                this.listViewStarters.Items.Add(item);
            }
        }

        public void UpdateNodeHandlerWaitersListSafe(Workflow workflow)
        {
            this.listViewWaiters.BeginInvoke(() => UpdateNodeHandlerWaitersList(workflow));
        }

        private void UpdateNodeHandlerWaitersList(Workflow? workflow)
        {
            listViewWaiters.Items.Clear();
            if (workflow == null)
            {
                return;
            }

            List<Tuple<string,INodeHandlerWaiter>> waiters = new();
            waiters.AddRange(workflow.TimeManager.GetWaiters().Select(x => Tuple.Create("Time",x)));
            waiters.AddRange(workflow.MessageManager.GetWaiters().Select(x => Tuple.Create("Message", x)));

            foreach (var waiter in waiters)
            {
                var item = new ListViewItem(waiter.Item2.Id);
                item.SubItems.Add(waiter.Item2.NodeToCall.Id);
                item.SubItems.Add(waiter.Item1);

                this.listViewWaiters.Items.Add(item);
            }
        }

        public void UpdateProcessInstancesListSafe(Workflow workflow)
        {
            this.listViewInstances.BeginInvoke(() => UpdateProcessInstancesList(workflow));
        }

        private void UpdateProcessInstancesList(Workflow? workflow)
        {
            listViewInstances.Items.Clear();
            if (workflow == null)
            {
                return;
            }

            foreach (var instance in workflow.ProcessInstances)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.StatusManager.Status.ToStringExt());
                item.SubItems.Add(instance.ActiveTasksManager.GetNodeHandlers().Count().ToString());

                this.listViewInstances.Items.Add(item);
            }

            foreach (var instance in workflow.History)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.StatusManager.Status.ToStringExt());
                item.SubItems.Add(instance.ActiveTasksManager.GetNodeHandlers().Count().ToString());

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

        public Workflow GetWorkflowById(string wfId)
        {
            return (Workflow)PolokusMaster.GetWorkflow(wfId);
        }

        public Workflow? GetActiveWorkflow()
        {
            if (ActiveWorkflow == null)
            {
                return null;
            }

            return PolokusMaster.GetWorkflow(ActiveWorkflow) as Workflow;
        }

        
        private string? ActiveWorkflow { get; set; } = null;
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


        private ProcessInstance GetProcessInstance(string globalPiId)
        {
            Helpers.GetWfPiIDs(globalPiId, out string wfId, out string piId);

            Workflow workflow = PolokusMaster.GetWorkflow(wfId) as Workflow
                ?? throw new Exception("Failed to get workflow.");

            return workflow.GetProcessInstanceById(piId) as ProcessInstance
                ?? throw new Exception("Failed to get process instance.");

        }

        public void LoadGraphForProcessInstance(string globalPiId)
        {
            ProcessInstance pi = GetProcessInstance(globalPiId);
            string rawString = pi.BpmnProcess.BpmnWorkflow.RawString ?? "";

            this.chromiumWindow.chromeBrowser.ExecuteScriptAsync(
                $"window.api.openInViewerAsync('{rawString}');");

            HashSet<string> activeNodesIds = pi.AvailableNodeHandlers.Values
                .Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = pi.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(chromiumWindow, activeNodesIds, inactiveNodesIds);
        }

        public void LoadLogsForProcessInstance(string globalPiId)
        {
            this.readOnlyRichTextBox1.Text = "";

            var logger = PolokusMaster.GetOrCreateLogger(globalPiId);
            var messages = logger.GetMessages();
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

            var workflow = GetActiveWorkflow();
            var bpmnProcessId = GetSelectedSingleItem(listViewProcesses);

            if (workflow == null || bpmnProcessId == null)
            {
                throw new Exception("None BPMN process is selected.");
            }


            var processInstance = workflow.StartProcessManually(bpmnProcessId);
            string processInstanceId = processInstance.Id;
            Task.Run(async () => // TODO: usunac to brzydkie rozwiazanie! musi byc to robione od razu jak sie item doda przez hooks providera
            {
                await Task.Delay(500);
                listViewInstances.BeginInvoke(() =>
                {
                    var item = this.listViewInstances.FindItemWithText(processInstanceId);
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
            var wf = GetActiveWorkflow();
            if (wf == null)
            {
                return;
            }

            string listenerId = this.textBoxPingWaiter.Text;
            wf.MessageManager.PingListener(listenerId);
        }

    }
}
