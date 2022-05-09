using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class TransferCard: KeyEntity
    {
        public Guid TransferId { get; set; }
      
        public Transfer Transfer { get; set; }

        public Guid SenderCardId { get; set; }

        public Card SenderCard { get; set; }
        
        public Guid? ReceiverCardId { get; set; }
        
        public Card ReceiverCard { get; set; }
    }
}
