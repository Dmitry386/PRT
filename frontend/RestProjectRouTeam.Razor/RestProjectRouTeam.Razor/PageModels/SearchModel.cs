using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestProjectRouTeam.Razor.Contracts;

namespace RestProjectRouTeam.FrontEnd.PageModels
{
    public class SearchModel : PageModel
    {
        public List<dynamic> Repositories = new();

        public SearchModel()
        {

        }

        public async Task OnPostAsync()
        {
            //dynamic hz = new System.Dynamic.ExpandoObject();
            //hz.Name = "name";
            //hz.Author = "test";
            //hz.HtmlUrl = "url";
            //hz.StargazersCount = 1;
            //hz.WatchersCount = 2;
            //Repositories.Add(hz);
            //Repositories.Add(hz);
            //Repositories.Add(hz);
            //Repositories.Add(hz);
            string request = Request.Form["query"];
            var res = await FetchAsync(SharedClient, $"https://localhost:7298/api/find?subject={request}");
            Repositories.Clear();

            foreach (var item in res)
            {
                string json = item.json;
                var obj = JsonConvert.DeserializeObject(json);
                Repositories.Add(obj);
            }
        }

        private static async Task<dynamic> FetchAsync(HttpClient httpClient, string request)
        {
            var response = await httpClient.GetStringAsync(request);
            dynamic result = JsonConvert.DeserializeObject(response);

            return result;
        }

        private static HttpClient SharedClient = new()
        {

        };
    }
}