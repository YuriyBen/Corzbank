using Corzbank.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class User: IdentityUser<Guid>
    {
        public string Firstname { get; set; }
      
        public string Lastname { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public List<Card> Cards { get; set; }
    }
}
