using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Models.DTOs.Details;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IDepositService
    {
        Task<IEnumerable<DepositDetails>> GetDeposits();

        Task<IEnumerable<DepositDetails>> GetDepositsForUser(Guid userId);

        Task<DepositDetails> GetDepositById(Guid id);

        Task<DepositDetails> OpenDeposit(DepositDTO deposit);

        Task<bool> CloseDeposit(Guid id);
    }
}
