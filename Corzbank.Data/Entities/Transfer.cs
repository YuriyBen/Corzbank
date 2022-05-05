using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Transfer: KeyEntity
    {
        public TransferType TransferType { get; set; }
       
        [Range(10, 13, ErrorMessage = "ReceiverPhoneNumber should be in range 10 - 13")]
        public string ReceiverPhoneNumber { get; set; }

        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        [MaxLength(50, ErrorMessage = "Note length can't be more than 50 characters")]
        public string Note { get; set; }
        
        public bool IsSuccessful { get; set; }

        public List<TransferCard> TransferCards { get; set; }
    }
}
