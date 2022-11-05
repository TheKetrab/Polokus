using CefSharp;
using CefSharp.WinForms;
using Polokus.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    public class BpmnioClient : IBpmnClient
    {
        ChromiumWebBrowser chromiumWebBrowser;
        public BpmnioClient(ChromiumWindow chromiumWindow)
        {
            chromiumWebBrowser = chromiumWindow.chromeBrowser;
        }

        public async Task<string> GetBpmnSvg(string bpmnXml)
        {
            bpmnXml = bpmnXml.Replace('\n', ' ');            
            string script = GetPromisedScript("window.xml2Svg", $"\'{bpmnXml}\'");
            var result = await chromiumWebBrowser.EvaluateScriptAsPromiseAsync(script);
            return result.Result?.ToString() ?? throw new Exception("Script evaluation failed.");
        }

        private string GetPromisedScript(string function, params string[] args)
        {
            string arguments = string.Join(",", args);

            StringBuilder sb = new StringBuilder();
            sb.Append("return (async function() { const result = await ");
            sb.Append(function);
            sb.Append($"({arguments})");
            sb.Append("; return result; })();");
            return sb.ToString();
        }
    }
}
