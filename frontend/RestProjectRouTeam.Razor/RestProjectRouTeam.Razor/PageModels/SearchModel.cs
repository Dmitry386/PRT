using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestProjectRouTeam.Razor.Helpers;
using System.Net;

namespace RestProjectRouTeam.FrontEnd.PageModels
{
    public class SearchModel : PageModel
    {
        public List<dynamic> Repositories = new();

        private readonly IConfiguration _configuration;

        private string AuthCookieName => ApiHelper.GetApiCookieName(_configuration);
        
        public SearchModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnPostAsync()
        {
            try
            {
                string request = Request.Form["query"];
                var res = await ApiHelper.FetchAsync(GetHttpClient(), $"https://localhost:7298/api/find?subject={request}");
                Repositories.Clear();

                foreach (var item in res)
                {
                    string json = item.json;
                    var obj = JsonConvert.DeserializeObject(json);
                    Repositories.Add(obj);
                }
            }
            catch
            {
                Response.Redirect("/Login");
            }
        }

        public HttpClient GetHttpClient()
        {
            var url = _configuration.GetSection("ApiUrl").Value + "/";
            Uri target = new Uri(url);

            var httpClientHandler = new HttpClientHandler();
            var  cookies = new CookieContainer();

            string c1 = Request.Cookies[AuthCookieName];
            cookies.Add(new Cookie(AuthCookieName, c1) { Domain = target.Host });
            httpClientHandler.CookieContainer = cookies;

            return new HttpClient(httpClientHandler);
        }
    }
}