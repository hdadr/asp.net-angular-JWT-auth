using backend.APIs.auth;
using System;
using System.Threading.Tasks;

namespace backend.Data.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshTokenAsync(Guid id);
        Task InsertRefreshTokenAsync(RefreshToken token);
        Task DeleteRefreshTokenAsync(Guid id);
        Task SaveAsync();
    }
}
