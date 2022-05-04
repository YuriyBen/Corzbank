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
        Task<Card> GetCardById(int id);
        Task<Card> CreateCard(CardModel card);
        void DeleteCard(int id);
    }
}
