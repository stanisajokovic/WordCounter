using DTOsLibrary.DTOs;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using WordCounterClient.ApiClients;

namespace WordCounterClient.UserInterface
{
    class AddToDBOption : IMenuOption
    {
        public string Choice => "4";
        public string Description => "Unos u bazu";
        public void Execute(string accessToken, string baseUrl, HttpClient httpClient) 
        {
            Console.Write("Unestite text:\n");
            var input = Console.ReadLine();
            var client = new WordCounterAPIClient();

            var phraseCreateDto = new PhraseCreateDto { Text = input};
            
            var result = client.CallClientCreatePhraseAsync(accessToken, baseUrl, httpClient, phraseCreateDto).Result;

            if (result == null )
            {
                Console.Write("Neuspešno cuvanje!:\n");
            }
            else
            {
                Console.Write("Uspesno sacuvan text:\n");
                Console.WriteLine("-----------");
                Console.WriteLine("ID\t\tText");
                Console.WriteLine("-----------");
                Console.WriteLine("{0}\t\t{1}", result.Id, result.Text);
            }
            
            Console.ReadLine();
 
        }


      }
}
