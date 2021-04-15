
using System;
using System.Net.Http;


namespace WordCounterClient.UserInterface
{
    interface IMenuOption
    {
        string Choice { get; }
        string Description { get; }
        void Execute(string accessToken, string baseUrl, HttpClient httpClient);
    }

    class ExitOption : IMenuOption
    {
        public string Choice => "6";
        public string Description => "Exit.";
        public void Execute(string accessToken, string baseUrl, HttpClient httpClient) { Environment.Exit(0); }
    }
}
