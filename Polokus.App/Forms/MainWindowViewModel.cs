﻿using Polokus.Core.Helpers;

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

        private bool _serviceViewVisitedOneTime = false;
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

                if (value == PanelView.Service && !_serviceViewVisitedOneTime)
                {
                    _serviceViewVisitedOneTime = true;
                    View.MainPanel.ServiceView.LoadViewForFirstWorkflowSafe();
                }

                ToggleConnectionToLabel(value);
            }
        }

        private void ToggleConnectionToLabel(PanelView view)
        {
            if (view == PanelView.Service)
            {
                string masterUri = PolokusApp.GrpcChannel?.Target ?? "App";
                this.View.SetConnectionToLabelText($"Polokus Master: {masterUri}");
            }
            else
            {
                this.View.SetConnectionToLabelText("");
            }
        }

    }
}
