using AutoMapper;
using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Repository.Interfaces;
using Corzbank.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IGenericRepository<Exchange> _exchangeRepo;
        private readonly IMapper _mapper;

        public ExchangeService(IGenericRepository<Exchange> exchangeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _exchangeRepo = exchangeRepo;
        }

        public async Task<IEnumerable<Exchange>> GetValues()
        {
            var result = await _exchangeRepo.GetQueryable().ToListAsync();
            return result;
        }

        public async Task<bool> CreateExchage()
        {
            var existingValues = _exchangeRepo.GetQueryable().FirstOrDefaultAsync(e => e.ExchangeCurrency == Currency.EUR || e.ExchangeCurrency == Currency.USD);

            if (existingValues != null)
                return false;

            var listOfValues = GetValuesForExchange.GetValues();

            var mappedValues = _mapper.Map<IEnumerable<Exchange>>(listOfValues);

            await _exchangeRepo.InsertRange(mappedValues);

            return true;
        }

        public async Task<bool> UpdateExchage()
        {
            var valuesFromDb = await GetValues();

            var listOfValues = GetValuesForExchange.GetValues();

            foreach (var valueFromDb in valuesFromDb)
            {
                foreach (var value in listOfValues)
                {
                    if (value.ExchangeCurrency == valueFromDb.ExchangeCurrency)
                    {
                        var mappedValue = _mapper.Map(value, valueFromDb);

                        await _exchangeRepo.Update(mappedValue);
                        break;
                    }
                }
            }

            return true;
        }
    }
}
