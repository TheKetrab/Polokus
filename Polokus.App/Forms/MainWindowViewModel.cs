using Polokus.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Forms
{
    public class MainWindowViewModel
    {
        public MainWindow View;

        public MainWindowViewModel(MainWindow view)
        {
            View = view;

            _map.Add(PanelView.None, view.PanelHome);
            _map.Add(PanelView.ProcessesGraph, view.PanelProcessesGraph);
            _map.Add(PanelView.ProcessesXml, view.PanelProcessesXml);
            _map.Add(PanelView.ProcessesScript, view.PanelProcessesCSharp);
            _map.Add(PanelView.Editor, view.PanelEditor);
            _map.Add(PanelView.Settings, view.PanelSettings);
            _map.Add(PanelView.Service, view.PanelService);
            _map.Add(PanelView.Home, view.PanelHome);
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
                var view = _map[value];
                if (view != null)
                {
                    view.Visible = true;
                }
            }
        }

        public readonly string MainDirPath = @"C:\Custom\BPMN\Polokus\Polokus.Tests\NodeHandlersTests\Bpmn";


    }
}
