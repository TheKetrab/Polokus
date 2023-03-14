namespace Polokus.Core.Extensibility
{
    public class DefaultSettingsProvider : ISettingsProvider
    {
        public virtual string BpmnPath { get; set; } = "./BPMN";
        public virtual bool RestoreProcessesOnStart { get; set; } = false;
        public int MessageListenerPort { get; set; } = 8085;
        public virtual string ExternalsPath { get; set; } = "./externals.json";
        public virtual int TimeoutForProcessSec { get; set; } = -1;
        public virtual int DelayPerNodeHandlerMs { get; set; } = 300;
        public virtual bool UseRemotePolokus { get; set; } = false;
        public virtual string RemotePolokusUri { get; set; } = "";
        public virtual bool LightMode { get; set; } = false;
        public virtual string OnStartFunctions { get; set; } = "";
        public virtual bool SerializePiSnapshots { get; set; } = false;
        public string ScriptingLanguage { get; set; } = "JS";
        public bool ExperimentalFunctions { get; set; } = false;

    }
}
