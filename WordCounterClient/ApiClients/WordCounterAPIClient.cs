using DTOsLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WordCounterClient.ApiClients
{
    public class WordCounterAPIClient
    {
       
        public WordCounterAPIClient()
        {
            
        }


        public async Task<PhraseReadDto> CallClientGetPhraseByIdAsync(string accessToken, string baseUrl, HttpClient httpClient,  string id)
        {
            if (accessToken != null)
            {
                 var path = baseUrl + "/api/wordcounter/" + id;

                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var responceBody = await response.Content.ReadAsAsync<PhraseReadDto>();
                    return responceBody;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
                return null;
            }
            else
            {
                return null;
            }
            
        }

        public  async Task<string> CallClientWordCountAsync(string accessToken, string baseUrl, HttpClient httpClient, string text)
        {
            if (accessToken != null)
            {
                var path = baseUrl + "/api/wordcounter/count";

                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await httpClient.PostAsJsonAsync(path, text);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responceBody = await response.Content.ReadAsStringAsync();
                    return responceBody;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
                return null;
            }
            return null;
        }

        public async Task<PhraseReadDto> CallClientCreatePhraseAsync(string accessToken, string baseUrl, HttpClient httpClient, PhraseCreateDto phraseCreateDto)
        {
            if (accessToken != null)
            {
                var path = baseUrl + "/api/wordcounter";

                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await httpClient.PostAsJsonAsync(path, phraseCreateDto);


                if (response.IsSuccessStatusCode)
                {
                    var responceBody = await response.Content.ReadAsAsync<PhraseReadDto>();
                    return responceBody;


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
                return null;
            }
            return null;
        }

        public async Task<bool> CallClientDeleteByIdAsync(string accessToken, string baseUrl, HttpClient httpClient, string id)
        {
            if (accessToken != null)
            {
                var path = baseUrl  + "/api/wordcounter/" + id;

                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await httpClient.DeleteAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
                return false;
            }
            else
            {
                return false;
            }

        }

        public async Task<List<PhraseReadDto>> CallClientGetPhrasesAsync(string accessToken, string baseUrl, HttpClient httpClient)
        {
            if (accessToken != null)
            {
                var path = baseUrl + "/api/wordcounter";

                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var responceBody = await response.Content.ReadAsAsync<List<PhraseReadDto>>();
                    return responceBody;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
                return null;
            }
            else
            {
                return null;
            }

        }

    }
}
