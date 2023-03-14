namespace Polokus.Core.Execution
{
    public class Signal : ISignal
    {
        public string Name { get; set; }
        public string[]? Params { get; set; }

        public Signal(string name)
        {
            Name = name;
        }

        public Signal(string name, string[]? @params) : this(name)
        {
            Params = @params;
        }
    }
}
