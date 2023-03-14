namespace Polokus.App.Utils
{
    internal static class Helpers
    {
        public static string GetGlobalProcessInstanceId(string wfId, string processInstanceId)
        {
            return $"{wfId}/{processInstanceId}";
        }

        public static void GetWfPiIDs(string globalPiId, out string wfId, out string piId)
        {
            int i = globalPiId.IndexOf('/');
            wfId = globalPiId.Substring(0, i);
            piId = globalPiId.Substring(i + 1);
        }
    }
}
