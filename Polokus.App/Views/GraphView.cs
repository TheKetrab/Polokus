using Polokus.App.Controls;
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

namespace Polokus.App.Views
{
    public partial class GraphView : UserControl
    {
        public Dictionary<string, Bitmap> _cache = new();

        Graphics _graphics;


        public GraphView()
        {
            InitializeComponent();

            if (MainWindow.Instance != null)
            {
                MainWindow.Instance.TVIndexChanged += async (s, e) =>
                {
                    if (MainWindow.Instance?.ActivePanelView != MainWindow.PanelView.ProcessesGraph)
                    {
                        return;
                    }


                    _graphics = this.panel1.CreateGraphics();
                    _graphics.Clear(BackColor);

                    string file = e.FilePath;
                    Bitmap bitmap;
                    if (_cache.ContainsKey(file))
                    {
                        bitmap = _cache[file];
                    }
                    else
                    {
                        string bpmnContent = File.ReadAllText(file);
                        string svg = await MainWindow.Instance.BpmnioClient.GetBpmnSvg(bpmnContent);
                        //string svg = File.ReadAllText("C:\\Users\\Bartlomiej.Grochowsk\\Downloads\\diagram (5).svg");
                        //Utils.ImageConverter.SavePngFromSvg(svg);
                        bitmap = Utils.ImageConverter.GetBitmapFromSvg(svg);
                        _cache[file] = bitmap;
                    }

                    this.panel1.Width = bitmap.Width;
                    this.panel1.Height = bitmap.Height;

                    _graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                };
            }
        }

       
    }
}
