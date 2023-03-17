namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Connection Manager is object that controls if any GUI client is connected to master.
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        /// Information if GUI application is connected (opened).
        /// </summary>
        bool ClientConnected { get; }

        /// <summary>
        /// Sets necessary flags so that ClientConnected returns true.
        /// </summary>
        void SetHaveConnection();
    }
}
