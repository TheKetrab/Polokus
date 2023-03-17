namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// Object sent as 
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Name of emited signal.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Optional parameters of emited signal.
        /// </summary>
        string[]? Params { get; set; }
    }
}
