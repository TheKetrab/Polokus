using CefSharp;
using CefSharp.WinForms;
using Polokus.App.Views;
using System.Text;

namespace Polokus.App.Utils
{
    public class BpmnioClient : IBpmnClient
    {
        ChromiumWebBrowser chromiumWebBrowser;

        public BpmnioClient(ChromiumWindow chromiumWindow)
        {
            chromiumWebBrowser = chromiumWindow.chromeBrowser;

            chromiumWebBrowser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            chromiumWebBrowser.JavascriptObjectRepository.Register("callbackObj", new CallbackObjectForJs(), options: BindingOptions.DefaultBinder);

            //chromiumWebBrowser.RegisterAsyncJsObject("callbackObj", _callbackObjectForJs);
        }

        public async Task<string> GetBpmnSvg(string bpmnXml)
        {
            bpmnXml = bpmnXml.Replace('\n', ' ');
            string script = GetPromisedScript("window.xml2Svg", $"\'{bpmnXml}\'");
            var result = await chromiumWebBrowser.EvaluateScriptAsPromiseAsync(script);
            return result.Result?.ToString() ?? throw new Exception("Script evaluation failed.");
        }

        public static string GetPromisedScript(string function, params string[] args)
        {
            string arguments = string.Join(",", args);

            StringBuilder sb = new StringBuilder();
            sb.Append("return (async function() { const result = await ");
            sb.Append(function);
            sb.Append($"({arguments})");
            sb.Append("; return result; })();");
            return sb.ToString();
        }

        public static void SetColours(ChromiumWindow chw, IEnumerable<string> activeNodesIds, IEnumerable<string> inactiveNodesIds)
        {
            if (chw.chromeBrowser != null)
            {
                string activeArguments = string.Join(',', activeNodesIds.Select(x => $"\'{x}\'"));
                string inactiveArguments = string.Join(',', inactiveNodesIds.Select(x => $"\'{x}\'"));

                string script = $"window.api.updateColoursForNodes([{activeArguments}],[{inactiveArguments}])";
                chw.chromeBrowser.ExecuteScriptAsync(script);
            }
        }
    }
}
