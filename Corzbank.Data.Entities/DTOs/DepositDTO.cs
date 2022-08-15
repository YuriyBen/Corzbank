using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.DTOs
{
    public class DepositDTO: DepositModel
    {
        public Guid Id { get; set; }

        public double APY { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Profit { get; set; }

        public DepositStatus Status { get; set; }
    }
}
