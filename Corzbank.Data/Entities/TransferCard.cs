using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class TransferCard: KeyEntity
    {
        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }

        public int SenderCardId { get; set; }
        public int ReceiverCardId { get; set; }
        public Card Card { get; set; }
    }
}
