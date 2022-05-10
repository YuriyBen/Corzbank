using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.Models
{
    public class DepositModel
    {
        public decimal Amount { get; set; }

        public int Duration { get; set; }
    }
}
