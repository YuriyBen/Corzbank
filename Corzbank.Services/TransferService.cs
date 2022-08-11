using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Repository.Interfaces;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class TransferService : ITransferService
    {
        private readonly IGenericRepository<Transfer> _transferRepo;
        private readonly IGenericRepository<Card> _cardRepo;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<Transfer> transferRepo, IMapper mapper, IGenericRepository<Card> cardRepo)
        {
            _transferRepo = transferRepo;
            _mapper = mapper;
            _cardRepo = cardRepo;
        }

        public async Task<IEnumerable<TransferDTO>> GetTransfers()
        {
            var transfers = await _transferRepo.GetQueryable().ToListAsync();

            var result = _mapper.Map<IEnumerable<TransferDTO>>(transfers);

            return result;
        }

        public async Task<TransferDTO> GetTransferById(Guid id)
        {
            var transfer = await _transferRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<TransferDTO>(transfer);

            return result;
        }

        public async Task<IEnumerable<TransferDTO>> GetTransfersForCard(Guid cardId)
        {
            var transfers = await _transferRepo.GetQueryable().Where(c => c.SenderCardId == cardId || c.ReceiverCardId == cardId).ToListAsync();

            var result = _mapper.Map<IEnumerable<TransferDTO>>(transfers);

            return result;
        }

        public async Task<TransferDTO> CreateTransfer(TransferModel transferRequest)
        {
            if (transferRequest.TransferType == TransferType.Card)
            {
                transferRequest.ReceiverPhoneNumber = null;

                if (transferRequest.ReceiverCardNumber == null)
                    return null;
            }
            else
            {
                transferRequest.ReceiverCardNumber = null;

                if (transferRequest.ReceiverPhoneNumber == null)
                    return null;
            }

            var senderCard = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == transferRequest.SenderCardId);

            if (senderCard.Balance <= transferRequest.Amount)
                return null;

            senderCard.Balance -= transferRequest.Amount;

            if (transferRequest.ReceiverCardNumber != null)
            {
                var receiverCard = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.CardNumber == transferRequest.ReceiverCardNumber) ?? null;

                if (receiverCard == null || senderCard.Id == receiverCard.Id)
                    return null;

                receiverCard.Balance += transferRequest.Amount;

                await _cardRepo.Update(receiverCard);
            }

            await _cardRepo.Update(senderCard);

            var transfer = _mapper.Map<Transfer>(transferRequest);

            transfer.IsSuccessful = true;

            await _transferRepo.Insert(transfer);

            var result = _mapper.Map<TransferDTO>(transfer);

            return result;
        }

        public async Task<bool> DeleteTransfer(Guid id)
        {
            var transfer = await _transferRepo.GetQueryable().FirstOrDefaultAsync(t => t.Id == id);

            if (transfer != null)
                return false;

            var mappedCard = _mapper.Map<Transfer>(transfer);

            await _transferRepo.Remove(mappedCard);

            return true;
        }
    }
}
