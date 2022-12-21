using CefSharp.DevTools.Page;
using Polokus.App.Forms;
using Polokus.App.Utils;
using Polokus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polokus.App.Views
{
    public partial class SettingsView : UserControl
    {
        private MainWindow _mainWindow;

        public SettingsView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            InitBindings();

            this.numericUpDownMessageListenerPort.ValueChanged += NumericUpDownMessageListenerPort_ValueChanged;

            textBoxBpmnPath.TextChanged += TextBoxBpmnPath_TextChanged;

            
        }

        private void TextBoxBpmnPath_TextChanged(object? sender, EventArgs e)
        {
            // TODO: reload Workflow instances
        }

        private void NumericUpDownMessageListenerPort_ValueChanged(object? sender, EventArgs e)
        {
            var messageManagers = _mainWindow.MainPanel.ServiceView.PolokusMaster
                .GetWorkflows().Select(x => x.MessageManager);

            if (messageManagers.Any(x => x.IsAnyWaiting()))
            {
                MessageBox.Show("Warning! You changed app port, but there are some waiters. You won't be able to perform ping with Polokus, you will have to ping them manually.");
            }
                
                    
        }



        public void InitBindings()
        {
            textBoxBpmnPath.DataBindings.Add("Text", Properties.Settings.Default, "BpmnPath");
            checkBoxEnableLogs.DataBindings.Add("Checked", Properties.Settings.Default, "EnableLogs");
            numericUpDownMessageListenerPort.DataBindings.Add("Value", Properties.Settings.Default, "MessageListenerPort");
            textBoxBpmnServiceNodeHandlers.DataBindings.Add("Text", Properties.Settings.Default, "ServiceTasksExternals");

            numericUpDownDelayPerNodeHandler.DataBindings.Add("Value", Properties.Settings.Default, "DelayPerNodeHandlerMs", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarDelayPerNodeHandler.DataBindings.Add("Value", Properties.Settings.Default, "DelayPerNodeHandlerMs", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownTimeoutForManualProcesses.DataBindings.Add("Value", Properties.Settings.Default, "TimeoutForProcessSec", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarTimeOutForManualProcesses.DataBindings.Add("Value", Properties.Settings.Default, "TimeoutForProcessSec", true, DataSourceUpdateMode.OnPropertyChanged);
               

        }

        private void buttonBrowseBpmnPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.textBoxBpmnPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void buttonServiceNodeHandlers_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";

                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    this.textBoxBpmnServiceNodeHandlers.Text = ofd.FileName;
                    this.textBoxBpmnServiceNodeHandlers.DataBindings["Text"].BindingManagerBase.EndCurrentEdit();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
