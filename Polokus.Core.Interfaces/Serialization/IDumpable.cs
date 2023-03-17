namespace Polokus.Core.Interfaces.Serialization
{
    public interface IDumpable<T>
    {
        /// <summary>
        /// Serializes the object to instance of <typeparamref name="T"/>.
        /// </summary>
        T Dump();
    }
}
