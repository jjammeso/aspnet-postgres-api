using RestApiTemplate.Models;

namespace RestApiTemplate.Repositories.Interface
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAsync(string token);
    }
}
