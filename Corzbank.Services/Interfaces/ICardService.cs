using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDetailsDTO>> GetCards();

        Task<IEnumerable<CardDetailsDTO>> GetCardsForUser(Guid userId);

        Task<CardDetailsDTO> GetById(Guid id);

        Task<CardDetailsDTO> CreateCard(CardDTO cardFromRequest);

        Task<bool> CloseCard(Guid id);
    }
}
