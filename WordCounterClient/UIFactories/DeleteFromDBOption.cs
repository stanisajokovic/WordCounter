using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WordCounterClient.ApiClients;

namespace WordCounterClient.UserInterface
{
    class DeleteFromDBOption : IMenuOption
    {
        public string Choice => "5";
        public string Description => "Brisanje iz u baze";
        public void Execute(string accessToken, string baseUrl, HttpClient httpClient) 
        {
            Console.Write("Izaberite broj reda za brisanje:\n");
            var client = new WordCounterAPIClient();
            var phrases = client.CallClientGetPhrasesAsync(accessToken, baseUrl, httpClient).Result;
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

                var br = client.CallClientDeleteByIdAsync(accessToken, baseUrl, httpClient, id).Result;

                if (br)
                {
                    Console.WriteLine("Red je uspesno obrisan. Pritisnie bilo koje dugme za nastavak", br);
                    
                }
                Console.ReadLine();

            }
        }
    }
}
