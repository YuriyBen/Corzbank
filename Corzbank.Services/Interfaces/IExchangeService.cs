using Corzbank.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface IExchangeService
    {
        Task<IEnumerable<Exchange>> GetValues();

        Task<bool> CreateExchage();

        Task<bool> UpdateExchage();
    }
}
