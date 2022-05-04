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

            await _context.Cards.AddAsync(result);

            await _context.SaveChangesAsync();

            return result;
        }

        public void DeleteCard(int id)
        {
            _context.Cards.Remove(new Card() { Id = id });

            _context.SaveChangesAsync();
        }
    }
}
