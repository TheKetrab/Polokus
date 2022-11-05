using Polokus.App.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Polokus.App.Views
{
    public partial class XmlView : UserControl
    {
        public XmlView()
        {
            InitializeComponent();
            readOnlyRichTextBox1.WordWrap = false;
            readOnlyRichTextBox1.Font = new Font(FontFamily.GenericMonospace, readOnlyRichTextBox1.Font.Size);


            MainWindow.Instance.TVIndexChanged += (s, e) =>
            {
                if (MainWindow.Instance.ActivePanelView != MainWindow.PanelView.ProcessesXml)
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

            this.labelFilename.Text = filename;
            this.labelProcessesCount.Text = $"Processes: {processesCnt}";
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

        private void readOnlyRichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
