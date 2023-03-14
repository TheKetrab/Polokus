namespace Polokus.Core.Extensibility.Externals.Models
{
    public class ExternalHooksProvider
    {
        public string? Name { get; set; }
        public string Assembly { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
}
