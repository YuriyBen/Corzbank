using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class DepositCard: KeyEntity
    {
        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
