using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Database;
using RestApiTemplate.Models;
using RestApiTemplate.Repositories.Interface;

namespace RestApiTemplate.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task RevokeAsync(string token)
        {
            var tokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (tokenEntity != null)
            {
                tokenEntity.IsRevoked = true;
                tokenEntity.RevokedAt = DateTime.UtcNow;

                _context.RefreshTokens.Update(tokenEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
