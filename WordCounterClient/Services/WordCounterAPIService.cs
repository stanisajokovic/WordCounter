using ClassLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WordCounterClient
{
    public class WordCounterAPIService : IWordCounterAPIService
    {
        private readonly ILogger<WordCounterAPIService> _log;
        private readonly IConfiguration _config;

        public WordCounterAPIService(ILogger<WordCounterAPIService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void Run()
        {

            for (int i = 0; i < _config.GetValue<int>("loopTimes"); i++)
            {
                _log.LogInformation("Run number {runNumber}", i);
            }
        }

        private async Task<AuthenticationResult> AuthenticateAsync()
        {
            AuthConfig config = AuthConfig.ReadJsonFromFile("appsettings.json");

            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
              .WithClientSecret(config.ClientSecret)
              .WithAuthority(new Uri(config.Authority))
              .Build();

            string[] ResourceId = new string[] { config.ResourceId };

            AuthenticationResult result = null;

            return result = await app.AcquireTokenForClient(ResourceId).ExecuteAsync();

        }

        public async Task RunAsync()
        {
            var authenticationResult = await AuthenticateAsync();
            if (!string.IsNullOrEmpty(authenticationResult.AccessToken))
            {
                var baseAddress = _config.GetValue<string>("BaseAddress");
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResult.AccessToken);

                WordCounterAPIClient wordCounterAPIClient = new WordCounterAPIClient(baseAddress, httpClient);

                //var responseMessage = await httpClient.GetAsync(baseAddress);

               

                var responseMessage = await wordCounterAPIClient.GetPhraseByIdAsync(1).GetAwaiter().ToString;

                if (responseMessage.IsSuccessStatusCode
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call Web API: {responseMessage.StatusCode}");
                    string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {jsonResponse}");
                }
                Console.ResetColor();
            }
        }
    }
}
