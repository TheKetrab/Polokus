using Polokus.Core.Execution.Scripting;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Tests.ScriptingTests
{
    public class VariablesEncoderTests
    {

        [Test]
        public void VariablesEncoder_GetVariablesQueryString_SingleVar()
        {
            // Arrange
            IScriptVariables variables = new ScriptVariables();
            variables.SetValue("a", 17);
            variables.SetValue("b", 42);

            // Act
            string? str = VariablesEncoder.GetVariablesQueryString(variables, "$a");

            // Assert
            Assert.AreEqual("variables={a,17,Int32}", str);
        }

        [Test]
        public void VeriablesEncoder_GetVariablesQueryString_MultipleVars()
        {
            // Arrange
            IScriptVariables variables = new ScriptVariables();
            variables.SetValue("a", 17);
            variables.SetValue("b", 42);
            variables.SetValue("c", 123f);

            // Act
            string? str = VariablesEncoder.GetVariablesQueryString(variables, "$a, $c");

            // Assert
            Assert.AreEqual("variables={a,17,Int32;c,123,Single}", str);
        }

        [Test]
        public void VeriablesEncoder_SetVariablesFromQueryString_AddNewVar()
        {
            // Arrange
            IScriptVariables variables = new ScriptVariables();
            string str = "{a,17,Int32;b,42.5,Double}";

            // Act
            VariablesEncoder.SetVariablesFromQueryString(variables, str);

            // Assert
            Assert.AreEqual(variables.GetValue("a"), 17);
            Assert.AreEqual(variables.GetValue("b"), 42.5);
        }

        [Test]
        public void VeriablesEncoder_SetVariablesFromQueryString_UpdateVar()
        {
            // Arrange
            IScriptVariables variables = new ScriptVariables();
            variables.SetValue("a", 17);
            variables.SetValue("b", 42);

            string str = "{a,123,Int32;b,42,Single}";

            // Act
            VariablesEncoder.SetVariablesFromQueryString(variables, str);

            // Assert
            Assert.AreEqual(variables.GetValue("a"), 123);
            Assert.AreEqual(variables.GetValue("b").GetType(), typeof(float));
        }


    }
}
