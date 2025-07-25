using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
        Task<UserDTO> RegisterAsync(UserRegisterDTO dto);
        Task<string> LoginAsync(UserLoginDTO dto);
    }
}
