using Corzbank.Data.Entities;
using Corzbank.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers
{
    public static class ConfigureDeposit
    {
        public static Deposit GenerateDeposit(this Deposit deposit)
        {
            deposit.APY = (int)deposit.Duration;
            var yearProfit = ((double)deposit.Amount * (deposit.APY / 100))/12;
            var sumProfit = yearProfit * (int)deposit.Duration;

            deposit.Profit = (decimal)sumProfit;
            deposit.OpenDate = DateTime.Now;
            deposit.EndDate = deposit.OpenDate.AddMonths((int)deposit.Duration);
            deposit.Status = DepositStatus.Opened;

            return deposit;
        }
    }
}
