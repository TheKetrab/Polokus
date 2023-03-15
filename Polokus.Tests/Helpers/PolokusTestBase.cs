using Polokus.Core.Interfaces;

namespace Polokus.Tests.Helpers
{
    public class PolokusTestBase
    {
        [SetUp]
        public virtual void Init()
        {
            Settings.RegisterSettingsProvider(new TestsSettingsProvider());
        }
    }
}
