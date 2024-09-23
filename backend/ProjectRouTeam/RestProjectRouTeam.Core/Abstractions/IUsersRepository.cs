using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<List<User>> Get();
        Task<User> Get(int id);
        Task<User> Get(string userName);
        Task<User> Create(User entity);
        Task<User> Update(User entity);
        Task<int> Delete(int id);
    }
}
