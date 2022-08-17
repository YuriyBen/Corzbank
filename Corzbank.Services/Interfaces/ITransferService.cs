using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ITransferService
    {
        Task<IEnumerable<TransferDetailsDTO>> GetTransfers();

        Task<TransferDetailsDTO> GetTransferById(Guid id);

        Task<IEnumerable<TransferDetailsDTO>> GetTransfersForCard(Guid cardId);

        Task<TransferDetailsDTO> CreateTransfer(TransferDTO transferRequest);

        Task<bool> DeleteTransfer(Guid id);
    }
}
