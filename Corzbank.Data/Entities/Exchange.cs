using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities
{
    public class Exchange: KeyEntity
    {
        public Currency ExchangeCurrency { get; set; }
      
        public Currency BaseCurrency { get; set; }
        
        public decimal Buy { get; set; }
        
        public decimal Sell { get; set; }
    }
}
