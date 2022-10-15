using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public static class Logger
    {
        enum MsgType
        {
            Simple,
            Warning,
            Error,
        }

        private static List<Tuple<MsgType, string>> messages = new();


        public static void Log(string msg)
        {
            Log(msg, MsgType.Simple);
        }

        public static void LogWarning(string msg)
        {
            Log(msg, MsgType.Warning);
        }

        public static void LogError(string msg)
        {
            Log(msg, MsgType.Error);
        }

        private static void Log(string msg, MsgType type)
        {
            messages.Add(new(type, msg));
        }

        public static string GetFullLog(bool prefixed = false)
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


    }
}
