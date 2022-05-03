using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Card: KeyEntity
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CvvCode { get; set; }
        public CardTypeEnum CardType { get; set; }
        public PaymentSystemEnum PaymentSystem { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public string SecretWord { get; set; }

        public User User { get; set; }
        public List<TransferCard> TransferCards { get; set; }
        public List<DepositCard> DepositCards { get; set; }
    }
}
