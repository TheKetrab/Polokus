using IniParser.Model;
using IniParser;
using Polokus.Core.Interfaces.Exceptions;

namespace Polokus.Core.Interfaces.Extensibility
{
    public class IniSettingsProvider : ISettingsProvider
    {
        const string ConfigFile = "config.ini";
        const string MainSection = "Main";
        const string AppSection = "App";
        const string ServiceSection = "Service";

        private object _lock = new object();
        private FileIniDataParser _parser;
        private string _configFile;
        public IniSettingsProvider()
        {
            _parser = new FileIniDataParser();
            _configFile = CreateOrGetConfigPath();
            LoadSettings();
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

                // RestoreProcessesOnStart
                {
                    if (bool.TryParse(data[ServiceSection]["RestoreProcessesOnStart"], out bool res))
                    {
                        _restoreProcessesOnStart = res;
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

                // LightMode
                {
                    if (bool.TryParse(data[AppSection]["LightMode"], out bool res))
                    {
                        _lightMode = res;
                    }
                }

                // OnStartFunctions
                _onStartFunctions = data[ServiceSection]["OnStartFunctions"];

                // SerializePiSnapshots
                {
                    if (bool.TryParse(data[MainSection]["SerializePiSnapshots"], out bool res))
                    {
                        _serializePiSnapshots = res;
                    }
                }


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
        public string BpmnPath
        {
            get => _bpmnPath ?? throw new SettingNotFoundException("BpmnPath");
            set
            {
                _bpmnPath = value;
                UpdateSetting(MainSection, "BpmnPath", value);
            }
        }

        private bool? _restoreProcessesOnStart;
        public bool RestoreProcessesOnStart
        {
            get => _restoreProcessesOnStart ?? false;
            set
            {
                _restoreProcessesOnStart = value;
                UpdateSetting(MainSection, "RestoreProcessesOnStart", value.ToString());
            }
        }

        private int? _messageListenerPort;
        public int MessageListenerPort
        {
            get => _messageListenerPort ?? throw new SettingNotFoundException("MessageListenerPort");
            set
            {
                _messageListenerPort = value;
                UpdateSetting(ServiceSection, "MessageListenerPort", value.ToString());
            }
        }

        private string? _externalsPath;
        public string ExternalsPath
        {
            get => _externalsPath ?? throw new SettingNotFoundException("ExternalsPath");
            set
            {
                _externalsPath = value;
                UpdateSetting(ServiceSection, "ExternalsPath", value ?? "");
            }
        }

        private int? _timeoutForProcessSec;
        public int TimeoutForProcessSec
        {
            get => _timeoutForProcessSec ?? -1;
            set
            {
                _timeoutForProcessSec = value;
                UpdateSetting(ServiceSection, "TimeoutForProcessSec", value.ToString());
            }
        }

        private int? _delayPerNodeHandlerMs;
        public int DelayPerNodeHandlerMs
        {
            get => _delayPerNodeHandlerMs ?? 0;
            set
            {
                _delayPerNodeHandlerMs = value;
                UpdateSetting(AppSection, "DelayPerNodeHandlerMs", value.ToString());
            }
        }

        private bool? _useRemotePolokus;
        public bool UseRemotePolokus
        {
            get => _useRemotePolokus ?? false;
            set
            {
                _useRemotePolokus = value;
                UpdateSetting(AppSection, "UseRemotePolokus", value.ToString());
            }
        }

        private string? _remotePolokusUri;
        public string RemotePolokusUri
        {
            get
            {
                return _remotePolokusUri ?? throw new SettingNotFoundException("RemotePolokusUri");
            }

            set
            {
                _remotePolokusUri = value;
                UpdateSetting(AppSection, "RemotePolokusUri", value ?? "");
            }
        }

        private bool? _lightMode;
        public bool LightMode
        {
            get => _lightMode ?? false;
            set
            {
                _lightMode = value;
                UpdateSetting(AppSection, "LightMode", value.ToString() ?? "False");
            }
        }

        private string? _onStartFunctions;
        public string OnStartFunctions
        {
            get => _onStartFunctions ?? "";
            set
            {
                _onStartFunctions = value;
                UpdateSetting(ServiceSection, "OnStartFunctions", value);
            }
        }

        private bool? _serializePiSnapshots;
        public bool SerializePiSnapshots
        {
            get => _serializePiSnapshots ?? false;
            set
            {
                _serializePiSnapshots = value;
                UpdateSetting(MainSection, "SerializePiSnapshots", value.ToString());
            }
        }

        #endregion

    }
}
