using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Serilog;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using WordCounterClient.UserInterface;

namespace WordCounterClient.UIFactories
{
    public class UIFactory : IUIFactory
    {
        
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public UIFactory(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        private async Task<string> AuthTokenHttpMessage(IConfiguration config)
        {
            IConfidentialClientApplication app;

            var Authority = String.Format(CultureInfo.InvariantCulture,
                             config.GetValue<string>("Instance"), config.GetValue<string>("TenantId"));

            app = ConfidentialClientApplicationBuilder.Create(config.GetValue<string>("ClientId"))
              .WithClientSecret(config.GetValue<string>("ClientSecret"))
              .WithAuthority(new Uri(Authority))
              .Build();

            string[] ResourceId = new string[] { config.GetValue<string>("ResourceId") };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(ResourceId).ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired \n");
                Console.WriteLine(result.AccessToken);
                Console.ResetColor();

            }
            catch (MsalClientException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return string.Empty;
            }
            return result.AccessToken;
        }

        public void MainMenu()
        {
            try
            {
                var accessToken = AuthTokenHttpMessage(_config);
                var authResult = accessToken.Result;
                if (accessToken != null )
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Dobrodošli u WordCounter!");
                        Console.WriteLine("Izaberite opciju:");
                        var options = BuildMainMenu();

                        foreach (var option in options)
                        {
                            Console.WriteLine("\t{0}. {1}", option.Choice, option.Description);
                        }

                        // Read until the input is valid.
                        var userChoice = string.Empty;
                        var commandIndex = -1;
                        do
                        {
                            userChoice = Console.ReadLine();
                        }
                        while (!int.TryParse(userChoice, out commandIndex) || commandIndex > options.Length);

                        // Execute the command.
                        options[commandIndex - 1].Execute(authResult, _config.GetValue<string>("BaseUrl"), _httpClient);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Greška pri izvrsavanju: {0}", ex.Message);
                
            }
        }
        private static IMenuOption[] BuildMainMenu()
        {
           return new IMenuOption[]
           {
                new UserInputOption(),
                new DBOption(),
                new FileOption(),
                new AddToDBOption(),
                new DeleteFromDBOption(),
                new ExitOption()
            };
        }
    }
}