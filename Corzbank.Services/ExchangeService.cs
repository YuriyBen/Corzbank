using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Corzbank.Helpers;
using Corzbank.Services.Interfaces;
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
        private readonly GenericService<Exchange> _genericService;
        private readonly IMapper _mapper;

        public ExchangeService(GenericService<Exchange> genericService, IMapper mapper)
        {
            _mapper = mapper;
            _genericService = genericService;
        }

        public async Task<IEnumerable<Exchange>> GetValues()
        {
            var result = await _genericService.GetRange();

            return result;
        }

        public async Task<bool> CreateExchage()
        {
            var existingValues = _genericService.FindByCondition(e => e.ExchangeCurrency == Currency.EUR || e.ExchangeCurrency == Currency.USD);

            if (existingValues == null)
            {
                var listOfValues = GetValuesForExchange.GetValues();

                var mappedValues = _mapper.Map<IEnumerable<Exchange>>(listOfValues);

                await _genericService.InsertRange(mappedValues);

                return true;
            }

            return false;
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

                        await _genericService.Update(mappedValue);
                        break;
                    }
                }
            }

            return true;
        }
    }
}
