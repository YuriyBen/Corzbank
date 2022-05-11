using Corzbank.Data.Entities;
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

        public ExchangeService(GenericService<Exchange> genericService)
        {
            _genericService = genericService;
        }
        
        public async Task<IEnumerable<Exchange>> GetValues()
        {
            var result = await _genericService.GetRange();

            return result;
        }

        public async Task<Exchange> GetValueById(Guid id)
        {
            var result = await _genericService.Get(id);

            return result;
        }

        public async Task<bool> CreateExchage()
        {
            dynamic valuesArray = GetValuesForExchange.GetValues();

            if (!_genericService.CheckByCondition(e => e.ExchangeCurrency == Currency.EUR || e.ExchangeCurrency == Currency.USD))
            {
                foreach (var value in valuesArray)
                {
                    if (value.ccy == "BTC")
                        continue;

                    var exchange = new Exchange
                    {
                        ExchangeCurrency = value.ccy,
                        BaseCurrency = value.base_ccy,
                        Buy = value.buy,
                        Sell = value.sale
                    };
                    await _genericService.Insert(exchange);
                }

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateExchage()
        {
            var valuesFromDb = await GetValues();

            dynamic valuesArray = GetValuesForExchange.GetValues();

            foreach (var valueFromDb in valuesFromDb)
            {
                foreach (var value in valuesArray)
                {
                    if (value.ccy == valueFromDb.ExchangeCurrency)
                    {
                        valueFromDb.ExchangeCurrency = value.ccy;
                        valueFromDb.BaseCurrency = value.base_ccy;
                        valueFromDb.Buy = value.buy;
                        valueFromDb.Sell = value.sale;

                        await _genericService.Update(valueFromDb);
                        break;
                    }
                }
            }

            return true;
        }

        public async Task<bool> DeleteExchange()
        {
            var values = await GetValues();

            if (_genericService.CheckByCondition(e => e.ExchangeCurrency == Currency.EUR || e.ExchangeCurrency == Currency.USD))
            {
                foreach (var value in values)
                {
                    await _genericService.Remove(value);
                }

                return true;
            }

            return false;
        }
    }
}
