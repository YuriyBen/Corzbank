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
            return await _genericService.GetRange();
        }
        public async Task<Card> GetCardById(int id)
        {
            return await _genericService.Get(id);
        }

        public async Task<Card> CreateCard(CardModel card)
        {
            var result = card.GenerateCard();
            var duplicateCard = _genericService.CheckByCondition(c => c.CardNumber.Equals(result.CardNumber));

            while (duplicateCard)
            {
                result = card.GenerateCard();
                duplicateCard = _genericService.CheckByCondition(c => c.CardNumber.Equals(result.CardNumber));
            }

            await _genericService.Insert(result);

            return result;
        }

        public async Task<bool> DeleteCard(int id)
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
