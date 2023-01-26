namespace Polokus.Core.Interfaces.Execution
{
    public interface IScriptVariables
    {
        List<object> Values { get; }
        List<string> Variables { get; }
        T? TryGetValue<T>(string variable);
    }
}
