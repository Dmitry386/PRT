using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}