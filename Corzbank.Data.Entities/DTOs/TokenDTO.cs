using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}
