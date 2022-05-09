using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ITransferService
    {
        Task<IEnumerable<Transfer>> GetTransfers();

        Task<Transfer> GetTransferById(Guid id);

        Task<Transfer> CreateTransfer(TransferModel card);

        Task<bool> DeleteTransfer (Guid id);
    }
}
