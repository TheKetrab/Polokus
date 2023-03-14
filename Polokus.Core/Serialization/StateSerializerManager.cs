using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using System.Text.Json;

namespace Polokus.Core.Serialization
{
    public class StateSerializerManager
    {
        private PolokusMaster _master;

        private static string? _piStateDirectory = null;
        private static string PiStateDirectory => _piStateDirectory ??= Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Polokus", "ProcessInstances");

        private Dictionary<string, object> _locks = new();

        private int _stepsToClearLocks = 0;
        private object GetLockForFile(string filename)
        {
            if (_locks.ContainsKey(filename))
            {
                return _locks[filename];
            }
            else
            {
                _stepsToClearLocks++;
                if (_stepsToClearLocks > 50)
                {
                    RemoveUnusedLocks();
                    _stepsToClearLocks = 0;
                }

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

        private void RemoveUnusedLocks()
        {
            var keys = _locks.Keys;
            foreach (var path in keys)
            {
                if (!File.Exists(path))
                {
                    RemoveLock(path);
                }
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
                .Replace("._____.", @"\")
                .Replace(".____.", @"/")
                .Replace(".___.", @")")
                .Replace(".__.", @"(");
        }

        private static string ENC(string str)
        {
            return str
                .Replace(@"(", ".__.")
                .Replace(@")", ".___.")
                .Replace(@"/", ".____.")
                .Replace(@"\", "._____.");
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

        public ProcessInstanceSnapShot DeserializeState(string filePath)
        {
            object l = GetLockForFile(filePath);
            lock (l)
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<ProcessInstanceSnapShot>(json)
                    ?? throw new Exception($"Unable to deserialize file {filePath}");
            }
        }

        public void Reconstruct(string filePath)
        {
            var snapshot = DeserializeState(filePath);

            var workflow = _master.GetWorkflow(snapshot.WorkflowId);
            var bpmnProcess = workflow.BpmnWorkflow.BpmnProcesses.First(x => x.Id == snapshot.BpmnProcessId);

            var pi = (ProcessInstance)workflow.CreateProcessInstance(bpmnProcess);
            pi.Restore(_master, snapshot);

        }

        public IEnumerable<Tuple<string, string, string>> GetInfoForAllSnapshots()
        {
            List<Tuple<string, string, string>> result = new();

            string[] files = Directory.GetFiles(PiStateDirectory);
            foreach (var file in files)
            {
                GetWfPiFromFilePath(file, out string wfId, out string piId);
                result.Add(Tuple.Create(wfId, piId, file));
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
