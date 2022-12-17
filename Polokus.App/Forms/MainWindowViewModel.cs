using Polokus.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polokus.App.Forms
{
    public class MainWindowViewModel
    {
        public MainWindow View;

        public MainWindowViewModel(MainWindow view)
        {
            View = view;

        }

        public void InitializeMap()
        {
            _map.Add(PanelView.None, View.PanelHome);
            _map.Add(PanelView.ProcessesGraph, View.PanelProcessesGraph);
            _map.Add(PanelView.ProcessesXml, View.PanelProcessesXml);
            _map.Add(PanelView.ProcessesScript, View.PanelProcessesCSharp);
            _map.Add(PanelView.Editor, View.PanelEditor);
            _map.Add(PanelView.Settings, View.PanelSettings);
            _map.Add(PanelView.Service, View.PanelService);
            _map.Add(PanelView.Home, View.PanelHome);

        }

        public Size FormSize;
        public int BorderSize = 2;

        public enum PanelView
        {
            None,
            ProcessesGraph,
            ProcessesXml,
            ProcessesScript,
            Editor,
            Settings,
            Service,
            Home
        }

        private PanelView _activePanelView;
        private Dictionary<PanelView, Panel> _map = new();
        public PanelView ActivePanelView
        {
            get
            {
                return _activePanelView;
            }
            set
            {
                _activePanelView = value;
                _map.Where(x => x.Key != value && x.Value != null).ForEach(x => x.Value.Visible = false);
                if (!_map.ContainsKey(value))
                    return;
                var view = _map[value];
                if (view != null)
                {
                    view.Visible = true;
                }
            }
        }

        public readonly string MainDirPath = @"C:\Custom\BPMN\Polokus\Polokus.Tests\Resources\Bpmn";


    }
}
