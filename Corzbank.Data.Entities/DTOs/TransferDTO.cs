using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.DTOs
{
    public class TransferDTO 
    {
        public Guid Id { get; set; }

        public TransferType TransferType { get; set; }

        public string ReceiverPhoneNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public bool IsSuccessful { get; set; }

        public Guid SenderCardId { get; set; }

        public Guid? ReceiverCardId { get; set; }
    }
}
