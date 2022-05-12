using Corzbank.Data.Entities.Models;
using Corzbank.Data.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Corzbank.Helpers
{
    public static class GetValuesForExchange
    {
        public static IEnumerable<ExchangeModel> GetValues()
        {
            WebClient wc = new WebClient();

            var parsedString = wc.DownloadString("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");

            var valuesArray = JsonConvert.DeserializeObject<ExchangeModelForParsing[]>(parsedString);

            List<ExchangeModel> exchangeList = new List<ExchangeModel>();

            foreach (var value in valuesArray)
            {
                ExchangeModel exchangeModel = new ExchangeModel
                {
                    BaseCurrency = (Currency)Enum.Parse(typeof(Currency), value.base_ccy),
                    ExchangeCurrency = (Currency)Enum.Parse(typeof(Currency), value.ccy),
                    Buy = Decimal.Parse(value.buy),
                    Sell = Decimal.Parse(value.sale)
                };

                exchangeList.Add(exchangeModel);
            }

            return exchangeList;
        }
    }
}
