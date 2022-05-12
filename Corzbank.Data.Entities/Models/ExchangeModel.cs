using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class ExchangeModel
    {
        public Currency ExchangeCurrency { get; set; }

        public Currency BaseCurrency { get; set; }

        public decimal Buy { get; set; }

        public decimal Sell { get; set; }
    }
}
