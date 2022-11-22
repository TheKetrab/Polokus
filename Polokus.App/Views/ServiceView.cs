using CefSharp;
using Polokus.App.Utils;
using Polokus.Core;
using Polokus.Core.Interfaces;
using System.Data;
using System.Reflection;

namespace Polokus.App.Views
{
    public partial class ServiceView : UserControl
    {
        public ContextsManager ContextsManager;
        public ChromiumWindow chromiumWindow;

        public ServiceView()
        {
            InitializeComponent();

            chromiumWindow = new ChromiumWindow("viewer");
            chromiumWindow.Parent = panelBpmnio;
            chromiumWindow.Dock = DockStyle.Fill;

            ContextsManager = new ContextsManager();
            LoadBpmnFiles();

            foreach (var contextInstance in ContextsManager.ContextInstances.Values)
            {
                ContextInstance ci = (ContextInstance)contextInstance;
                ((TimeManager)(contextInstance.TimeManager)).CallersChanged += (s, e) =>
                {
                    if (listViewWaiters.IsHandleCreated && listViewStarters.IsHandleCreated)
                    {
                        listViewWaiters.BeginInvoke(() => UpdateNodeHandlerWaitersList(ci));
                        listViewStarters.BeginInvoke(() => UpdateProcessStartersList(ci));
                    }
                };
                ((MessageManager)(contextInstance.MessageManager)).CallersChanged += (s, e) =>
                {
                    if (listViewWaiters.IsHandleCreated && listViewStarters.IsHandleCreated)
                    {
                        listViewWaiters.BeginInvoke(() => UpdateNodeHandlerWaitersList(ci));
                        listViewStarters.BeginInvoke(() => UpdateProcessStartersList(ci));
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


            InitializeComboBoxContexts();

            UpdateNodeHandlerWaitersList((ContextInstance?)ContextsManager.ContextInstances.Values.FirstOrDefault());
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
            ContextsManager.LoadXmlFile(file, settingsProvider: new AppSettingsProvider());
            if (!_comboBoxContextsInitialized)
            {
                InitializeComboBoxContexts();
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

        private bool _comboBoxContextsInitialized = false;
        private void InitializeComboBoxContexts()
        {
            if (!ContextsManager.ContextInstances.Values.Any())
            {
                return;
            }

            comboBoxContexts.DataSource = new BindingSource(ContextsManager.ContextInstances, null);
            comboBoxContexts.DisplayMember = "Key";
            comboBoxContexts.ValueMember = "Value";

            comboBoxContexts.SelectedIndexChanged += comboBoxContexts_SelectedIndexChanged;

            comboBoxContexts_SelectedIndexChanged(null, EventArgs.Empty);
            _comboBoxContextsInitialized = true;
        }

        private void comboBoxContexts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var context = GetActiveContextInstance();
            if (comboBoxContexts.SelectedItem is KeyValuePair<string, IContextInstance> ci)
            {
                ActiveContextInstance = ci.Key;
            }

            LoadViewForContext(context);
        }

        private void LoadViewForContext(ContextInstance contextInstance)
        {
            listViewProcesses.Items.Clear();
            foreach (var process in contextInstance.BpmnContext.BpmnProcesses)
            {
                this.listViewProcesses.Items.Add(process.Id);
            }

            UpdateProcessInstancesList(contextInstance);

        }

        private void UpdateProcessStartersList(ContextInstance? contextInstance)
        {
            listViewStarters.Items.Clear();
            if (contextInstance == null)
            {
                return;
            }

            List<Tuple<string,IProcessStarter>> starters = new();
            starters.AddRange(contextInstance.TimeManager.GetStarters().Select(x => Tuple.Create("Time",x)));
            starters.AddRange(contextInstance.MessageManager.GetStarters().Select(x => Tuple.Create("Message", x)));

            foreach (var starter in starters)
            {
                var item = new ListViewItem(starter.Item2.Id);
                item.SubItems.Add(starter.Item2.StartNode.Id);
                item.SubItems.Add(starter.Item1);

                this.listViewStarters.Items.Add(item);
            }
        }

        private void UpdateNodeHandlerWaitersList(ContextInstance? contextInstance)
        {
            listViewWaiters.Items.Clear();
            if (contextInstance == null)
            {
                return;
            }

            List<Tuple<string,INodeHandlerWaiter>> waiters = new();
            waiters.AddRange(contextInstance.TimeManager.GetWaiters().Select(x => Tuple.Create("Time",x)));
            waiters.AddRange(contextInstance.MessageManager.GetWaiters().Select(x => Tuple.Create("Message", x)));

            foreach (var waiter in waiters)
            {
                var item = new ListViewItem(waiter.Item2.Id);
                item.SubItems.Add(waiter.Item2.NodeToCall.Id);
                item.SubItems.Add(waiter.Item1);

                this.listViewWaiters.Items.Add(item);
            }
        }

        public void UpdateProcessInstancesList(ContextInstance? contextInstance)
        {
            listViewInstances.Items.Clear();
            if (contextInstance == null)
            {
                return;
            }

            foreach (var instance in contextInstance.ProcessInstances)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.Status.ToStringExt());
                item.SubItems.Add(instance.ActiveTasksManager.ActiveTasks.Count.ToString());

                this.listViewInstances.Items.Add(item);
            }

            foreach (var instance in contextInstance.History)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.Status.ToStringExt());
                item.SubItems.Add(instance.ActiveTasksManager.ActiveTasks.Count.ToString());

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

        public ContextInstance? GetActiveContextInstance()
        {
            object si;
            if (comboBoxContexts.InvokeRequired)
            {
                si = comboBoxContexts.Invoke(() => comboBoxContexts.SelectedItem);
            }
            else
            {
                si = comboBoxContexts.SelectedItem;
            }

            if (si == null)
            {
                return null;
            }

            if (si is KeyValuePair<string, IContextInstance> kv)
            {
                return (ContextInstance)kv.Value;
            }

            return null;
        }

        private string? ActiveContextInstance { get; set; } = null;
        private string? ActiveProcessInstance { get; set; } = null;
        public string? GetOpenedProcessInstanceGlobalId()
        {
            if (ActiveContextInstance == null || ActiveProcessInstance == null)
            {
                return null;
            }

            return Helpers.GetGlobalProcessInstanceId(ActiveContextInstance, ActiveProcessInstance);
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
            string contextInstanceId = globalProcessInstanceId.Substring(0, i);
            string processInstanceId = globalProcessInstanceId.Substring(i + 1);

            ContextInstance contextInstance = (ContextInstance)ContextsManager.ContextInstances[contextInstanceId];
            ProcessInstance processInstance = (ProcessInstance)contextInstance.GetProcessInstanceById(processInstanceId);

            string rawString = processInstance.BpmnProcess.BpmnContext.RawString;

            this.chromiumWindow.chromeBrowser.ExecuteScriptAsync($"window.api.openInViewerAsync('{rawString}');");


            HashSet<string> activeNodesIds = processInstance.AvailableNodeHandlers.Values.Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = processInstance.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

        public void LoadLogsForProcessInstance(string globalProcessInstanceId)
        {
            this.readOnlyRichTextBox1.Text = "";
            
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

            var contextInstance = GetActiveContextInstance();
            var bpmnProcessId = GetSelectedSingleItem(listViewProcesses);

            if (contextInstance == null || bpmnProcessId == null)
            {
                throw new Exception("None BPMN process is selected.");
            }

            var appHooksProvider = new AppHooksProvider(contextInstance);
            contextInstance.SetHooksProvider(appHooksProvider);

            string processInstanceId = contextInstance.StartProcessManually(bpmnProcessId);
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

        private void buttonLoadContext_Click(object sender, EventArgs e)
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
            var ci = GetActiveContextInstance();
            if (ci == null)
            {
                return;
            }

            string listenerId = this.textBoxPingWaiter.Text;
            ci.MessageManager.PingListener(listenerId);
        }




    }
}
