namespace Polokus.Core.Execution
{
    public static class ProcessStatusExtensions
    {
        public static string ToStringExt(this ProcessStatus status)
        {
            return Enum.GetName(status) ?? "";
        }

    }

}
