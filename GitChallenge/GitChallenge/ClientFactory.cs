using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace GitChallenge
{
    internal class ClientFactory
    {
        private static HttpClient Client;

        static ClientFactory()
        {
            
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com/"),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };

            Client.DefaultRequestHeaders.Add("User-Agent", "UserAgent");
        }

        internal static HttpClient GetGitClient()
        {
            return Client;
        }
    }
}
