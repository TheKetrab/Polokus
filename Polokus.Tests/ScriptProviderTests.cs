using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Scripting;

namespace Polokus.Tests
{
    public class ScriptProviderTests
    {

        [Test]
        public async Task EvalCSharpScript_SimpleScript()
        {
            // Arrange
            var scriptProvider = new ScriptProvider();

            // Act
            int x = await scriptProvider.EvalCSharpScriptAsync<int>("int x = 1; return x;");

            // Assert
            Assert.AreEqual(1, x);
        }


    }
}
