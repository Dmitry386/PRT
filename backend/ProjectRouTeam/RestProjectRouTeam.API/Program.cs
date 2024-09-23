using Microsoft.EntityFrameworkCore;
using RestProjectRouTeam.API.Extensions;
using RestProjectRouTeam.API.Helpers;
using RestProjectRouTeam.Application.Services;
using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.DataAccess.Contexts;
using RestProjectRouTeam.DataAccess.Repositories;
using RestProjectRouTeam.Infrastructure;

namespace RestProjectRouTeam.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
             
            builder.Services.AddScoped<IGitHubProjectPageRepository, GitHubPageRepository>();
            builder.Services.AddScoped<IGitHubService, GitHubService>();

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

            builder.Services.AddScoped<UsersService>();
            builder.Services.AddApiAuthentification(builder.Configuration);

            builder.Services.AddDbContext<MainDbContext>(
            options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MainDbContext)));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.AddMappedEndpoints();

            app.MapControllers();

            app.UseCors(options =>
            {
                options.WithHeaders().AllowAnyHeader();
                options.WithOrigins("http://localhost:3000");
                options.WithMethods().AllowAnyMethod();
            });

            app.Run();
        }
    }
}