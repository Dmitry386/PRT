using RestProjectRouTeam.API.Contracts;
using RestProjectRouTeam.API.Helpers;
using RestProjectRouTeam.Application.Services;

namespace RestProjectRouTeam.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/register", Register);
            app.MapPost("api/login", Login);

            return app;
        }

        private static async Task<IResult> Register(RegisterUserRequest request, UsersService usersService)
        {
            await usersService.Register(request.UserName, request.Password);
            return Results.Ok();
        }

        private static async Task<IResult> Login(LoginUserRequest request, UsersService usersService, HttpContext context, IConfiguration config)
        {
            var token = await usersService.Login(request.UserName, request.Password);

            context.Response.Cookies.Append(ApiHelper.GetAuthCookieName(config), token);

            return Results.Ok(token);
        }
    }
}