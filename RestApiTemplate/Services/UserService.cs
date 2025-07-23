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
        public async Task<UserDTO> CreateAsync(CreateUserDTO createUserDTO)
        {
            var user = new User
            {
                Name = createUserDTO.Name,
                Id = Guid.NewGuid().ToString(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password)
            };
           
            var returnedUser = await _userRepository.CreateAsync(user);

            return new UserDTO(returnedUser);
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
