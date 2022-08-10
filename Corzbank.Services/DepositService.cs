using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class DepositService : IDepositService
    {
        private readonly GenericService<Deposit> _genericService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IWrappedVerificationService _verificationService;

        public DepositService(GenericService<Deposit> genericService, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager, IWrappedVerificationService verificationService)
        {
            _genericService = genericService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _verificationService = verificationService;
        }

        public async Task<IEnumerable<DepositDTO>> GetDeposits()
        {
            var deposits = await _genericService.GetRange();

            var result = _mapper.Map<IEnumerable<DepositDTO>>(deposits);

            return result;
        }

        public async Task<DepositDTO> GetDepositById(Guid id)
        {
            var deposit = await _genericService.Get(id);

            var result = _mapper.Map<DepositDTO>(deposit);

            return result;
        }

        public async Task<DepositDTO> OpenDeposit(DepositModel deposit)
        {
            var mappedDeposit = _mapper.Map<Deposit>(deposit);

            var generatedDeposit = mappedDeposit.GenerateDeposit();

            await _genericService.Insert(generatedDeposit);

            var result = _mapper.Map<DepositDTO>(generatedDeposit);

            return result;
        }

        public async Task<bool> CloseDeposit(Guid id)
        {
            var deposit = await GetDepositById(id);

            var currentUserEmail = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (deposit != null)
            {
                var verificationModel = new VerificationModel
                {
                    Email = currentUserEmail,
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
