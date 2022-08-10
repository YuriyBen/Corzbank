
using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.DTOs
{
    public class CardDTO 
    {
        public Guid Id { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public ushort CvvCode { get; set; }

        public CardType CardType { get; set; }

        public PaymentSystem PaymentSystem { get; set; }

        public decimal Balance { get; set; }

        public bool IsActive { get; set; }

        public string SecretWord { get; set; }

        public User User { get; set; }
    }
}
