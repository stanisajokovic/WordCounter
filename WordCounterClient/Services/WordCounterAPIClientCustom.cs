using ClassLibrary.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{

    
    public class WordCounterAPIClientCustom : IWordCounterAPIClientCustom
    {
        private readonly ILogger<WordCounterAPIClientCustom> _log;
        private readonly IConfiguration _config;
        static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        public WordCounterAPIClientCustom(ILogger<WordCounterAPIClientCustom> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        private async Task<AuthenticationResult> AuthenticateAsync()
        {
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(_config.GetValue<string>("ClientId"))
              .WithClientSecret(_config.GetValue<string>("ClientSecret"))
              .WithAuthority(new Uri(_config.GetValue<string>("Authority")))
              .Build();

            string[] ResourceId = new string[] { _config.GetValue<string>("ResourceId") };

            return  await app.AcquireTokenForClient(ResourceId).ExecuteAsync();
        }


        public async Task<PhraseReadDto> GetAllPhrasesAsync()

        private async var authenticationResult = await AuthenticateAsync();
            if (!string.IsNullOrEmpty(authenticationResult.AccessToken))


    }
}
