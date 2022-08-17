using AutoMapper;
using Corzbank.Data;
using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Models.DTOs.Details;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Repository.Interfaces;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class CardService : ICardService
    {
        private readonly IGenericRepository<Card> _cardRepo;
        private readonly IWrappedVerificationService _verificationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CardService(IGenericRepository<Card> genericService, UserManager<User> userManager,
            IWrappedVerificationService verificationService, IMapper mapper)
        {
            _cardRepo = genericService;
            _userManager = userManager;
            _verificationService = verificationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CardDetails>> GetCards()
        {
            var cards = await _cardRepo.GetQueryable().ToListAsync();

            var result = _mapper.Map<IEnumerable<CardDetails>>(cards);

            return result;
        }

        public async Task<IEnumerable<CardDetails>> GetCardsForUser(Guid userId)
        {
            var cards = await _cardRepo.GetQueryable()
                .Include(u => u.User)
                .Where(c => c.User.Id == userId).ToListAsync();

            var result = _mapper.Map<IEnumerable<CardDetails>>(cards);

            return result;
        }

        public async Task<CardDetails> GetById(Guid id)
        {
            var card = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<CardDetails>(card);

            return result;
        }

        public async Task<CardDetails> CreateCard(CardDTO cardFromRequest)
        {
            var currentUser = await _userManager.FindByIdAsync(cardFromRequest.UserId.ToString());

            var userHasCard = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.CardType.Equals(cardFromRequest.CardType) && c.User.Id == currentUser.Id && c.IsActive);

            if (userHasCard != null)
                return null;

            var card = cardFromRequest.GenerateCard();
            var duplicateCard = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.CardNumber.Equals(card.CardNumber));

            while (duplicateCard != null)
            {
                card = cardFromRequest.GenerateCard();
                duplicateCard = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.CardNumber.Equals(card.CardNumber));
            }

            card.User = currentUser;

            await _cardRepo.Insert(card);

            var result = _mapper.Map<CardDetails>(card);

            return result;
        }

        public async Task<bool> CloseCard(Guid id)
        {
            var card = await _cardRepo.GetQueryable().Include(u => u.User).FirstOrDefaultAsync(c => c.Id == id);

            if (card == null)
                return false;

            var verificationModel = new VerificationDTO
            {
                Email = card.User.Email,
                VerificationType = VerificationType.CloseCard,
                CardId = id
            };

            await _verificationService.Verify(verificationModel);

            return true;
        }
    }
}