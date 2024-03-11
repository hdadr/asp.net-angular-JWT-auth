namespace backend.APIs.Auth
{
    public class CreateAuthTokensRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
