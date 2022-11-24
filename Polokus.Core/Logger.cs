using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class Logger
    {
        private static Logger _global = new();
        public static Logger Global => _global;

        public enum MsgType
        {
            Simple,
            Warning,
            Error,
        }

        private List<Tuple<MsgType, string>> messages = new();


        public void Log(string msg)
        {
            Log(msg, MsgType.Simple);
        }

        public void LogWarning(string msg)
        {
            Log(msg, MsgType.Warning);
        }

        public void LogError(string msg)
        {
            Log(msg, MsgType.Error);
        }

        private void Log(string msg, MsgType type)
        {
            messages.Add(new(type, msg));
        }

        public string GetFullLog(bool prefixed = false)
        {
            StringBuilder sb = new();

            foreach (var message in messages)
            {
                string prefix = prefixed ? MsgPrefix(message) : "";
                sb.AppendLine($"{prefix}{message.Item2}");
            }

            return sb.ToString();
        }

        private static string MsgPrefix(Tuple<MsgType,string> message)
        {
            return message.Item1 switch
            {
                MsgType.Simple =>  "    ",
                MsgType.Warning => "[!] ",
                MsgType.Error =>   "ERR ",
                _ => throw new ArgumentException()
            };
        }

        public IEnumerable<Tuple<MsgType, string>> GetMessages()
        {
            return messages;
        }

        public void ClearLog()
        {
            messages.Clear();
        }


    }
}
