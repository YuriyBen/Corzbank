using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Models
{
    public class Deposit : KeyEntity
    {
        public decimal Amount { get; set; }

        public int Duration { get; set; }

        public double APY { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Profit { get; set; }

        public DepositStatus Status { get; set; }

        public Card Card { get; set; }

        public List<DepositCard> DepositCards { get; set; }
    }
}
