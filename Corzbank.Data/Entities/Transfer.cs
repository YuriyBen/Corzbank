using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Transfer: KeyEntity
    {
        [Required]
        public TransferType TransferType { get; set; }
       
        public string ToPhoneNumber { get; set; }

        [Required]
        public decimal Amount { get; set; } = 0;
        
        public DateTime Date { get; set; }
        
        public string Note { get; set; }
        
        public bool IsSuccessful { get; set; }


        public List<TransferCard> TransferCards { get; set; }
    }
}
