using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Lib;
using Microsoft.CodeAnalysis.Scripting;

namespace Polokus.Tests
{
    public class ScriptProviderTests
    {

        [Test]
        public async Task EvalCSharpScript_SimpleScript()
        {
            // Arrange
            var scriptProvider = new BpmnContext().ScriptProvider;

            // Act
            int x = await scriptProvider.EvalCSharpScriptAsync<int>("int x = 1; return x;");

            // Assert
            Assert.AreEqual(1, x);
        }


    }
}
