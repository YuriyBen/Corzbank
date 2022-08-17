using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Models.DTOs.Details;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDetails>> GetCards();

        Task<IEnumerable<CardDetails>> GetCardsForUser(Guid userId);

        Task<CardDetails> GetById(Guid id);

        Task<CardDetails> CreateCard(CardDTO cardFromRequest);

        Task<bool> CloseCard(Guid id);
    }
}
