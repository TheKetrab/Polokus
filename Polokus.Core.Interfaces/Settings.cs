﻿using IniParser;
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
        public static Settings Instance
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
            BpmnPath = "./BPMN";
            EnableLogs = true;
            MessageListenerPort = 8085;
            ExternalsPath = "./externals.json";
            TimeoutForProcessSec = -1;
            DelayPerNodeHandlerMs = 0;
            UseRemotePolokus = false;
            RemotePolokusUri = "http://localhost:3000";
        }

        public static void ReloadSettings()
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
        public string bpmnPath
        {
            get => _bpmnPath ?? throw new SettingNotFoundException("BpmnPath");
            set
            {
                _bpmnPath = value;
                UpdateSetting(MainSection, "BpmnPath", value);
            }
        }
        public static string BpmnPath
        {
            get => Instance.bpmnPath;
            set => Instance.bpmnPath = value;
        }

        private bool? _enableLogs;
        public bool enableLogs
        {
            get => _enableLogs ?? true;
            set
            {
                _enableLogs = value;
                UpdateSetting(MainSection, "EnableLogs", value.ToString());
            }
        }
        public static bool EnableLogs 
        {
            get => Instance.enableLogs;
            set => Instance.enableLogs = value;
        }

        private int? _messageListenerPort;
        public int messageListenerPort
        {
            get => _messageListenerPort ?? throw new SettingNotFoundException("MessageListenerPort");
            set
            {
                _messageListenerPort = value;
                UpdateSetting(ServiceSection, "MessageListenerPort", value.ToString());
            }
        }
        public static int MessageListenerPort
        {
            get => Instance.messageListenerPort;
            set => Instance.messageListenerPort = value;
        }

        private string? _externalsPath;
        public string? externalsPath
        {
            get => _externalsPath;
            set
            {
                _externalsPath = value;
                UpdateSetting(ServiceSection, "ExternalsPath", value ?? "");
            }
        }
        public static string? ExternalsPath
        {
            get => Instance.externalsPath;
            set => Instance.externalsPath = value;
        }

        private int? _timeoutForProcessSec;
        public int timeoutForProcessSec
        {
            get => _timeoutForProcessSec ?? -1;
            set
            {
                _timeoutForProcessSec = value;
                UpdateSetting(ServiceSection, "TimeoutForProcessSec", value.ToString());
            }
        }
        public static int TimeoutForProcessSec
        {
            get => Instance.timeoutForProcessSec;
            set => Instance.timeoutForProcessSec = value;
        }
        

        private int? _delayPerNodeHandlerMs;
        public int delayPerNodeHandlerMs 
        {
            get => _delayPerNodeHandlerMs ?? 0;
            set
            {
                _delayPerNodeHandlerMs = value;
                UpdateSetting(AppSection, "DelayPerNodeHandlerMs", value.ToString());
            }
        }
        public static int DelayPerNodeHandlerMs
        {
            get => Instance.delayPerNodeHandlerMs;
            set => Instance.delayPerNodeHandlerMs = value;
        }

        private bool? _useRemotePolokus;
        public bool useRemotePolokus 
        {
            get => _useRemotePolokus ?? false;
            set
            {
                _useRemotePolokus = value;
                UpdateSetting(AppSection, "UseRemotePolokus", value.ToString());
            }
        }
        public static bool UseRemotePolokus
        {
            get => Instance.useRemotePolokus;
            set => Instance.useRemotePolokus = true;
        }

        private string? _remotePolokusUri;
        public string? remotePolokusUri 
        {
            get => _remotePolokusUri;
            set
            {
                _remotePolokusUri = value;
                UpdateSetting(AppSection, "RemotePolokusUri", value ?? "");
            }
        }
        public static string RemotePolokusUri
        {
            get => Instance.remotePolokusUri;
            set => Instance.remotePolokusUri = value;
        }

        #endregion
    }
}
