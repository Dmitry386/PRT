using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestProjectRouTeam.API.Endpoints;
using RestProjectRouTeam.API.Helpers;
using RestProjectRouTeam.Infrastructure;
using System.Text;

namespace RestProjectRouTeam.API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapUsersEndpoints();
        }

        public static void AddApiAuthentification(this IServiceCollection services, IConfiguration config)
        {
            var jwtOptions = new JwtOptions();
            config.GetSection(nameof(JwtOptions)).Bind(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[ApiHelper.GetAuthCookieName(config)];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
