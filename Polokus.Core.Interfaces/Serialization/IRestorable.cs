namespace Polokus.Core.Interfaces.Serialization
{
    public interface IRestorable<T>
    {
        /// <summary>
        /// Restores object from information stored in <paramref name="source"/>.
        /// </summary>
        /// <param name="master">Master of engine.</param>
        /// <param name="source">Object with information.</param>
        void Restore(IPolokusMaster master, T source);
    }
}
