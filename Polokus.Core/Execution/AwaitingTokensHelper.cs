namespace Polokus.Core.Execution
{
    public static class AwaitingTokensHelper
    {
        private static int _atId = 0;
        private static object _atlock = new();
        public static string CreateAwaitingToken(string wfId, string piId, string nodeId)
        {
            lock (_atlock)
            {
                return $"AT_{wfId}_{piId}_{nodeId}_{_atId++}";
            }
        }

    }
}
