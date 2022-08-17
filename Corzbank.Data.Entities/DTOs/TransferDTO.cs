using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class TransferDTO
    {
        public TransferType TransferType { get; set; }

        public string ReceiverPhoneNumber { get; set; }

        public decimal Amount { get; set; }

        public string Note { get; set; }

        public Guid SenderCardId { get; set; }

        public string ReceiverCardNumber { get; set; }
    }
}
