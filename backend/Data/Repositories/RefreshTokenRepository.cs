using backend.APIs.auth;
using System;
using System.Threading.Tasks;

namespace backend.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(Guid id)
        {
            return await _context.RefreshTokens.FindAsync(id);
        }

        public async Task InsertRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
        }

        public async Task DeleteRefreshTokenAsync(Guid id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);
            if (refreshToken != null)
                _context.RefreshTokens.Remove(refreshToken);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
