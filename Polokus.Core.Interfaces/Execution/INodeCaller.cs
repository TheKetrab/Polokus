namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Object that can 'call' a flow node.
    /// </summary>
    public interface INodeCaller
    {
        /// <summary>
        /// Id of caller.
        /// </summary>
        string Id { get; }
    }
}
