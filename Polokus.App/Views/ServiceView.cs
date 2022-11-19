using CefSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Polokus.App.Utils;
using Polokus.Core;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Polokus.App.Views
{
    public partial class ServiceView : UserControl
    {
        ContextsManager ContextsManager;
        public ChromiumWindow chromiumWindow;

        public ServiceView()
        {
            InitializeComponent();

            chromiumWindow = new ChromiumWindow("viewer");
            chromiumWindow.Parent = panelBpmnio;
            chromiumWindow.Dock = DockStyle.Fill;

            // TODO: powiazac to z waiterami konkretnego procesu?
            //this.dataGridView1.DataSource = contextsManager


            ContextsManager = new ContextsManager();

            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\inclusive1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\serviceTask1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\timerEvent1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\exclusive1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\msgStart.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Examples\\biggerprocess1.bpmn");

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

            UpdateNodeHandlerWaitersList((ContextInstance)ContextsManager.ContextInstances.First().Value);
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

        private void InitializeComboBoxContexts()
        {
            comboBoxContexts.DataSource = new BindingSource(ContextsManager.ContextInstances, null);
            comboBoxContexts.DisplayMember = "Key";
            comboBoxContexts.ValueMember = "Value";

            comboBoxContexts.SelectedIndexChanged += (s, e) =>
            {
                var context = GetActiveContextInstance();
                if (comboBoxContexts.SelectedItem is KeyValuePair<string,IContextInstance> ci)
                {
                    ActiveContextInstance = ci.Key;
                }

                LoadViewForContext(context);
            };
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

        private void UpdateProcessStartersList(ContextInstance contextInstance)
        {
            listViewStarters.Items.Clear();

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

        private void UpdateNodeHandlerWaitersList(ContextInstance contextInstance)
        {
            listViewWaiters.Items.Clear();

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

        public void UpdateProcessInstancesList(ContextInstance contextInstance)
        {
            listViewInstances.Items.Clear();
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
                this.readOnlyRichTextBox1.AppendFormattedText(line, Color.Red, true);
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
                switch (message.Item1) 
                {
                    case Logger.MsgType.Simple:
                        readOnlyRichTextBox1.AppendText('\n' + message.Item2);
                        break;
                    case Logger.MsgType.Warning:
                        readOnlyRichTextBox1.AppendFormattedText('\n' + message.Item2, Color.Orange, false);
                        break;
                    case Logger.MsgType.Error:
                        readOnlyRichTextBox1.AppendFormattedText('\n' + message.Item2, Color.Red, true);
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
                throw new Exception("failiure");
            }

            var appHooksProvider = new AppHooksProvider(contextInstance);
            contextInstance.SetHooksProvider(appHooksProvider);

            contextInstance.StartProcessManually(bpmnProcessId);
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
                    ContextsManager.LoadXmlFile(filePath);
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
