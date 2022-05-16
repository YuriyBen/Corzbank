using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class VerificationModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "VerificationType is required")]
        public VerificationType VerificationType { get; set; }

        public Guid CardId { get; set; }
    }
}
