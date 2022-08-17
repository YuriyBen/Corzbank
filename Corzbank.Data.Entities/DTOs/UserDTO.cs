using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class UserDTO
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
