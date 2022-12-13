namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// ContextsManager is an object that manages all loaded XML definitions.
    /// </summary>
    public interface IContextsManager
    {
        /// <summary>
        /// Collection of loaded context instances accessible by their ids.
        /// </summary>
        IDictionary<string,IContextInstance> ContextInstances { get; }
    }
}
