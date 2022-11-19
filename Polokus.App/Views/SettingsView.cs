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
        }

        public void InitBindings()
        {
            textBoxBpmnPath.DataBindings.Add("Text", Properties.Settings.Default, "BpmnPath");
            checkBoxEnableLogs.DataBindings.Add("Checked", Properties.Settings.Default, "EnableLogs");
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
    }
}
