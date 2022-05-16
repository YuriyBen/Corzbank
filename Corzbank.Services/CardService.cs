using Corzbank.Data;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
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

        public CardService(GenericService<Card> genericService)
        {
            _genericService = genericService;
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
            var result = card.GenerateCard();
            var duplicateCard = await _genericService.FindByCondition(c => c.CardNumber.Equals(result.CardNumber));

            while (duplicateCard != null)
            {
                result = card.GenerateCard();
                duplicateCard = await _genericService.FindByCondition(c => c.CardNumber.Equals(result.CardNumber));
            }

            await _genericService.Insert(result);

            return result;
        }

        public async Task<bool> DeleteCard(Guid id)
        {
            var card = await GetCardById(id);

            if (card != null)
            {
                await _genericService.Remove(card);

                return true;
            }
            return false;
        }
    }
}
