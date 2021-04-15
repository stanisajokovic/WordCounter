using Serilog;
using System;
using System.Net.Http;
using WordCounterClient.ApiClients;

namespace WordCounterClient.UserInterface
{
    class FileOption : IMenuOption
    {
        public string Choice => "3";
        public string Description => "Upload sa fajla";
        public void Execute(string accessToken, string baseUrl, HttpClient httpClient) 
        {

            Console.WriteLine("Unesite putanju do txt fajla:\n");
            var path = Console.ReadLine();
            try
            {
                var text = System.IO.File.ReadAllText(path);

                var client = new WordCounterAPIClient();
                var result = client.CallClientWordCountAsync(accessToken, baseUrl, httpClient, text).Result;
                
                if (!string.IsNullOrEmpty(result))
                {
                    Console.WriteLine("Broj reci je:{0}", result);
                    
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Greška pri ucitavanju fajla: {0}", ex.Message);
                Console.ReadLine();
            }
            
        }
    }
}
