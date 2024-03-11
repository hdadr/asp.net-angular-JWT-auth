using System;

namespace backend.APIs.auth
{
    public class RefreshToken
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}
