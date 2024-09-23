namespace RestProjectRouTeam.API.Helpers
{
    public class ApiHelper
    { 
        public static string GetAuthCookieName(IConfiguration config)
        {
            return config.GetSection("ApiAuthCookie")?.Value;
        } 
    }
}