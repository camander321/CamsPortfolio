using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Portfolio.Models
{
    public class GithubRepo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        public void GetTopStarred()
        {
            RestClient client = new RestClient("https://api.github.com");
            RestRequest request = new RestRequest("search/repositories", Method.GET);
            request.AddParameter("q", "user:camander321");
            request.AddParameter("sort", "stars");
            request.AddParameter("per_page", "3");
            request.AddHeader("User-Agent", "camander321");

            client.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response);
            });
        }
    }  
}
