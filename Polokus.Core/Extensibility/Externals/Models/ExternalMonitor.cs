namespace Polokus.Core.Extensibility.Externals.Models
{
    public class ExternalMonitor
    {
        public string? Name { get; set; }
        public string Assembly { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string[] Arguments { get; set; } = new string[] { };
        public string[] Dependencies { get; set; } = new string[] { };
    }

}