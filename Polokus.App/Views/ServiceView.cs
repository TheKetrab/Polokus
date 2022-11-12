using Polokus.App.Utils;
using Polokus.Core;
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
        ContextsManager ContextsManager = new ContextsManager();


        public ServiceView()
        {
            InitializeComponent();

            ChromiumWindow chw = new ChromiumWindow("viewer");
            chw.Parent = panelBpmnio;
            chw.Dock = DockStyle.Fill;

            // TODO: powiazac to z waiterami konkretnego procesu?
            //this.dataGridView1.DataSource = contextsManager


            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\inclusive1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\serviceTask1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\timerEvent1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\exclusive1.bpmn");
            ContextsManager.LoadXmlFile("C:\\Custom\\BPMN\\Polokus\\Polokus.Tests\\NodeHandlersTests\\Bpmn\\msgStart.bpmn");

            this.listViewProcesses.SizeChanged += (s, e) =>
            {
                this.listViewProcesses.Columns[0].Width = this.listViewProcesses.Width - 25;
            };
            this.listViewInstances.SizeChanged += (s, e) =>
            {
                this.listViewInstances.Columns[0].Width = this.listViewInstances.Width - this.listViewInstances.Columns[1].Width - 25;
            };

            InitializeComboBoxContexts();


        }

        private void InitializeComboBoxContexts()
        {
            comboBoxContexts.DataSource = new BindingSource(ContextsManager.ContextInstances, null);
            comboBoxContexts.DisplayMember = "Key";
            comboBoxContexts.ValueMember = "Value";

            comboBoxContexts.SelectedIndexChanged += (s, e) =>
            {
                var context = GetActiveContextInstance();
                context.ProcessInstanceChanged += (s, e) =>
                {
                    this.BeginInvoke(new Action(() =>
                        UpdateProcessInstancesList(context)));
                };
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

        private void UpdateProcessInstancesList(ContextInstance contextInstance)
        {
            listViewInstances.Items.Clear();
            foreach (var instance in contextInstance.ProcessInstances)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.Status.ToStringExt());

                this.listViewInstances.Items.Add(item);
            }

            foreach (var instance in contextInstance.History)
            {
                var item = new ListViewItem(instance.Id);
                item.SubItems.Add(instance.Status.ToStringExt());

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
            return (ContextInstance)((KeyValuePair<string, IContextInstance>)comboBoxContexts.SelectedItem).Value;
        }

        public string? GetSelectedProcessId()
        {
            return GetSelectedSingleItem(listViewProcesses);
        }

        public string? GetSelectedInstanceId()
        {
            return GetSelectedSingleItem(listViewInstances);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // add new process instance

            var contextInstance = GetActiveContextInstance();
            var bpmnProcessId = GetSelectedProcessId();
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
    }
}
