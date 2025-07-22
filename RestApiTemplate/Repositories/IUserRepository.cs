using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetByIdAsync(string id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<User> CreateAsync(User user);
    }
}
