

using System;
using System.Net.Http;
using WordCounterClient.ApiClients;

namespace WordCounterClient.UserInterface
{
    class DBOption : IMenuOption
    {
        public string Choice => "2";
        public string Description => "Izbor iz baze";
        public void Execute(string accessToken, string baseUrl, HttpClient httpClient) 
        {
            Console.WriteLine("Izaberite red iz baze:");
            var client = new WordCounterAPIClient();
            var phrases  = client.CallClientGetPhrasesAsync(accessToken, baseUrl, httpClient).Result;
            if (phrases != null)
            {
                Console.WriteLine("-----------");
                Console.WriteLine("ID\t\tText");
                Console.WriteLine("-----------");
                foreach (var phrase in phrases)
                {
                    Console.WriteLine("{0}\t\t{1}", phrase.Id, phrase.Text);
                }
                    
                string id = Console.ReadLine();
  
                var text = client.CallClientGetPhraseByIdAsync(accessToken, baseUrl, httpClient, id).Result;

                var br = client.CallClientWordCountAsync(accessToken, baseUrl, httpClient, text.Text).Result;

                Console.WriteLine("Broj reci je:{0}", br);
                Console.ReadLine();
            }
        }
    }
}
