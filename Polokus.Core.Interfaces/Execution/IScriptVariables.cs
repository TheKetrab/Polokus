namespace Polokus.Core.Interfaces.Execution
{
    public interface IScriptVariables
    {
        List<object> Values { get; }
        List<string> Variables { get; }
        T? TryGetValue<T>(string variable);
        void SetValue(string variable, object value);
        object GetValue(string variable);
    }
}
