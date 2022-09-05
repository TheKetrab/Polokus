using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.Serialization;
using Polokus.Lib.BpmnObjects;

namespace Polokus.Lib
{


    public class ProcessInstance
    {
        public Guid Guid { get; set; }
        public BpmnProcess BpmnProcess { get; set; }
        public bool IsRunning { get; set; }


        public void Stop()
        {

        }

        public void Start()
        {

        }


    }

}
