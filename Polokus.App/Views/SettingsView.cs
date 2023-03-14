using Polokus.App.Forms;
using Polokus.Core.Interfaces;

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
            // TODO

            //var messageManagers = _mainWindow.MainPanel.ServiceView.PolokusMaster
            //    .GetWorkflows().Select(x => x.MessageManager);

            //if (messageManagers.Any(x => x.IsAnyWaiting()))
            //{
            //    MessageBox.Show("Warning! You changed app port, but there are some waiters. You won't be able to perform ping with Polokus, you will have to ping them manually.");
            //}
        }



        public void InitBindings()
        {
            // Main
            textBoxBpmnPath.DataBindings.Add("Text", Settings.SettingsProvider, "bpmnPath", true, DataSourceUpdateMode.OnPropertyChanged);
            checkBoxRestoreOnStart.DataBindings.Add("Checked", Settings.SettingsProvider, "restoreProcessesOnStart", true, DataSourceUpdateMode.OnPropertyChanged);

            // App
            numericUpDownDelayPerNodeHandler.DataBindings.Add("Value", Settings.SettingsProvider, "delayPerNodeHandlerMs", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarDelayPerNodeHandler.DataBindings.Add("Value", Settings.SettingsProvider, "delayPerNodeHandlerMs", true, DataSourceUpdateMode.OnPropertyChanged);

            textBoxRemotePolokusUri.DataBindings.Add("Text", Settings.SettingsProvider, "remotePolokusUri", true, DataSourceUpdateMode.OnPropertyChanged);
            checkBoxUseRemotePolokus.DataBindings.Add("Checked", Settings.SettingsProvider, "useRemotePolokus", true, DataSourceUpdateMode.OnPropertyChanged);

            // Service
            numericUpDownMessageListenerPort.DataBindings.Add("Value", Settings.SettingsProvider, "messageListenerPort", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxExternalsPath.DataBindings.Add("Text", Settings.SettingsProvider, "externalsPath", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownTimeoutForManualProcesses.DataBindings.Add("Value", Settings.SettingsProvider, "timeoutForProcessSec", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarTimeOutForManualProcesses.DataBindings.Add("Value", Settings.SettingsProvider, "timeoutForProcessSec", true, DataSourceUpdateMode.OnPropertyChanged);

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

        private void buttonResetSettings_Click(object sender, EventArgs e)
        {
            Settings.ResetSettings();
        }

        private void buttonServiceNodeHandlers_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";

                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    this.textBoxExternalsPath.Text = ofd.FileName;
                    this.textBoxExternalsPath.DataBindings["Text"].BindingManagerBase.EndCurrentEdit();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
