using Microsoft.CodeAnalysis.CSharp.Syntax;
using Polokus.App.Controls;
using Polokus.App.Forms;
using Polokus.App.Utils;
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
        private MainWindow _mainWindow;

        Bitmap currentBmp;

        string current = "";

        public GraphView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            this.panelGraphic.Paint += Panel1_Paint;

            _mainWindow.Menu.TVIndexChanged += async (s, e) =>
            {
                if (_mainWindow.ViewModel.ActivePanelView != MainWindowViewModel.PanelView.ProcessesGraph)
                {
                    return;
                }


                string file = e.FilePath;

                if (file == current)
                {
                    return;
                }

                current = file;

                Bitmap bitmap;
                if (_cache.ContainsKey(file))
                {
                    bitmap = _cache[file];
                }
                else
                {
                    string bpmnContent = File.ReadAllText(file);
                    throw new NotImplementedException();
                    string svg = "";// await _mainWindow.BpmnClient.GetBpmnSvg(bpmnContent);
                    bitmap = Utils.ImageConverter.GetBitmapFromSvg(svg);
                    _cache[file] = bitmap;
                }

                currentBmp = bitmap;
                this.panelGraphic.Width = bitmap.Width;
                this.panelGraphic.Height = bitmap.Height;
                this.panelGraphic.Invalidate();
            };
        }

        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            if (currentBmp != null)
            {
                _mainWindow.SetInfo($"{this.DimString()}{30,' '}{this.panelMain.DimString()}{30,' '}{this.panelGraphic.DimString()}");
                e.Graphics.DrawImage(currentBmp, 0, 0, currentBmp.Width, currentBmp.Height);
            }

        }
    }
}
