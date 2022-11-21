using Polokus.Core.Interfaces;
using System;

namespace Polokus.App.Utils
{
    public class AppSettingsProvider : ISettingsProvider
    {
        public int MessageListenerPort => Properties.Settings.Default.MessageListenerPort;
    }

}