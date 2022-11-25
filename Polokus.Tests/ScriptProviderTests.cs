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

        [Test]
        [TestCase("$a","globals[\"a\"]")]
        [TestCase("$variable", "globals[\"variable\"]")]
        [TestCase("$vaRIable abc", "globals[\"vaRIable\"] abc")]
        [TestCase("$vari77able + 5", "globals[\"vari77able\"] + 5")]
        [TestCase("3 + $variable = 4", "3 + globals[\"variable\"] = 4")]
        [TestCase("$var_underscore + 1", "globals[\"var_underscore\"] + 1")]
        [TestCase("$a $b $c", "globals[\"a\"] globals[\"b\"] globals[\"c\"]")]
        [TestCase("$aa + 1 = $bx and $aa - $bx = $c", "globals[\"aa\"] + 1 = globals[\"bx\"] and globals[\"aa\"] - globals[\"bx\"] = globals[\"c\"]")]
        public void EvalCSharpScript_MarkVariables(string input, string output)
        {
            // Arrange
            var scriptProvider = new ScriptProvider();

            // Act
            string marked = scriptProvider.MarkVariables(input);

            // Assert
            Assert.AreEqual(output, marked);
        }



    }
}
