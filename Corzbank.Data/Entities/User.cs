using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class User: IdentityUser
    {
        public string Firstname { get; set; }
      
        public string Lastname { get; set; }

        public List<Card> Cards { get; set; }
    }
}
