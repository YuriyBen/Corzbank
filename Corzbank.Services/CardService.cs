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

        public CardService(GenericService<Card> genericService, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager, IWrappedVerificationService verificationService)
        {
            _genericService = genericService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _verificationService = verificationService;
        }

        public async Task<IEnumerable<Card>> GetCards()
        {
            var result = await _genericService.GetRange();

            return result;
        }

        public async Task<Card> GetCardById(Guid id)
        {
            var result = await _genericService.Get(id);

            return result;
        }

        public async Task<Card> CreateCard(CardModel card)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext.User.Identity.Name;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if (await _genericService.FindByCondition(c => c.CardType.Equals(card.CardType) && c.User.Id == currentUser.Id && c.IsActive) != null)
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

        public async Task<bool> CloseCard(Guid id)
        {
            var card = await GetCardById(id);

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