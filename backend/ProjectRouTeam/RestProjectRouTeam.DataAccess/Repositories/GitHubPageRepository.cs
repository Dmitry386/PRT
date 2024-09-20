using Microsoft.EntityFrameworkCore;
using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.Core.Models;
using RestProjectRouTeam.DataAccess.Contexts;

namespace RestProjectRouTeam.DataAccess.Repositories
{
    public class GitHubPageRepository : AbstractRepository<GitHubSubject, MainDbContext>, IGitHubProjectPageRepository
    {
        public GitHubPageRepository(MainDbContext context) : base(context)
        {
        }

        public async Task<List<GitHubSubject>> Search(string subject)
        {
            return await _context.ProjectPage
                .Where(x => x.SearchSubject == subject)
                .ToListAsync();
        }
    }
}