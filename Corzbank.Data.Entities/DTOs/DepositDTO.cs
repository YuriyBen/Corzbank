using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class DepositDTO
    {
        public decimal Amount { get; set; }

        public int Duration { get; set; }

        public Guid CardId { get; set; }
    }
}
