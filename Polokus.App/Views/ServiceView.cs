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


            chromiumWindow = new ChromiumWindow(_mainWindow,"viewer");
            chromiumWindow.Parent = panelBpmnio;
            chromiumWindow.Dock = DockStyle.Fill;

            PolokusMaster = new PolokusMaster();
            PolokusMaster.HooksManager.RegisterHooksProvider(new AppHooksProvider(this));
            LoadBpmnFiles();

            foreach (var iwf in PolokusMaster.GetWorkflows())
            {
                Workflow wf = (Workflow)iwf;
                ((TimeManager)(wf.TimeManager)).CallersChanged += (s, e) =>
                {
                    if (listViewWaiters.IsHandleCreated && listViewStarters.IsHandleCreated)
                    {
                        listViewWaiters.BeginInvoke(() => UpdateNodeHandlerWaitersList(wf));
                        listViewStarters.BeginInvoke(() => UpdateProcessStartersList(wf));
                    }
                };
                ((MessageManager)(wf.MessageManager)).CallersChanged += (s, e) =>
                {
                    if (listViewWaiters.IsHandleCreated && listViewStarters.IsHandleCreated)
                    {
                        listViewWaiters.BeginInvoke(() => UpdateNodeHandlerWaitersList(wf));
                        listViewStarters.BeginInvoke(() => UpdateProcessStartersList(wf));
                    }
                };

            }

            this.listViewProcesses.SizeChanged += (s, e) =>
            {
                this.listViewProcesses.Columns[0].Width = this.listViewProcesses.Width - 25;
            };
            this.listViewInstances.SizeChanged += (s, e) =>
            {
                this.listViewInstances.Columns[0].Width = this.listViewInstances.Width - this.listViewInstances.Columns[1].Width - 25;
            };

            this.listViewInstances.SelectedIndexChanged += (s, e) =>
            {
                if (listViewInstances.SelectedItems.Count == 1)
                {
                    var item = this.listViewInstances.SelectedItems[0];
                    ActiveProcessInstance = item.SubItems[0].Text;

                    ActiveProcessChanged();
                }
            };


            InitializeComboBoxWorkflows();

            UpdateNodeHandlerWaitersList((Workflow?)PolokusMaster.GetWorkflows().FirstOrDefault());
        }

        private void LoadBpmnFiles()
        {
            string bpmnDir = Properties.Settings.Default.BpmnPath;
            if (!Directory.Exists(bpmnDir))
            {
                if (MessageBox.Show($"BpmnPath {bpmnDir} does not exists. Do you want to create this directory?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

            if (!_comboBoxWorkflowsInitialized)
            {
                InitializeComboBoxWorkflows();
            }

        }


        private Dictionary<string, Logger> _logs = new();

        public Logger GetOrCreateLogger(string globalId)
        {
            if (_logs.ContainsKey(globalId))
            {
                return _logs[globalId];
            }
            else
            {
                var newLogger = new Logger();
                _logs.Add(globalId, new Logger());
                return newLogger;
            }
        }
        public Logger? GetLoggerForOpenedInstance()
        {
            string? id = GetOpenedProcessInstanceGlobalId();
            if (id == null)
            {
                return null;
            }

            return GetOrCreateLogger(id);
        }

        private void ActiveProcessChanged()
        {
            string activeProcess = GetOpenedProcessInstanceGlobalId();

            LoadLogsForProcessInstance(activeProcess);
            LoadGraphForProcessInstance(activeProcess);
        }

        private bool _comboBoxWorkflowsInitialized = false;
        private void InitializeComboBoxWorkflows()
        {
            if (!PolokusMaster.GetWorkflows().Any())
            {
                return;
            }

            comboBoxWorkflows.DataSource = new BindingSource(PolokusMaster._workflows, null);
            comboBoxWorkflows.DisplayMember = "Key";
            comboBoxWorkflows.ValueMember = "Value";

            comboBoxWorkflows.SelectedIndexChanged += comboBoxWorkflows_SelectedIndexChanged;

            comboBoxWorkflows_SelectedIndexChanged(null, EventArgs.Empty);
            _comboBoxWorkflowsInitialized = true;
        }

        private void comboBoxWorkflows_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var workflow = GetActiveWorkflow();
            if (comboBoxWorkflows.SelectedItem is KeyValuePair<string, IWorkflow> wf)
            {
                ActiveWorkflow = wf.Key;
            }

            LoadViewForWorkflow(workflow);
        }

        private void LoadViewForWorkflow(Workflow workflow)
        {
            listViewProcesses.Items.Clear();
            foreach (var process in workflow.BpmnWorkflow.BpmnProcesses)
            {
                this.listViewProcesses.Items.Add(process.Id);
            }

            UpdateProcessInstancesList(workflow);

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

        public void UpdateProcessInstancesList(Workflow? workflow)
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
            object si;
            if (comboBoxWorkflows.InvokeRequired)
            {
                si = comboBoxWorkflows.Invoke(() => comboBoxWorkflows.SelectedItem);
            }
            else
            {
                si = comboBoxWorkflows.SelectedItem;
            }

            if (si == null)
            {
                return null;
            }

            if (si is KeyValuePair<string, IWorkflow> kv)
            {
                return (Workflow)kv.Value;
            }

            return null;
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

        public void AppendLogLine(string line)
        {
            this.BeginInvoke(() =>
            {
                string br = string.IsNullOrEmpty(this.readOnlyRichTextBox1.Text) ? "" : "\n";
                this.readOnlyRichTextBox1.AppendText(br + line);
            });
        }

        
        public void LoadGraphForProcessInstance(string globalProcessInstanceId)
        {
            int i = globalProcessInstanceId.IndexOf('/');
            string wfId = globalProcessInstanceId.Substring(0, i);
            string processInstanceId = globalProcessInstanceId.Substring(i + 1);

            Workflow workflow = (Workflow)PolokusMaster.GetWorkflow(wfId);
            ProcessInstance processInstance = (ProcessInstance)workflow.GetProcessInstanceById(processInstanceId);

            string rawString = processInstance.BpmnProcess.BpmnWorkflow.RawString;

            this.chromiumWindow.chromeBrowser.ExecuteScriptAsync($"window.api.openInViewerAsync('{rawString}');");


            HashSet<string> activeNodesIds = processInstance.AvailableNodeHandlers.Values.Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = processInstance.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

        public void LoadLogsForProcessInstance(string globalProcessInstanceId)
        {
            this.readOnlyRichTextBox1.Text = "";
            
            if (!_logs.ContainsKey(globalProcessInstanceId))
            {
                return;
            }

            var messages = _logs[globalProcessInstanceId].GetMessages();
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
