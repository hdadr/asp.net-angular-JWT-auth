using backend.APIs.Auth;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface ITokenService
    {
        Task<CreateAuthTokensResponse> CreateAuthTokensAsync(CreateAuthTokensRequest request);
        Task<string> RefreshAccessTokenAsync(RefreshAccessTokenRequest request);
        Task RevokeRefreshTokenAsync(string refreshTokenID);
    }
}
