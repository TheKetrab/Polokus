namespace Polokus.Core.Interfaces.Exceptions
{
    public class SettingNotFoundException : PolokusException
    {
        public SettingNotFoundException() { }
        public SettingNotFoundException(string setting)
            : base($"Not found setting: {setting}")
        {
        }
    }
}
