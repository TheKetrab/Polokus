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
            Assert.AreEqual(2, externals.Workflows.Count);

            Assert.AreEqual("name1.bpmn", externals.Workflows[0].Name);
            Assert.AreEqual(2, externals.Workflows[0].ServiceTasks.Count);
            Assert.AreEqual("assembly1.dll", externals.Workflows[0].ServiceTasks[0].Assembly);

            Assert.AreEqual("name2.bpmn", externals.Workflows[1].Name);
            Assert.AreEqual(2, externals.Workflows[0].ServiceTasks.Count);
            Assert.AreEqual("className4", externals.Workflows[1].ServiceTasks[1].ClassName);

        }

    }
}
