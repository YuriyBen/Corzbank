using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Models.DTOs
{
    public class DepositDetailsDTO : DepositDTO
    {
        public Guid Id { get; set; }

        public double APY { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Profit { get; set; }

        public DepositStatus Status { get; set; }
    }
}
