using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class ExchangeModelForParsing
    {
        public string ccy { get; set; }

        public string base_ccy { get; set; }

        public string buy { get; set; }

        public string sale { get; set; }
    }
}
