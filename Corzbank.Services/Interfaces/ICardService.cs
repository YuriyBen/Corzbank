using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDTO>> GetCards();

        Task<IEnumerable<CardDTO>> GetCardsForUser(Guid userId);

        Task<CardDTO> GetById(Guid id);

        Task<CardDTO> CreateCard(CardModel cardFromRequest);

        Task<bool> CloseCard(Guid id);
    }
}
