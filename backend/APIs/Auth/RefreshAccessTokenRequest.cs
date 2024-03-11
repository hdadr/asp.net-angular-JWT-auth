using backend.Models.ViewModels;

namespace backend.APIs.Auth
{
    public class RefreshAccessTokenRequest
    {
        public RefreshTokenViewModel RefreshToken { get; set; }
        public string AccessToken { get; set; }

    }
}
