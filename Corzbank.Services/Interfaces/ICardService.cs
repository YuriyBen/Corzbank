using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetCards();

        IEnumerable<Card> GetCardsForUser(Guid userId);

        Task<Card> GetCardById(Guid id);

        Task<Card> CreateCard(CardModel card);

        Task<Card> UpdateCard(Card cardForUpdate);

        Task<bool> CloseCard(Guid id);
    }
}
