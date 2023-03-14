using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces
{   
    public static class Settings
    {
        private static object _lock = new object();
        private static ISettingsProvider? _settingsProvider;
        public static ISettingsProvider SettingsProvider
        {
            get
            {
                lock (_lock)
                {
                    _settingsProvider ??= new IniSettingsProvider();
                    return _settingsProvider;
                }
            }
        }

        public static void RegisterSettingsProvider(ISettingsProvider settingsProvider)
        {
            lock (_lock)
            {
                _settingsProvider = settingsProvider;
            }
        }

        public static void ResetSettings()
        {
            BpmnPath = "./BPMN";
            RestoreProcessesOnStart = false;
            MessageListenerPort = 8085;
            ExternalsPath = "./externals.json";
            TimeoutForProcessSec = -1;
            DelayPerNodeHandlerMs = 0;
            UseRemotePolokus = false;
            RemotePolokusUri = "http://localhost:3000";
            LightMode = false;
            OnStartFunctions = "";
            SerializePiSnapshots = false;
        }

        #region Settings

        public static string BpmnPath
        {
            get => SettingsProvider.BpmnPath;
            set => SettingsProvider.BpmnPath = value;
        }

        public static bool RestoreProcessesOnStart
        {
            get => SettingsProvider.RestoreProcessesOnStart;
            set => SettingsProvider.RestoreProcessesOnStart = value;
        }

        public static int MessageListenerPort
        {
            get => SettingsProvider.MessageListenerPort;
            set => SettingsProvider.MessageListenerPort = value;
        }

        public static string ExternalsPath
        {
            get => SettingsProvider.ExternalsPath;
            set => SettingsProvider.ExternalsPath = value;
        }

        public static int TimeoutForProcessSec
        {
            get => SettingsProvider.TimeoutForProcessSec;
            set => SettingsProvider.TimeoutForProcessSec = value;
        }

        public static int DelayPerNodeHandlerMs
        {
            get => SettingsProvider.DelayPerNodeHandlerMs;
            set => SettingsProvider.DelayPerNodeHandlerMs = value;
        }

        public static bool UseRemotePolokus
        {
            get => SettingsProvider.UseRemotePolokus;
            set => SettingsProvider.UseRemotePolokus = true;
        }

        public static string RemotePolokusUri
        {
            get => SettingsProvider.RemotePolokusUri;
            set => SettingsProvider.RemotePolokusUri = value;
        }

        public static bool LightMode
        {
            get => SettingsProvider.LightMode;
            set => SettingsProvider.LightMode = value;
        }

        public static string OnStartFunctions
        {
            get => SettingsProvider.OnStartFunctions;
            set => SettingsProvider.OnStartFunctions = value;
        }

        public static bool SerializePiSnapshots
        {
            get => SettingsProvider.SerializePiSnapshots;
            set => SettingsProvider.SerializePiSnapshots = value;
        }

        public static string ScriptingLanguage
        {
            get => SettingsProvider.ScriptingLanguage;
            set => SettingsProvider.ScriptingLanguage = value;
        }

        public static bool ExperimentalFunctions
        {
            get => SettingsProvider.ExperimentalFunctions;
            set => SettingsProvider.ExperimentalFunctions = value;
        }

        #endregion
    }
}
