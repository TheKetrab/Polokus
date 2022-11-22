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
        public SettingsView()
        {
            InitializeComponent();
            InitBindings();

            this.numericUpDownMessageListenerPort.ValueChanged += NumericUpDownMessageListenerPort_ValueChanged;
        }

        private void NumericUpDownMessageListenerPort_ValueChanged(object? sender, EventArgs e)
        {
            var messageManagers = MainWindow.Instance.ServiceView.ContextsManager
                .ContextInstances.Select(x => x.Value.MessageManager);

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
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.textBoxBpmnServiceNodeHandlers.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
