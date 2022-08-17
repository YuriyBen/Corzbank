using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models
{
    public class DepositCard : KeyEntity
    {
        public Guid DepositId { get; set; }

        public Deposit Deposit { get; set; }

        public Guid CardId { get; set; }

        public Card Card { get; set; }
    }
}
