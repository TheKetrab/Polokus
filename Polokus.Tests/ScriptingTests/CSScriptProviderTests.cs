﻿using Polokus.Core.Execution.Scripting;

namespace Polokus.Tests.ScriptingTests
{
    public class CSScriptProviderTests
    {

        [Test]
        public async Task EvalCSharpScript_SimpleScript()
        {
            // Arrange
            var scriptProvider = new CSScriptProvider();

            // Act
            int x = await scriptProvider.EvalScriptAsync<int>("int x = 1; return x;");

            // Assert
            Assert.AreEqual(1, x);
        }

        [Test]
        [TestCase("$a", "globals[\"a\"]")]
        [TestCase("$variable", "globals[\"variable\"]")]
        [TestCase("$vaRIable abc", "globals[\"vaRIable\"] abc")]
        [TestCase("$vari77able + 5", "globals[\"vari77able\"] + 5")]
        [TestCase("3 + $variable = 4", "3 + globals[\"variable\"] = 4")]
        [TestCase("$var_underscore + 1", "globals[\"var_underscore\"] + 1")]
        [TestCase("$a $b $c", "globals[\"a\"] globals[\"b\"] globals[\"c\"]")]
        [TestCase("$aa + 1 = $bx and $aa - $bx = $c", "globals[\"aa\"] + 1 = globals[\"bx\"] and globals[\"aa\"] - globals[\"bx\"] = globals[\"c\"]")]
        [TestCase(
            "int y; y=(42+58) * (int)$x; $y = y; return y;",
            "int y; y=(42+58) * (int)globals[\"x\"]; globals[\"y\"] = y; return y;")]
        [TestCase("int x = 1;\r\n$a = x;", "int x = 1;\nglobals[\"a\"] = x;")]
        [TestCase("int x = 1;\n$a = x;", "int x = 1;\nglobals[\"a\"] = x;")]

        public void EvalCSharpScript_MarkVariables(string input, string output)
        {
            // Arrange
            var scriptProvider = new CSScriptProvider();

            // Act
            string marked = scriptProvider.MarkVariables(input);

            // Assert
            Assert.AreEqual(output, marked);
        }

        [Test]
        [TestCase("$a", true)]
        [TestCase("$_vlmq", true)]
        [TestCase("$a + 13", false)]
        [TestCase("", false)]
        public void IsValidOutgoingVariable_Tests(string str, bool valid)
        {
            // Arrange
            var scriptProvider = new CSScriptProvider();

            // Act
            bool result = scriptProvider.IsValidOutgoingVariable(str);

            // Assert
            Assert.AreEqual(valid, result);
        }


    }
}
