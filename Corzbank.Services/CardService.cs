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
        private readonly CorzbankDbContext _context;

        public CardService(CorzbankDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }
        public async Task<Card> GetCardById(int id)
        {
            return await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Card> CreateCard(CardModel card)
        {
            var result = card.GenerateCard();
            var duplicateCard = _context.Cards.AnyAsync(c => c.CardNumber == result.CardNumber);

            while (duplicateCard.Result)
            {
                result = card.GenerateCard();
                duplicateCard = _context.Cards.AnyAsync(c => c.CardNumber == result.CardNumber);
            }

            await _context.Cards.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteCard(int id)
        {
            var card = await GetCardById(id);

            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
