﻿using Polokus.Core.Interfaces.Communication;

namespace Polokus.Core.Communication.Services.OnPremise
{
    public class OnPremisePolokusService : IPolokusService
    {
        private PolokusMaster _polokus;

        public OnPremisePolokusService(PolokusMaster polokus)
        {
            _polokus = polokus;
        }

        public void DeregisterHooksProvider(IHooksProvider hooksProvider)
        {
            _polokus.HooksManager.DeregisterHooksProvider(hooksProvider);
        }

        public IEnumerable<string> GetWorkflowsIds()
        {
            return _polokus.GetWorkflows().Select(x => x.Id);
        }

        public void LoadXmlString(string str, string wfName)
        {
            _polokus.LoadXmlString(str, wfName);
        }

        public void RegisterHooksProvider(IHooksProvider hooksProvider)
        {
            _polokus.HooksManager.RegisterHooksProvider(hooksProvider);
        }

        public void SetClientConnected()
        {
            _polokus.ConnectionManager.SetHaveConnection();
        }
    }
}
