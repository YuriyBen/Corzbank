using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IDepositService
    {
        Task<IEnumerable<DepositDetailsDTO>> GetDeposits();

        Task<IEnumerable<DepositDetailsDTO>> GetDepositsForUser(Guid userId);

        Task<DepositDetailsDTO> GetDepositById(Guid id);

        Task<DepositDetailsDTO> OpenDeposit(DepositDTO deposit);

        Task<bool> CloseDeposit(Guid id);
    }
}
