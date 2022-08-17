using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class CardDTO
    {
        public CardType CardType { get; set; }

        public PaymentSystem PaymentSystem { get; set; }

        public string SecretWord { get; set; }

        public Guid UserId { get; set; }
    }
}
