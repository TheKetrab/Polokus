using Polokus.App.Forms;
using System.Data;
using System.Xml.Linq;

namespace Polokus.App.Views
{
    public partial class XmlView : UserControl
    {
        private MainWindow _mainWindow;

        public XmlView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            readOnlyRichTextBox1.WordWrap = false;
            readOnlyRichTextBox1.Font = new Font(FontFamily.GenericMonospace, readOnlyRichTextBox1.Font.Size);

            _mainWindow.Menu.TVIndexChanged += (s, e) =>
            {
                if (_mainWindow.ViewModel.ActivePanelView != MainWindowViewModel.PanelView.ProcessesXml)
                {
                    return;
                }

                LoadBpmnXml(e.FilePath);

            };

        }

        public void LoadBpmnXml(string filepath)
        {
            string bpmnContent = File.ReadAllText(filepath);
            this.readOnlyRichTextBox1.Text = FormatXml(bpmnContent);

            string filename = Path.GetFileName(filepath);
            int processesCnt = CountProcesses(bpmnContent);

            this.labelFilenameVal.Text = filename;
            this.labelProcessesCountVal.Text = processesCnt.ToString();
        }

        public int CountProcesses(string bpmn)
        {
            XDocument doc = XDocument.Parse(bpmn);
            int cnt = doc.Descendants().Where(x => x.Name.LocalName == "process").Count();

            return cnt;
        }

        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

    }
}
