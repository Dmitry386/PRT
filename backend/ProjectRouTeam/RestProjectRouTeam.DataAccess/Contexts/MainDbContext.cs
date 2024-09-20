using Microsoft.EntityFrameworkCore;
using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.DataAccess.Contexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<GitHubSubject> ProjectPage { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}