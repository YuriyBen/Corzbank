using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class UserForLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password  is required")]
        public string Password { get; set; }
    }
}
