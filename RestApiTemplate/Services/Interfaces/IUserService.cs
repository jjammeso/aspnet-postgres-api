using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(string id);
        Task<UserDTO> CreateAsync(CreateUserDTO createUserDTO);
    }
}
