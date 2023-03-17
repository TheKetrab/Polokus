using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.Extensibility;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;

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

        /// <summary>
        /// Registers object that provides settings from a static context.
        /// </summary>
        /// <param name="settingsProvider">Object that provides implementation and logic of settings.</param>
        public static void RegisterSettingsProvider(ISettingsProvider settingsProvider)
        {
            lock (_lock)
            {
                _settingsProvider = settingsProvider;
            }
        }

        /// <summary>
        /// Sets all settings to default values.
        /// </summary>
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

        /// <summary>
        /// Path to directory with .bpmn files that will be parsed to Workflows.
        /// </summary>
        public static string BpmnPath
        {
            get => SettingsProvider.BpmnPath;
            set => SettingsProvider.BpmnPath = value;
        }

        /// <summary>
        /// If application close when there are some not finished processes,
        /// service will continue processing it when this flag is set.
        /// </summary>
        public static bool RestoreProcessesOnStart
        {
            get => SettingsProvider.RestoreProcessesOnStart;
            set => SettingsProvider.RestoreProcessesOnStart = value;
        }

        /// <summary>
        /// Message communication within a process is made with pinging localhost port.
        /// </summary>
        public static int MessageListenerPort
        {
            get => SettingsProvider.MessageListenerPort;
            set => SettingsProvider.MessageListenerPort = value;
        }

        /// <summary>
        /// Path to file with externals.
        /// </summary>
        public static string ExternalsPath
        {
            get => SettingsProvider.ExternalsPath;
            set => SettingsProvider.ExternalsPath = value;
        }

        /// <summary>
        /// Maximum time after which kill single process if not finished yet. -1 means infinity.
        /// </summary>
        public static int TimeoutForProcessSec
        {
            get => SettingsProvider.TimeoutForProcessSec;
            set => SettingsProvider.TimeoutForProcessSec = value;
        }

        /// <summary>
        /// When app is connected to Polokus Service, executing of every node can be delayed
        /// by time defined here so that you can see on diagram which node is active.
        /// </summary>
        public static int DelayPerNodeHandlerMs
        {
            get => SettingsProvider.DelayPerNodeHandlerMs;
            set => SettingsProvider.DelayPerNodeHandlerMs = value;
        }

        /// <summary>
        /// Flag to decide if you want to connect application to service (true)
        /// or you want to run processes only inside app (false).
        /// </summary>
        public static bool UseRemotePolokus
        {
            get => SettingsProvider.UseRemotePolokus;
            set => SettingsProvider.UseRemotePolokus = true;
        }

        /// <summary>
        /// Endpoint on which service is listening.
        /// </summary>
        public static string RemotePolokusUri
        {
            get => SettingsProvider.RemotePolokusUri;
            set => SettingsProvider.RemotePolokusUri = value;
        }

        /// <summary>
        /// Changes view of application to a brighter one.
        /// </summary>
        public static bool LightMode
        {
            get => SettingsProvider.LightMode;
            set => SettingsProvider.LightMode = value;
        }

        /// <summary>
        /// (Experimental) - Service will do the function on start. Functions should be separated with #.
        /// <example>For example:
        /// <code>
        /// StartProcessManually(wfId:workflow_1,piId:process_1,cnt:30) # StartProcessManually(wfId:workflow_1,piId:process_2,cnt:50)
        /// </code>
        /// starts 30 instances of process_1 and 50 instances of process_2
        /// </example>
        /// </summary>
        public static string OnStartFunctions
        {
            get => SettingsProvider.OnStartFunctions;
            set => SettingsProvider.OnStartFunctions = value;
        }

        /// <summary>
        /// Flag to decide if you want to save state of process instances in case of failure.
        /// </summary>
        public static bool SerializePiSnapshots
        {
            get => SettingsProvider.SerializePiSnapshots;
            set => SettingsProvider.SerializePiSnapshots = value;
        }

        /// <summary>
        /// Language in which scripts and conditions on sequences are computed. (JS - JavaScript, CS - C#)
        /// </summary>
        public static string ScriptingLanguage
        {
            get => SettingsProvider.ScriptingLanguage;
            set => SettingsProvider.ScriptingLanguage = value;
        }

        /// <summary>
        /// Flag to activate experimental functions (beta only).
        /// </summary>
        public static bool ExperimentalFunctions
        {
            get => SettingsProvider.ExperimentalFunctions;
            set => SettingsProvider.ExperimentalFunctions = value;
        }

        #endregion
    }
}
