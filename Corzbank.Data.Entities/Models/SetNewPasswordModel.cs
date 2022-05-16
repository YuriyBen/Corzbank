using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class SetNewPasswordModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
