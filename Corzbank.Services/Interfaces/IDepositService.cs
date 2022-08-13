using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IDepositService
    {
        Task<IEnumerable<DepositDTO>> GetDeposits();

        Task<IEnumerable<DepositDTO>> GetDepositsForUser(Guid userId);

        Task<DepositDTO> GetDepositById(Guid id);

        Task<DepositDTO> OpenDeposit(DepositModel deposit);

        Task<bool> CloseDeposit(Guid id);
    }
}
