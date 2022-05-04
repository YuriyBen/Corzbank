using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class CardModel
    {
        public CardType CardType { get; set; }

        public PaymentSystem PaymentSystem { get; set; }

        public string SecretWord { get; set; }
    }
}
