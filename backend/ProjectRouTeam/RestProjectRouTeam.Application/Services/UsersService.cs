using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.Core.Models;
using RestProjectRouTeam.Infrastructure;

namespace RestProjectRouTeam.Application.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _repository;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IPasswordHasher passwordHasher, IUsersRepository repository, IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _repository = repository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string userName, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User()
            {
                Name = userName,
                PasswordHash = hashedPassword
            };

            await _repository.Create(user);
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _repository.Get(userName);

            if (user == null) return "User not found";

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                return string.Empty;
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}