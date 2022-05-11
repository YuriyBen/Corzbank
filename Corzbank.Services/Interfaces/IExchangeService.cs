using Corzbank.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IExchangeService
    {
        Task<IEnumerable<Exchange>> GetValues();

        Task<Exchange> GetValueById(Guid id);

        Task<bool> CreateExchage();

        Task<bool> UpdateExchage();

        Task<bool> DeleteExchange();
    }
}
