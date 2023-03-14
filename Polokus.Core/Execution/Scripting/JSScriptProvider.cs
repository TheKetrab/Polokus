using Jint;

namespace Polokus.Core.Execution.Scripting
{
    public class JSScriptProvider : ScriptProvider
    {
        private void InjectGlobals(Engine engine)
        {
            string json = this.Globals.GetJson();
            engine.Execute($"globals = {json}");

        }

        private void SaveGlobals(Engine engine)
        {
            var globals = engine.GetValue("globals").ToObject();
            var dict = (IDictionary<string, object>)globals;
            this.Globals.SetValues(dict);
        }

        public override Task<T> EvalScriptAsync<T>(string script)
        {
            string script2 = MarkVariables(script);

            Engine engine = new Engine(options => options.AllowClr());

            InjectGlobals(engine);
            var res = engine.Evaluate(script2);
            SaveGlobals(engine);

            var res2 = Convert.ChangeType(res.ToObject(), typeof(T));
            T output = (T)res2;
            return Task.FromResult(output);
        }

        public override Task EvalScriptAsync(string script)
        {
            string script2 = MarkVariables(script);

            Engine engine = new Engine(options => options.AllowClr());

            InjectGlobals(engine);
            engine.Execute(script2);
            SaveGlobals(engine);

            return Task.CompletedTask;
        }
    }
}
