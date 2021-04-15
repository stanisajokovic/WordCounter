using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Serilog;
using WordCounterClient.UIFactories;

namespace WordCounterClient
{
    class Program
    {
        static HttpClient client = new HttpClient();
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //services.AddSingleton<IWordCounterAPIClient, WordCounterAPIClient>() ;
                    //services.AddScoped<IWordCounterAPIClientCustom, WordCounterAPIClientCustom>();
                    services.AddSingleton<IUIFactory, UIFactory>();                   
                })
                
                .UseSerilog()
                .Build();

            Log.Logger.Information("poziv...");
            var svc = ActivatorUtilities.CreateInstance<UIFactory>(host.Services, client);
            svc.MainMenu();

            #if DEBUG
            Console.WriteLine("Za izlaz pritisnite bilo koje dugme...");
            Console.ReadLine();
            #endif
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"} .json", optional: true)
                .AddEnvironmentVariables();
        }
            
        private static async Task RunAsync()
        {
            AuthConfig config = AuthConfig.ReadJsonFromFile("appsettings.json");

            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
              .WithClientSecret(config.ClientSecret)
              .WithAuthority(new Uri(config.Authority))
              .Build();

            string[] ResourceId = new string[] { config.ResourceId };

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
            }

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                var httpClient = new HttpClient();
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if(defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(h => h.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);

                HttpResponseMessage responseMessage = await httpClient.GetAsync(config.BaseAddress);

                if (responseMessage.IsSuccessStatusCode)
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
