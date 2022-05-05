using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers
{
    public static class CardGenerator
    {
        public static Card GenerateCard(this CardModel card)
        {
            Card card1 = new Card
            {
                CardNumber = GenerateCardNumber(card.PaymentSystem),
                ExpirationDate = GenerateExpirationDate(DateTime.Now.AddYears(4)),
                CvvCode = GenerateCvvCode(),
                PaymentSystem = card.PaymentSystem,
                CardType = card.CardType,
                SecretWord = card.SecretWord
            };

            return card1;
        }

        private static string GenerateExpirationDate(DateTime date)
        {
            var expirationDate = date.ToString("MM/yy");
            return expirationDate;
        }

        private static string GenerateCardNumber(PaymentSystem paymentSystem)
        {
            var card = paymentSystem == PaymentSystem.Visa ? "4441" : "5168";
            Random rnd = new Random();

            for (int i = 0; i < 12; i++)
            {
                card += rnd.Next(0, 10).ToString();
            }

            return card;
        }

        private static ushort GenerateCvvCode()
        {
            Random rnd = new Random();
            var cvv = "";

            for (int i = 0; i < 3; i++)
            {
                cvv += rnd.Next(0, 10).ToString();
            }

           return ushort.Parse(cvv);
        }
    }
}
