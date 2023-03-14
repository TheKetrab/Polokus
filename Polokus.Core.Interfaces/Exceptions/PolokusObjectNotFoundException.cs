namespace Polokus.Core.Interfaces.Exceptions
{
    public class PolokusObjectNotFoundException : PolokusException
    {
        public PolokusObjectNotFoundException() { }
        public PolokusObjectNotFoundException(ObjectType type, string name)
            : base($"Object {type} with name {name} not found.")
        {
        }

    }

    public class WorkflowNotFoundException : PolokusObjectNotFoundException
    {
        public WorkflowNotFoundException(string name) : base(ObjectType.Workflow, name) { }

    }

    public class ProcessInstanceNotFoundException : PolokusObjectNotFoundException
    {
        public ProcessInstanceNotFoundException(string name) : base(ObjectType.ProcessInstance, name) { }
    }

    public class FlowNodeNotFoundException : PolokusObjectNotFoundException
    {
        public FlowNodeNotFoundException(string name) : base(ObjectType.FlowNode, name) { }
    }

    public class SequenceNotFoundException : PolokusObjectNotFoundException
    {
        public SequenceNotFoundException(string name) : base(ObjectType.Sequence, name) { }
    }

}
