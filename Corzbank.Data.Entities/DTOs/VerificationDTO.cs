using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class VerificationDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "VerificationType is required")]
        public VerificationType VerificationType { get; set; }

        public Guid CardId { get; set; }

        public Guid DepositId { get; set; }
    }
}
