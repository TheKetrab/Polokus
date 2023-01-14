using CefSharp;
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
        private MainWindow _mainWindow;
        private ChromiumWindow _chromiumWindow;

        public GraphView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            _chromiumWindow = new ChromiumWindow(mainWindow, "viewer");
            _chromiumWindow.Dock = DockStyle.Fill;
            _chromiumWindow.Parent = this.panel2;

            _mainWindow.Menu.TVIndexChanged += (s, e) =>
            {
                if (_mainWindow.ViewModel.ActivePanelView != MainWindowViewModel.PanelView.ProcessesGraph)
                {
                    return;
                }

                LoadBpmnXml(e.FilePath);

            };
        }

        private void LoadBpmnXml(string path)
        {
            if (!_chromiumWindow.chromeBrowser.IsBrowserInitialized)
            {
                return;
            }

            string rawString = File.ReadAllText(path).Replace("\r","").Replace("\n","");

            Task.Run(async () =>
            {
                var res = await _chromiumWindow.chromeBrowser.EvaluateScriptAsync(
                    $"window.api.openInViewerAsync('{rawString}');");
            });
        }
    }
}
