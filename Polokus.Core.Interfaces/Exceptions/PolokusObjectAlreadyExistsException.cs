namespace Polokus.Core.Interfaces.Exceptions
{
    public class PolokusObjectAlreadyExistsException : PolokusException
    {
        public PolokusObjectAlreadyExistsException() { }
        public PolokusObjectAlreadyExistsException(ObjectType type, string name)
            : base($"Object {type} with name {name} already exists.")
        {
        }

    }

    public class WorkflowAlreadyExistsException : PolokusObjectAlreadyExistsException
    {
        public WorkflowAlreadyExistsException(string name) : base(ObjectType.Workflow, name) { }

    }
}
