using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class TransferService : ITransferService
    {
        private readonly GenericService<Transfer> _genericService;
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public TransferService(GenericService<Transfer> genericService, IMapper mapper, ICardService cardService,
            IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _cardService = cardService;
            _genericService = genericService;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TransferDTO>> GetTransfers()
        {
            var transfers = await _genericService.GetRange();

            var result = _mapper.Map<IEnumerable<TransferDTO>>(transfers);

            return result;
        }

        public async Task<TransferDTO> GetTransferById(Guid id)
        {
            var transfer = await _genericService.Get(id);

            var result = _mapper.Map<TransferDTO>(transfer);

            return result;
        }

        public IEnumerable<TransferDTO> GetTransfersForCard(Guid cardId)
        {
            var transfers = _genericService.GetByCondition(x => x.SenderCardId == cardId || x.ReceiverCardId == cardId);

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

            var senderCard = await _cardService.GetCardByExpression(x => x.Id == transferRequest.SenderCardId);

            if (senderCard.Balance <= transferRequest.Amount)
                return null;

            senderCard.Balance -= transferRequest.Amount;

            if (transferRequest.ReceiverCardNumber != null)
            {
                var receiverCard = await _cardService.GetCardByExpression(x => x.CardNumber == transferRequest.ReceiverCardNumber) ?? null;

                if (receiverCard == null || senderCard.Id == receiverCard.Id)
                    return null;

                receiverCard.Balance += transferRequest.Amount;

                await _cardService.UpdateCard(receiverCard);
            }

            await _cardService.UpdateCard(senderCard);

            var transfer = _mapper.Map<Transfer>(transferRequest);

            transfer.IsSuccessful = true;

            await _genericService.Insert(transfer);

            var result = _mapper.Map<TransferDTO>(transfer);

            return result;
        }

        public async Task<bool> DeleteTransfer(Guid id)
        {
            var transfer = await GetTransferById(id);

            if (transfer != null)
                return false;

            var mappedCard = _mapper.Map<Transfer>(transfer);

            await _genericService.Remove(mappedCard);

            return true;
        }
    }
}
