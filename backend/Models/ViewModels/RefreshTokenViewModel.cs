using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.ViewModels
{
    public class RefreshTokenViewModel
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public long Created { get; set; }
        public long Expires { get; set; }
    }
}
