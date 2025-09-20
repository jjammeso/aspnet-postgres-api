using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
        Task<UserDTO> RegisterAsync(UserRegisterDTO dto);
        Task<(string token, string refreshToken)> LoginAsync(UserLoginDTO dto, string ip);
        RefreshToken GenerateRefreshToken(Guid userId, string ip);
        Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string oldRefreshToken, string requestIp);
    }
}
