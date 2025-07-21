using RestApiTemplate.DTOs;
using RestApiTemplate.Models;
using RestApiTemplate.Services.Interfaces;

namespace RestApiTemplate.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new()
        {
            new User { Id = "1", Name = "alice", PasswordHash = "hashed_pw_1" },
            new User { Id = "2", Name = "bob", PasswordHash = "hashed_pw_2" },
            new User { Id = "3", Name = "carol", PasswordHash = "hashed_pw_3" }
        };
        public Task CreateAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<List<UserDTO>> GetAllAsync()
        {
            var userDTOs = _users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name
            }).ToList();

            return Task.FromResult(userDTOs);
        }

        public Task<UserDTO?> GetByIdAsync(string id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return Task.FromResult<UserDTO?>(null);

            var dto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name
            };

            return Task.FromResult<UserDTO?>(dto);
        }
    }
}
