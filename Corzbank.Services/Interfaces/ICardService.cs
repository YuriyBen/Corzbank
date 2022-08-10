using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDTO>> GetCards();

        IEnumerable<CardDTO> GetCardsForUser(Guid userId);

        Task<CardDTO> GetCardById(Guid id);

        Task<CardDTO> CreateCard(CardModel card);

        Task<CardDTO> UpdateCard(CardDTO cardForUpdate);

        Task<bool> CloseCard(Guid id);
    }
}
