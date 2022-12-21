using Polokus.Core;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Service
{
    public class PolokusService
    {
        private IPolokusMaster _polokus;

        public PolokusService()
        {
            _polokus = new PolokusMaster();
        }

        public void Start()
        {

        }
        public void Stop()
        {

        }

    }
}
