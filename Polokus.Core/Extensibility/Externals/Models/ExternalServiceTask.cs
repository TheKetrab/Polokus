namespace Polokus.Core.Extensibility.Externals.Models
{
    public class ExternalServiceTask
    {
        public string? Name { get; set; }
        public string Assembly { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string ServiceTaskName { get; set; } = string.Empty;
    }
}
