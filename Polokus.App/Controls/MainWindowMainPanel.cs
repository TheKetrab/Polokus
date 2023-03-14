using Polokus.App.Forms;
using Polokus.App.Views;

namespace Polokus.App.Controls
{
    public class MainWindowMainPanel
    {
        public ServiceView ServiceView { get; }
        public ChromiumWindow EditorView { get; }
        public GraphView GraphView { get; }
        public XmlView XmlView { get; }
        public SettingsView SettingsView { get; }
        public ExternalsView ExternalsView { get; }

        private MainWindow _mainWindow;

        public void SuspendLayout()
        {
            ServiceView.SuspendLayout();
            EditorView.SuspendLayout();
            GraphView.SuspendLayout();
            XmlView.SuspendLayout();
            SettingsView.SuspendLayout();
            ExternalsView.SuspendLayout();
        }

        public void ResumeLayout()
        {
            ServiceView.ResumeLayout(true);
            EditorView.ResumeLayout(true);
            GraphView.ResumeLayout(true);
            XmlView.ResumeLayout(true);
            SettingsView.ResumeLayout(true);
            ExternalsView.ResumeLayout(true);
        }

        public MainWindowMainPanel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            ServiceView = new ServiceView(_mainWindow);
            EditorView = new ChromiumWindow(_mainWindow);
            GraphView = new GraphView(_mainWindow);
            XmlView = new XmlView(_mainWindow);
            SettingsView = new SettingsView(_mainWindow);
            ExternalsView = new ExternalsView(_mainWindow);

            InitializeSubViews();
        }

        private void InitializeSubViews()
        {
            EditorView.Dock = DockStyle.Fill;
            EditorView.Parent = _mainWindow.PanelEditor;

            GraphView.Dock = DockStyle.Fill;
            GraphView.BackColor = PolokusStyle.DefaultViewColor;
            GraphView.Parent = _mainWindow.PanelProcessesGraph;

            XmlView.Dock = DockStyle.Fill;
            XmlView.BackColor = PolokusStyle.DefaultViewColor;
            XmlView.Parent = _mainWindow.PanelProcessesXml;

            ServiceView.Dock = DockStyle.Fill;
            ServiceView.BackColor = PolokusStyle.DefaultViewColor;
            ServiceView.Parent = _mainWindow.PanelService;

            SettingsView.Dock = DockStyle.Fill;
            SettingsView.BackColor = PolokusStyle.DefaultViewColor;
            SettingsView.Parent = _mainWindow.PanelSettings;

            ExternalsView.Dock = DockStyle.Fill;
            ExternalsView.BackColor = PolokusStyle.DefaultViewColor;
            ExternalsView.Parent = _mainWindow.PanelProcessesCSharp;
        }

    }
}
