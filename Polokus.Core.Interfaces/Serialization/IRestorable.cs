namespace Polokus.Core.Interfaces.Serialization
{
    public interface IRestorable<T>
    {
        void Restore(IPolokusMaster master, T source);
    }
}
