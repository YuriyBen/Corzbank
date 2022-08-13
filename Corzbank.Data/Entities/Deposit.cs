using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Deposit : KeyEntity
    {
        public decimal Amount { get; set; }

        public int Duration { get; set; }

        public double APY { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Profit { get; set; }

        public DepositStatus Status { get; set; }

        public Card Card { get; set; }

        public List<DepositCard> DepositCards { get; set; }
    }
}
