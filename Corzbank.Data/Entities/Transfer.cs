using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Transfer: KeyEntity
    {
        [Required(ErrorMessage = "TransferType is required")]
        public TransferType TransferType { get; set; }
       
        public string ReceiverPhoneNumber { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Note { get; set; }
        
        public bool IsSuccessful { get; set; }

        public List<TransferCard> TransferCards { get; set; }
    }
}
