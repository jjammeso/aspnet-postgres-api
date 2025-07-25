using RestApiTemplate.DTOs;
using RestApiTemplate.Models;
using RestApiTemplate.Repositories;
using RestApiTemplate.Services.Interfaces;

namespace RestApiTemplate.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDTO(u)).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new UserDTO(user);
        }
    }
}
