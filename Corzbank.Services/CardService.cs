using AutoMapper;
using Corzbank.Data;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class CardService : ICardService
    {
        private readonly GenericService<Card> _genericService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IWrappedVerificationService _verificationService;
        private readonly IMapper _mapper;

        public CardService(GenericService<Card> genericService, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager, IWrappedVerificationService verificationService, IMapper mapper)
        {
            _genericService = genericService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _verificationService = verificationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CardDTO>> GetCards()
        {
            var cards = await _genericService.GetRange();

            var result = _mapper.Map<IEnumerable<CardDTO>>(cards);

            return result;
        }

        public IEnumerable<CardDTO> GetCardsForUser(Guid userId)
        {
            var cards = _genericService.GetByCondition(x => x.User.Id == userId, x => x.User);

            var result = _mapper.Map<IEnumerable<CardDTO>>(cards);

            return result;
        }

        public async Task<CardDTO> GetCardById(Guid id)
        {
            var card = await _genericService.Get(id);

            var result = _mapper.Map<CardDTO>(card);

            return result;
        }

        public async Task<CardDTO> CreateCard(CardModel cardFromRequest)
        {
            var currentUser = await _userManager.FindByIdAsync(cardFromRequest.UserId.ToString());

            var userHasCard = await _genericService.FindByCondition(c => c.CardType.Equals(cardFromRequest.CardType) && c.User.Id == currentUser.Id && c.IsActive);

            if (userHasCard != null)
                return null;

            var card = cardFromRequest.GenerateCard();
            var duplicateCard = await _genericService.FindByCondition(c => c.CardNumber.Equals(card.CardNumber));

            while (duplicateCard != null)
            {
                card = cardFromRequest.GenerateCard();
                duplicateCard = await _genericService.FindByCondition(c => c.CardNumber.Equals(card.CardNumber));
            }

            card.User = currentUser;

            await _genericService.Insert(card);

            var result = _mapper.Map<CardDTO>(card);

            return result;
        }

        public async Task<CardDTO> UpdateCard(CardDTO cardForUpdate)
        {
            if (cardForUpdate != null)
            {
                var mappedCard = _mapper.Map<Card>(cardForUpdate);

                _genericService.DetachLocal(x => x.Id == mappedCard.Id);

                await _genericService.Update(mappedCard);

                return cardForUpdate;
            }

            return null;
        }

        public async Task<bool> CloseCard(Guid id)
        {
            var card = GetCardById(id);

            var currentUserEmail = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (card != null)
            {
                var verificationModel = new VerificationModel
                {
                    Email = currentUserEmail,
                    VerificationType = VerificationType.CloseCard,
                    CardId = id
                };

                await _verificationService.Verify(verificationModel);

                return true;
            }
            return false;
        }
    }
}