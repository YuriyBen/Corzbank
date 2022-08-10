using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Card : KeyEntity
    {
        [Range(16, 16, ErrorMessage = "Card length should be 16")]
        public string CardNumber { get; set; }

        [Range(4, 4, ErrorMessage = "ExpirationDate length should be 4")]
        public string ExpirationDate { get; set; }

        [Range(3, 3, ErrorMessage = "CvvCode length should be 3")]
        public ushort CvvCode { get; set; }

        public CardType CardType { get; set; }

        public PaymentSystem PaymentSystem { get; set; }

        public decimal Balance { get; set; }

        public bool IsActive { get; set; }

        [Range(5, 20, ErrorMessage = "SecretWord length should be from 5 to 20 characters")]
        public string SecretWord { get; set; }

        public User User { get; set; }

        public List<DepositCard> DepositCards { get; set; }

        [InverseProperty("SenderCard")]
        public List<TransferCard> SendTransfers { get; set; }

        [InverseProperty("ReceiverCard")]
        public List<TransferCard> ReceiveTransfers { get; set; }
    }
}
