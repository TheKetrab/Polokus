namespace Polokus.Core.Interfaces.BpmnModels
{
    public interface ISignal
    {
        string Name { get; set; }
        string[]? Params { get; set; }
    }
}
