using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Deposit: KeyEntity
    {
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public double APY { get; set; }

        public List<DepositCard> DepositCards { get; set; }
    }
}
