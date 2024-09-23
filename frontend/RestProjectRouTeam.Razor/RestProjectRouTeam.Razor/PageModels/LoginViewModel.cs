using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestProjectRouTeam.Razor.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RestProjectRouTeam.Razor.PageModels
{
    public class LoginViewModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        private readonly IConfiguration _configuration;

        private string AuthCookieName => ApiHelper.GetApiCookieName(_configuration);

        private string ApiUrl => ApiHelper.GetApiUrl(_configuration);

        public LoginViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnPostAsync()
        {
            await LoginHandling();
        }

        public void OnPostButtonExit()
        {
            if (Request.Cookies[AuthCookieName] != null)
            {
                Response.Cookies.Delete(AuthCookieName);
            }
            Response.Redirect("/Login");
        }

        public async Task<bool> IsAuthorized()
        {
            string token = Request.Cookies[AuthCookieName];
            return !string.IsNullOrEmpty(token);
        }

        private async Task LoginHandling()
        {
            var url = ApiUrl;

            string url_login = url + "/api/login";
            string url_reg = url + "/api/register";

            var data = new { username = Username, password = Password };

            var login_response = await ApiHelper.PostAsync(new HttpClient(), url_login, data);
            login_response = JsonConvert.DeserializeObject(login_response).ToString();

            if (login_response.ToLower().Contains("user not found"))
            {
                await ApiHelper.PostAsync(new HttpClient(), url_reg, data);
                login_response = await ApiHelper.PostAsync(new HttpClient(), url_login, data);
            }

            string token = login_response;
            HttpContext.Response.Cookies.Append(AuthCookieName, token);

            //if (await IsAuthorized() == false)
            //{
            Response.Redirect("/Login");
            //}
        }
    }
}
