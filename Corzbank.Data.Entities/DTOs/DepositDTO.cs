using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Data.Entities.DTOs
{
    public class DepositDTO
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public int Duration { get; set; }

        public double APY { get; set; }

        public DateTime DepositOpened { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Profit { get; set; }

        public DepositStatus IsActive { get; set; }

        public Guid CardId { get; set; }
    }
}
