using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Models.DTOs.Details;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ITransferService
    {
        Task<IEnumerable<TransferDetails>> GetTransfers();

        Task<TransferDetails> GetTransferById(Guid id);

        Task<IEnumerable<TransferDetails>> GetTransfersForCard(Guid cardId);

        Task<TransferDetails> CreateTransfer(TransferDTO transferRequest);

        Task<bool> DeleteTransfer(Guid id);
    }
}
