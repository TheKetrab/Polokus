namespace Polokus.Core.Extensibility.Hooks
{
    public enum CallerChangedType
    {
        WaiterInserted,
        WaiterRemoved,

        StarterRegistered,
        StarterDeregistered,
        StarterStartedProcess
    }

    public static class CallerChangedTypeExcensions
    {
        public static CallerChangedType CallerChangedTypeFromString(string str)
        {
            return Enum.Parse<CallerChangedType>(str);
        }
    }
}
