using Microsoft.EntityFrameworkCore;
using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.Core.Models;
using RestProjectRouTeam.DataAccess.Contexts;

namespace RestProjectRouTeam.DataAccess.Repositories
{
    public class UsersRepository : AbstractRepository<User, MainDbContext>, IUsersRepository
    {
        public UsersRepository(MainDbContext context) : base(context)
        {
        }

        public async Task<User> Get(string userName)
        {
            return await _context.Users
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.Name == userName);
        }
    }
}