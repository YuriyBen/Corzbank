using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Transfer: KeyEntity
    {
        public TransferTypeEnum TransferType { get; set; }
        public string toPhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public bool IsSuccessful { get; set; }

        public List<TransferCard> TransferCards { get; set; }
    }
}
