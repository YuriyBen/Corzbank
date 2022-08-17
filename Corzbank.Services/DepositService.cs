using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Helpers.Exceptions;
using Corzbank.Repository.Interfaces;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class DepositService : IDepositService
    {
        private readonly IGenericRepository<Deposit> _depositRepo;
        private readonly IGenericRepository<Card> _cardRepo;
        private readonly IMapper _mapper;
        private readonly IWrappedVerificationService _verificationService;

        public DepositService(IGenericRepository<Deposit> depositRepo, IMapper mapper,
            IWrappedVerificationService verificationService, IGenericRepository<Card> cardRepo)
        {
            _depositRepo = depositRepo;
            _cardRepo = cardRepo;
            _mapper = mapper;
            _verificationService = verificationService;
        }

        public async Task<IEnumerable<DepositDTO>> GetDeposits()
        {
            var deposits = await _depositRepo.GetQueryable().ToListAsync();

            var result = _mapper.Map<IEnumerable<DepositDTO>>(deposits);

            return result;
        }

        public async Task<DepositDTO> GetDepositById(Guid id)
        {
            var deposit = await _depositRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<DepositDTO>(deposit);

            return result;
        }

        public async Task<IEnumerable<DepositDTO>> GetDepositsForUser(Guid userId)
        {
            var deposits = await _depositRepo
                .GetQueryable()
                .Include(c => c.Card).ThenInclude(u => u.User)
                .Where(u => u.Card.User.Id == userId && u.Status == DepositStatus.Opened)
                .ToListAsync();

            var mappedDeposits = _mapper.Map<IEnumerable<DepositDTO>>(deposits);

            return mappedDeposits;
        }

        public async Task<DepositDTO> OpenDeposit(DepositModel deposit)
        {
            var cardForDeposit = await _cardRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == deposit.CardId);

            if (cardForDeposit.Balance < deposit.Amount)
                throw new ForbiddenException();
            else
            {
                cardForDeposit.Balance -= deposit.Amount;
                await _cardRepo.Update(cardForDeposit);
            }

            var mappedDeposit = _mapper.Map<Deposit>(deposit);
            mappedDeposit.Card = cardForDeposit;

            var generatedDeposit = mappedDeposit.GenerateDeposit();

            await _depositRepo.Insert(generatedDeposit);

            var result = _mapper.Map<DepositDTO>(generatedDeposit);

            return result;
        }

        public async Task<bool> CloseDeposit(Guid id)
        {
            var deposit = await _depositRepo
                .GetQueryable()
                .Include(c => c.Card).ThenInclude(u => u.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (deposit != null && deposit.Status == DepositStatus.Opened)
            {
                var verificationModel = new VerificationModel
                {
                    Email = deposit.Card.User.Email,
                    VerificationType = VerificationType.CloseDeposit,
                    DepositId = id
                };

                await _verificationService.Verify(verificationModel);

                return true;
            }
            return false;
        }
    }
}
