using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class ConfirmationDTO
    {
        public string Email { get; set; }

        public Guid? DepositId { get; set; }

        public Guid? CardId { get; set; }

        [Required(ErrorMessage = "VerificationCode is required")]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = "VerificationType is required")]
        public VerificationType VerificationType { get; set; }
    }
}
