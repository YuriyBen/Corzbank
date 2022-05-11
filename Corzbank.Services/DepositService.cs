using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
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

        public DepositService(GenericService<Deposit> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Deposit>> GetDeposits()
        {
            var result = await _genericService.GetRange();

            return result;
        }

        public async Task<Deposit> GetDepositById(Guid id)
        {
            var result = await _genericService.Get(id);

            return result;
        }

        public async Task<Deposit> OpenDeposit(DepositModel deposit)
        {
            var mappedDeposit = _mapper.Map<Deposit>(deposit);

            var result = mappedDeposit.GenerateDeposit();

            await _genericService.Insert(result);
           
            return result;
        }

        public async Task<bool> DeleteDeposit(Guid id)
        {
            var deposit = await GetDepositById(id);

            if (deposit != null)
            {
                await _genericService.Remove(deposit);

                return true;
            }
            return false;
        }
    }
}
