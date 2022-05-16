using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Verification: KeyEntity
    {
        public string VerificationCode { get; set; }
       
        public DateTime ValidTo { get; set; }
       
        public bool IsVerified { get; set; }
       
        public VerificationType VerificationType { get; set; }

        public User User { get; set; }
    }
}
