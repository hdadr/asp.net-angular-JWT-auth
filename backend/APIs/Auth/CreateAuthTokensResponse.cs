using backend.Models.ViewModels;

namespace backend.APIs.Auth
{
    public class CreateAuthTokensResponse
    {
        public RefreshTokenViewModel RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
