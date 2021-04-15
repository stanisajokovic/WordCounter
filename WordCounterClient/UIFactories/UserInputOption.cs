using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WordCounterClient.ApiClients;

namespace WordCounterClient.UserInterface
{
    class UserInputOption : IMenuOption
    {
        public string Choice => "1";
        public string Description => "Rucni unos";
        public void Execute(string accessToken, string baseUrl, HttpClient httpClient)
        {
            Console.WriteLine("Unesite text:\n");
            var text = Console.ReadLine();

            var client = new WordCounterAPIClient();
            
            var result = client.CallClientWordCountAsync(accessToken, baseUrl, httpClient, text);
            
         
            if (!string.IsNullOrEmpty(result.Result))
            {
                Console.WriteLine("Broj reci je:{0}", result.Result);
            }
            Console.ReadLine();
            
        }
    }
}
