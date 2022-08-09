using AutoMapper;
using Corzbank.Data;
using Corzbank.Data.Entities;
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

        public async Task<IEnumerable<Card>> GetCards()
        {
            var result = await _genericService.GetRange();

            return result;
        }

        public IEnumerable<Card> GetCardsForUser(Guid userId)
        {
            var result = _genericService.GetByCondition(x => x.User.Id == userId, x=>x.User);

            return result;
        }

        public async Task<Card> GetCardById(Guid id)
        {
            var result = await _genericService.Get(id);

            return result;
        }

        public async Task<Card> CreateCard(CardModel card)
        {
            var currentUser = await _userManager.FindByIdAsync(card.UserId.ToString());

            var userHasCard = await _genericService.FindByCondition(c => c.CardType.Equals(card.CardType) && c.User.Id == currentUser.Id && c.IsActive);

            if (userHasCard != null)
                return null;

            var result = card.GenerateCard();
            var duplicateCard = await _genericService.FindByCondition(c => c.CardNumber.Equals(result.CardNumber));

            while (duplicateCard != null)
            {
                result = card.GenerateCard();
                duplicateCard = await _genericService.FindByCondition(c => c.CardNumber.Equals(result.CardNumber));
            }

            result.User = currentUser;

            await _genericService.Insert(result);

            return result;
        }

        public async Task<Card> UpdateCard(Card cardForUpdate)
        {
            if (cardForUpdate != null)
            {
                await _genericService.Update(cardForUpdate);

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