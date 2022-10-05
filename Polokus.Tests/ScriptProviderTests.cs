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

        [Test]
        public async Task EvalCSharpScript_ImplicitCasting()
        {
            // Arrange
            var scriptProvider = new BpmnContext().ScriptProvider;

            // Act
            await scriptProvider.EvalCSharpScriptAsync("$x = 1");
            string markedScript = scriptProvider.MarkVariables("$x");

            // Assert
            Assert.AreEqual("(int)globals[\"x\"]", markedScript);

        }

        [Test]
        public async Task EvalCSharpScript_CastingFailedBeforeInitialized()
        {
            // Arrange
            var scriptProvider = new BpmnContext().ScriptProvider;

            // Assert
            var exc = Assert.ThrowsAsync<CompilationErrorException>(async () =>
                await scriptProvider.EvalCSharpScriptAsync<int>("$x = 1; return $x > 1;"));

            Assert.IsTrue(exc.Message.Contains(">"));
        }

    }
}
