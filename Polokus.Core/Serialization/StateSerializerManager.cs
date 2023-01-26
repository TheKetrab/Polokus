using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Polokus.Core.Serialization
{
    public class StateSerializerManager
    {
        private PolokusMaster _master;

        private static string? _piStateDirectory = null;
        private static string PiStateDirectory => _piStateDirectory ??= Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Polokus", "ProcessInstances");

        private Dictionary<string, object> _locks = new();
        private object GetLockForFile(string filename)
        {
            if (_locks.ContainsKey(filename))
            {
                return _locks[filename];
            }
            else
            {
                _locks.Add(filename, new object());
                return _locks[filename];
            }
        }

        private void RemoveLock(string filename)
        {
            if (_locks.ContainsKey(filename))
            {
                _locks.Remove(filename);
            }
        }


        public StateSerializerManager(PolokusMaster master)
        {
            _master = master;

            if (!Directory.Exists(PiStateDirectory))
            {
                Directory.CreateDirectory(PiStateDirectory);
            }
        }

        private string GetFilePath(string wfId, string piId)
        {
            string stateString = EncodingIds.GetStateString(wfId, piId);

            string filename = ENC($"{stateString}.json");

            return Path.Combine(PiStateDirectory, filename);
        }

        private void GetWfPiFromFilePath(string filepath, out string wfId, out string piId)
        {
            string filename = Path.GetFileNameWithoutExtension(filepath);
            string stateString = DEC(filename);

            wfId = EncodingIds.GetWorkflowIdFromStateString(stateString);
            piId = EncodingIds.GetProcessInstanceIdFromStateString(stateString);
        }



        private static string DEC(string str)
        {
            return str
                .Replace("__", @"(")
                .Replace("___", @")")
                .Replace("____", @"/")
                .Replace("_____", @"\");
        }

        private static string ENC(string str)
        {
            return str
                .Replace(@"(", "__")
                .Replace(@")", "___")
                .Replace(@"/", "____")
                .Replace(@"\", "_____");
        }

        public void SerializeState(string wfId, string piId)
        {
            var pi = GetProcessInstance(wfId, piId);

            string file = GetFilePath(wfId, piId);
            var snapshot = pi.Dump();
            string json = JsonSerializer.Serialize(snapshot);

            object l = GetLockForFile(file);
            lock (l)
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.Write(json);
                }
            }
        }

        public ProcessInstance ReconstructProcessInstance(string wfId, string piId)
        {
            // TODO!!
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<string, string>> GetInfoForAllSnapshots()
        {
            List<Tuple<string, string>> result = new();

            string[] files = Directory.GetFiles(PiStateDirectory);
            foreach (var file in files)
            {
                GetWfPiFromFilePath(file, out string wfId, out string piId);
                result.Add(Tuple.Create(wfId, piId));
            }

            return result;
        }

        public void ClearStateFor(string wfId, string piId)
        {
            string file = GetFilePath(wfId, piId);
            File.Delete(file);
            RemoveLock(file);
        }

        private ProcessInstance GetProcessInstance(string wfId, string piId)
        {
            return (ProcessInstance)_master.GetWorkflow(wfId).GetProcessInstanceById(piId);
        }

    }
}
