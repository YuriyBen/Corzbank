using AutoMapper;
using Corzbank.Data.Entities;
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
        public async Task<IEnumerable<Transfer>> GetTransfers()
        {
            var result = await _genericService.GetRange();

            return result;
        }

        public async Task<Transfer> GetTransferById(Guid id)
        {
            var result = await _genericService.Get(id);

            return result;
        }

        public async Task<Transfer> CreateTransfer(TransferModel transferRequest)
        {
            if (transferRequest.TransferType == TransferType.Card)
            {
                transferRequest.ReceiverPhoneNumber = null;

                if (transferRequest.ReceiverCardId == null)
                    return null;
            }
            else
            {
                transferRequest.ReceiverCardId = null;

                if (transferRequest.ReceiverPhoneNumber == null)
                    return null;
            }

            var currentUserEmail = _httpContextAccessor.HttpContext.User.Identity.Name;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            var senderCard = await _cardService.GetCardById(transferRequest.SenderCardId);

            if (senderCard.User == currentUser && senderCard.Balance > transferRequest.Amount)
            {
                senderCard.Balance -= transferRequest.Amount;
                await _cardService.UpdateCard(senderCard);

                if (transferRequest.ReceiverCardId != null)
                {
                    var receiverCard = await _cardService.GetCardById(transferRequest.ReceiverCardId ?? Guid.Empty);

                    if (receiverCard != null && senderCard != receiverCard) 
                    {
                        receiverCard.Balance += transferRequest.Amount;
                        await _cardService.UpdateCard(receiverCard);
                    }

                    return null;
                }

                var transfer = _mapper.Map<Transfer>(transferRequest);

                transfer.IsSuccessful = true;

                await _genericService.Insert(transfer);

                return transfer;
            }

            return null;
        }

        public async Task<bool> DeleteTransfer(Guid id)
        {
            var transfer = await GetTransferById(id);

            if (transfer != null)
            {
                await _genericService.Remove(transfer);

                return true;
            }
            return false;
        }
    }
}
