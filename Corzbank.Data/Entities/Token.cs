using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Token : KeyEntity
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}
