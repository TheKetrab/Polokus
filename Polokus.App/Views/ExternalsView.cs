using Polokus.App.Forms;
using Polokus.Core.Extensibility.Externals;
using Polokus.Core.Extensibility.Externals.Models;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Exceptions;

namespace Polokus.App.Views
{
    public partial class ExternalsView : UserControl
    {
        private MainWindow _mainWindow;

        public ExternalsView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            FillData();
        }

        void FillData()
        {
            textBoxExternalsFile.Text = Settings.ExternalsPath;
            if (Settings.ExternalsPath != null)
            {
                var externals = ExternalsManager.TryLoadExternals(Settings.ExternalsPath);
                if (externals == null)
                {
                    throw new PolokusException("Failed to load externals.");
                }

                if (externals.HooksProviders != null)
                {
                    InitializeListViewHooks(externals.HooksProviders);
                }
                InitializeListViewMonitors(externals.Monitors);
                InitializeListViewServiceTasks(externals.ServiceTasks);
                InitializeListViewSettings(externals.SettingsProvider);
            }
        }


        private void InitializeListViewSettings(ExternalSettingsProvider? settingsProvider)
        {
            if (settingsProvider == null)
            {
                listViewSettingsProvider.Enabled = false;
                return;
            }

            listViewSettingsProvider.View = View.Details;
            listViewSettingsProvider.Columns.Add("Name", 150, HorizontalAlignment.Left);
            listViewSettingsProvider.Columns.Add("Assembly", 250, HorizontalAlignment.Left);
            listViewSettingsProvider.Columns.Add("Class", 300, HorizontalAlignment.Left);
            listViewSettingsProvider.Columns.Add("Assembly path", 100, HorizontalAlignment.Left);


            ListViewItem item = new ListViewItem();
            item.Text = settingsProvider.Name;
            item.SubItems.Add(settingsProvider.Assembly.Substring(settingsProvider.Assembly.LastIndexOfAny(new char[] { '\\', '/' }) + 1));
            item.SubItems.Add(settingsProvider.ClassName);
            item.SubItems.Add(settingsProvider.Assembly);

            listViewSettingsProvider.Items.Add(item);

        }

        private void InitializeListViewMonitors(IEnumerable<ExternalMonitor> monitors)
        {
            if (!monitors.Any())
            {
                listViewMonitors.Enabled = false;
                return;
            }

            listViewMonitors.View = View.Details;
            listViewMonitors.Columns.Add("Name", 150, HorizontalAlignment.Left);
            listViewMonitors.Columns.Add("Assembly", 250, HorizontalAlignment.Left);
            listViewMonitors.Columns.Add("Class", 300, HorizontalAlignment.Left);
            listViewMonitors.Columns.Add("Arguments", 100, HorizontalAlignment.Left);
            listViewMonitors.Columns.Add("Assembly path", 100, HorizontalAlignment.Left);

            foreach (var m in monitors)
            {
                ListViewItem item = new ListViewItem();
                item.Text = m.Name;
                item.SubItems.Add(m.Assembly.Substring(m.Assembly.LastIndexOfAny(new char[] { '\\', '/' }) + 1));
                item.SubItems.Add(m.ClassName);
                item.SubItems.Add(string.Join(", ", m.Arguments));
                item.SubItems.Add(m.Assembly);

                listViewMonitors.Items.Add(item);
            }

        }

        private void InitializeListViewHooks(IEnumerable<ExternalHooksProvider> hooks)
        {
            if (!hooks.Any())
            {
                listViewHooksProviders.Enabled = false;
                return;
            }

            listViewHooksProviders.View = View.Details;
            listViewHooksProviders.Columns.Add("Name", 150, HorizontalAlignment.Left);
            listViewHooksProviders.Columns.Add("Assembly", 250, HorizontalAlignment.Left);
            listViewHooksProviders.Columns.Add("Class", 300, HorizontalAlignment.Left);
            listViewHooksProviders.Columns.Add("Assembly path", 100, HorizontalAlignment.Left);

            foreach (var h in hooks)
            {
                ListViewItem item = new ListViewItem();
                item.Text = h.Name;
                item.SubItems.Add(h.Assembly.Substring(h.Assembly.LastIndexOfAny(new char[] { '\\', '/' }) + 1));
                item.SubItems.Add(h.ClassName);
                item.SubItems.Add(h.Assembly);

                listViewHooksProviders.Items.Add(item);
            }
        }

        private void InitializeListViewServiceTasks(IEnumerable<ExternalServiceTask> serviceTasks)
        {
            if (!serviceTasks.Any())
            {
                listViewServiceTasks.Enabled = false;
                return;
            }

            listViewServiceTasks.View = View.Details;
            listViewServiceTasks.Columns.Add("Name", 150, HorizontalAlignment.Left);
            listViewServiceTasks.Columns.Add("Service task name", 150, HorizontalAlignment.Left);
            listViewServiceTasks.Columns.Add("Assembly", 250, HorizontalAlignment.Left);
            listViewServiceTasks.Columns.Add("Class", 300, HorizontalAlignment.Left);
            listViewServiceTasks.Columns.Add("Assembly path", 100, HorizontalAlignment.Left);

            foreach (var st in serviceTasks)
            {
                ListViewItem item = new ListViewItem();
                item.Text = st.Name;
                item.SubItems.Add(st.ServiceTaskName);
                item.SubItems.Add(st.Assembly.Substring(st.Assembly.LastIndexOfAny(new char[] { '\\', '/' }) + 1));
                item.SubItems.Add(st.ClassName);
                item.SubItems.Add(st.Assembly);

                listViewServiceTasks.Items.Add(item);
            }
        }
    }
}
