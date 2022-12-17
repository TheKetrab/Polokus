using Polokus.Core.Externals;
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
        public void LoadExternals()
        {
            // Arrange
            var manager = new ExternalsManager();

            // Act
            var externals = manager.LoadExternals(Resources.TestExternals);

            // Assert
            Assert.AreEqual(2, externals.ContextInstances.Count);

            Assert.AreEqual("name1.bpmn", externals.ContextInstances[0].Name);
            Assert.AreEqual(2, externals.ContextInstances[0].ServiceTasks.Count);
            Assert.AreEqual("assembly1.dll", externals.ContextInstances[0].ServiceTasks[0].Assembly);

            Assert.AreEqual("name2.bpmn", externals.ContextInstances[1].Name);
            Assert.AreEqual(2, externals.ContextInstances[0].ServiceTasks.Count);
            Assert.AreEqual("className4", externals.ContextInstances[1].ServiceTasks[1].ClassName);

        }

    }
}
