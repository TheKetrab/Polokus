using Jint.Runtime;
using Polokus.Core.Execution.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.ScriptingTests
{
    public class JSScriptProviderTests
    {
        [Test]
        public async Task EvalJSScript_SimpleScript()
        {
            // Arrange
            var scriptProvider = new JSScriptProvider();

            // Act
            int x = await scriptProvider.EvalScriptAsync<int>("let x = 1; return x;");

            // Assert
            Assert.AreEqual(1, x);
        }

        [Test]
        public async Task EvalJSScript_SimpleCondition()
        {
            // Arrange
            var scriptProvider = new JSScriptProvider();

            // Act
            bool b = await scriptProvider.EvalScriptAsync<bool>("let x = 1; let y = 0.5; return x > y;");

            // Assert
            Assert.AreEqual(true, b);
        }

        [Test]
        public async Task EvalJSScript_ScriptWithForLoop()
        {
            // Arrange
            var scriptProvider = new JSScriptProvider();

            // Act
            bool b = await scriptProvider.EvalScriptAsync<bool>("let x = 0; for (let i=0; i<10; i++) { x += 0.11; } return x > 1;");

            // Assert
            Assert.AreEqual(true, b);
        }


        [Test]
        public async Task EvalJSScript_DifferentScopes_UnknownLocalVar()
        {
            // Arrange
            var scriptProvider = new JSScriptProvider();

            // Act
            await scriptProvider.EvalScriptAsync("let x = 1;");

            // Assert
            Assert.Throws<JavaScriptException>(() => scriptProvider.EvalScriptAsync("return x == 1;"));
        }

        [Test]
        public async Task EvalJSScript_DifferentScopes_GlobalVariable()
        {
            // Arrange
            var scriptProvider = new JSScriptProvider();

            // Act
            await scriptProvider.EvalScriptAsync("$x = 1;");
            bool b = await scriptProvider.EvalScriptAsync<bool>("$x == 1;");

            // Assert
            Assert.AreEqual(true, b);
        }

        [Test]
        public async Task EvalJSScript_DifferentScopes_GlobalsAndLocals()
        {
            // Arrange
            var scriptProvider = new JSScriptProvider();

            // Act
            await scriptProvider.EvalScriptAsync("let x = 1; $x = x + 3;");
            await scriptProvider.EvalScriptAsync("let y = 2; for (let i=0; i<10; i++) y*=2; $z = 2 + y;");
            int res = await scriptProvider.EvalScriptAsync<int>("return $x + $z");

            // Assert
            Assert.AreEqual(2054, res);
        }

    }
}
