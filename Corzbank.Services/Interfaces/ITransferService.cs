using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ITransferService
    {
        Task<IEnumerable<TransferDTO>> GetTransfers();

        Task<TransferDTO> GetTransferById(Guid id);

        Task<IEnumerable<TransferDTO>> GetTransfersForCard(Guid cardId);

        Task<TransferDTO> CreateTransfer(TransferModel transferRequest);

        Task<bool> DeleteTransfer(Guid id);
    }
}
