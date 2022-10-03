using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public abstract class NodeHandler<T> : INodeHandler
        where T : tFlowNode
    {
        // odpowiedzialnosci:
        // 1. nodehandler -> przetwarza KONKRETNY FLowNode
        //     moze byc tak, ze sie zakonczy, wtedy mowimy ktore sekwencje odpalic
        //     moze byc tak ze bedzie blad, wtedy error
        //     moze byc tak ze stan trzeba zapisac stan, po to mamy slownik z nodehandlerami w processinstance
        //     moze byc tak ze trzeba czekać iles sekund zanim ponownie odpali sie ten sam nodehandler, zeby cos sprawdzic
        // 2. process instance: zarzadza tym, ktore aktualnie flownode sa przetwarzane
        //       czyli tylko on powinien sie martwic o numery taskow...? TaskPool?
        // 3. eventy -> cos z zewnatrz sie moze stac i to cos rzuca event i process instance REAGUJE
        //
        //
        // 


        // nodehandler przetwarza to co ma zrobic i wybiera kolejne node'y do potencjalnego wywolania

        public IFlowNode Node => TypedNode;
        public FlowNode<T> TypedNode { get; set; }

        public NodeHandler(FlowNode<T> typedNode)
        {
            TypedNode = typedNode;
        }


        public virtual async Task<ProcessResultInfo> Execute(IFlowNode? caller)
        {
            try
            {
                await Task.Delay(300);
                var nextSequences = Node.Outgoing.ToList();
                Console.WriteLine($"Processing: {Node.Id,30} ({typeof(T).Name}) ({Node.Name}) ... DONE");
                return new ProcessResultInfo(ProcessResultState.Success, nextSequences);
            }
            catch (Exception)
            {
                return new ProcessResultInfo(ProcessResultState.Failure);
            }



        }

    }
}
