using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}
