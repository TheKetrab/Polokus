using Polokus.Core.Externals;
using Polokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.ExternalsTests
{
    public class DeserializationTests
    {
        [Test]
        public void LoadExternals_LoadServiceTasks()
        {
            // Arrange

            // Act
            var externals = ExternalsManager.LoadExternals(Resources.TestExternals);

            // Assert
            Assert.AreEqual(2, externals.Workflows.Count);

            Assert.AreEqual("name1.bpmn", externals.Workflows[0].Name);
            Assert.AreEqual(2, externals.Workflows[0].ServiceTasks.Count);
            Assert.AreEqual("assembly1.dll", externals.Workflows[0].ServiceTasks[0].Assembly);

            Assert.AreEqual("name2.bpmn", externals.Workflows[1].Name);
            Assert.AreEqual(2, externals.Workflows[0].ServiceTasks.Count);
            Assert.AreEqual("className4", externals.Workflows[1].ServiceTasks[1].ClassName);

        }

        [Test]
        public void LoadExternals_LoadMonitors()
        {
            // Arrange

            // Act
            var externals = ExternalsManager.LoadExternals(Resources.TestExternals);

            // Assert
            Assert.AreEqual(1, externals.Monitors.Count);
            Assert.AreEqual("ass1", externals.Monitors[0].Assembly);
            Assert.AreEqual("namespace.class.type", externals.Monitors[0].ClassName);
            Assert.AreEqual(1, externals.Monitors[0].Arguments.Length);
            Assert.AreEqual("arg1", externals.Monitors[0].Arguments[0]);

        }

        [Test]
        public void LoadExternals_HooksProviders()
        {
            // Arrange

            // Act
            var externals = ExternalsManager.LoadExternals(Resources.TestExternals);

            // Assert
            Assert.AreEqual(2, externals.HooksProviders.Count);

            Assert.AreEqual("assembly1.dll", externals.HooksProviders[0].Assembly);
            Assert.AreEqual("HooksProvider1", externals.HooksProviders[0].ClassName);

            Assert.AreEqual("assembly2.dll", externals.HooksProviders[1].Assembly);
            Assert.AreEqual("HooksProvider2", externals.HooksProviders[1].ClassName);
        }

        [Test]
        public void LoadExternals_SettingsProvider()
        {
            // Arrange

            // Act
            var externals = ExternalsManager.LoadExternals(Resources.TestExternals);

            // Assert
            Assert.AreEqual("SettingsProviderAssembly.dll", externals.SettingsProvider.Assembly);
            Assert.AreEqual("SettingsProviderClass", externals.SettingsProvider.ClassName);
        }

    }
}
