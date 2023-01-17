using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.ExternalsExample.MailMonitoring
{
    public class GmailManager
    {
        private readonly string[] Scopes = { GmailService.Scope.GmailReadonly };
        const string ApplicationName = "Polokus";

        public GmailService Service { get; set; }

        public GmailManager(string gmailCredentialsPath)
        {
            Service = CreateGmailService(gmailCredentialsPath);
        }

        private GmailService CreateGmailService(string gmailCredentialPath)
        {
            var credential = GetCredentials(gmailCredentialPath);
            return new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        private UserCredential GetCredentials(string gmailCredentialsPath)
        {
            UserCredential credential;
            using (var stream = new FileStream(gmailCredentialsPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                ).Result;
            }

            return credential;
        }

        public async Task<string?> GetNewestMailId()
        {
            var request = Service.Users.Messages.List("me");
            var res = await request.ExecuteAsync();
            return res.Messages.FirstOrDefault()?.Id;
        }

        public struct MailData
        {
            public string Topic { get; set; }
            public string Sender { get; set; }
        }

        public async Task<MailData> GetMailData(string id)
        {
            var request = Service.Users.Messages.Get("me", id);
            var res = await request.ExecuteAsync();
            string topic = res.Payload.Headers.First(x => x.Name == "Subject").Value;
            string sender = res.Payload.Headers.First(x => x.Name == "From").Value;

            return new MailData()
            {
                Topic = topic,
                Sender = sender
            };
        }
    }
}
