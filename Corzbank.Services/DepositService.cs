using AutoMapper;
using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
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

        public async Task<IEnumerable<DepositDetailsDTO>> GetDeposits()
        {
            var deposits = await _depositRepo.GetQueryable().ToListAsync();

            var result = _mapper.Map<IEnumerable<DepositDetailsDTO>>(deposits);

            return result;
        }

        public async Task<DepositDetailsDTO> GetDepositById(Guid id)
        {
            var deposit = await _depositRepo.GetQueryable().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<DepositDetailsDTO>(deposit);

            return result;
        }

        public async Task<IEnumerable<DepositDetailsDTO>> GetDepositsForUser(Guid userId)
        {
            var deposits = await _depositRepo
                .GetQueryable()
                .Include(c => c.Card).ThenInclude(u => u.User)
                .Where(u => u.Card.User.Id == userId && u.Status == DepositStatus.Opened)
                .ToListAsync();

            var mappedDeposits = _mapper.Map<IEnumerable<DepositDetailsDTO>>(deposits);

            return mappedDeposits;
        }

        public async Task<DepositDetailsDTO> OpenDeposit(DepositDTO deposit)
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

            var result = _mapper.Map<DepositDetailsDTO>(generatedDeposit);

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
                var verificationModel = new VerificationDTO
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
