using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IDepositService
    {
        Task<IEnumerable<Deposit>> GetDeposits();

        Task<Deposit> GetDepositById(Guid id);

        Task<Deposit> OpenDeposit(DepositModel deposit);

        Task<bool> CloseDeposit(Guid id);
    }
}
