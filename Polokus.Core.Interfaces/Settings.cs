using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IniParser;
using IniParser.Model;
using Polokus.Core.Interfaces.Exceptions;

namespace Polokus.Core.Interfaces
{
    public class Settings
    {
        const string ConfigFile = "config.ini";
        const string MainSection = "Main";
        const string AppSection = "App";
        const string ServiceSection = "Service";


        private static Settings? _instance;
        private static object _lock = new object();
        private static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Settings();
                        }
                    }
                }
                return _instance;
            }
        }

        private FileIniDataParser _parser;
        private string _configFile;
        private Settings()
        {
            _parser = new FileIniDataParser();
            _configFile = CreateOrGetConfigPath();

            LoadSettings();
        }


        public static void ResetSettings()
        {
            Instance.LoadSettings();
        }


        private string CreateOrGetConfigPath()
        {
            string maindir = AppDomain.CurrentDomain.BaseDirectory;
            string configFile = Path.Combine(maindir, ConfigFile);
            if (!File.Exists(configFile))
            {
                File.Create(configFile).Close();
            }

            return configFile;
        }

        private void LoadSettings()
        {
            lock (_lock)
            {
                IniData data = _parser.ReadFile(_configFile);

                // BpmnPath
                _bpmnPath = data[MainSection]["BpmnPath"];

                // EnableLogs
                {
                    if (bool.TryParse(data[MainSection]["EnableLogs"], out bool res))
                    {
                        _enableLogs = res;
                    }
                }

                // MessageListenerPort
                {
                    if (int.TryParse(data[ServiceSection]["MessageListenerPort"], out int res))
                    {
                        _messageListenerPort = res;
                    }
                }

                // ExternalsPath
                _externalsPath = data[ServiceSection]["ExternalsPath"];

                // TimeoutForProcessSec
                {
                    if (int.TryParse(data[ServiceSection]["TimeoutForProcessSec"], out int res))
                    {
                        _timeoutForProcessSec = res;
                    }
                }

                // DelayPerNodeHandlerMs
                {
                    if (int.TryParse(data[AppSection]["DelayPerNodeHandlerMs"], out int res))
                    {
                        _delayPerNodeHandlerMs = res;
                    }
                }

                // UseRemotePolokus
                {
                    if (bool.TryParse(data[AppSection]["UseRemotePolokus"], out bool res))
                    {
                        _useRemotePolokus = res;
                    }
                }

                // RemotePolokusUri
                _remotePolokusUri = data[AppSection]["RemotePolokusUri"];

            }
        }

        private void UpdateSetting(string section, string key, string value)
        {
            lock (_lock)
            {
                IniData data = _parser.ReadFile(_configFile);
                data[section][key] = value;
                _parser.WriteFile(_configFile, data);
            }
        }

        #region Settings

        private string? _bpmnPath;
        public static string BpmnPath
        {
            get => Instance._bpmnPath ?? throw new SettingNotFoundException("BpmnPath");
            set
            {
                Instance._bpmnPath = value;
                Instance.UpdateSetting(MainSection, "BpmnPath", value);
            }
        }

        private bool? _enableLogs;
        public static bool EnableLogs
        {
            get => Instance._enableLogs ?? true;
            set
            {
                Instance._enableLogs = value;
                Instance.UpdateSetting(MainSection, "EnableLogs", value.ToString());
            }
        }

        private int? _messageListenerPort;
        public static int MessageListenerPort
        {
            get => Instance._messageListenerPort ?? throw new SettingNotFoundException("MessageListenerPort");
            set
            {
                Instance._messageListenerPort = value;
                Instance.UpdateSetting(ServiceSection, "MessageListenerPort", value.ToString());
            }
        }

        private string? _externalsPath;
        public static string? ExternalsPath
        {
            get => Instance._externalsPath;
            set
            {
                Instance._externalsPath = value;
                Instance.UpdateSetting(ServiceSection, "ExternalsPath", value ?? "");
            }
        }

        private int? _timeoutForProcessSec;
        public static int TimeoutForProcessSec
        {
            get => Instance._timeoutForProcessSec ?? -1;
            set
            {
                Instance._timeoutForProcessSec = value;
                Instance.UpdateSetting(ServiceSection, "TimeoutForProcessSec", value.ToString());
            }
        }

        private int? _delayPerNodeHandlerMs;
        public static int DelayPerNodeHandlerMs 
        {
            get => Instance._delayPerNodeHandlerMs ?? 0;
            set
            {
                Instance._delayPerNodeHandlerMs = value;
                Instance.UpdateSetting(AppSection, "DelayPerNodeHandlerMs", value.ToString());
            }
        }

        private bool? _useRemotePolokus;
        public static bool UseRemotePolokus 
        {
            get => Instance._useRemotePolokus ?? false;
            set
            {
                Instance._useRemotePolokus = value;
                Instance.UpdateSetting(AppSection, "UseRemotePolokus", value.ToString());
            }
        }

        private string? _remotePolokusUri;
        public static string? RemotePolokusUri 
        {
            get => Instance._remotePolokusUri;
            set
            {
                Instance._remotePolokusUri = value;
                Instance.UpdateSetting(AppSection, "RemotePolokusUri", value ?? "");
            }
        }

        #endregion
    }
}
