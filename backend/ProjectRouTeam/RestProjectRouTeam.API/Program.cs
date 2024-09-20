using Microsoft.EntityFrameworkCore;
using RestProjectRouTeam.Application.Services;
using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.DataAccess.Contexts;
using RestProjectRouTeam.DataAccess.Repositories;

namespace RestProjectRouTeam.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IGitHubProjectPageRepository, GitHubPageRepository>();
            builder.Services.AddScoped<IGitHubService, GitHubService>();

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

            app.UseAuthorization();

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
