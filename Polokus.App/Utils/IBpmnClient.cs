namespace Polokus.App.Utils
{
    public interface IBpmnClient
    {
        Task<string> GetBpmnSvg(string bpmnXml);
    }
}
