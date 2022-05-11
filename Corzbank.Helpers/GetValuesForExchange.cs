using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Corzbank.Helpers
{
    public static class GetValuesForExchange
    {
        public static dynamic GetValues()
        {
            WebClient wc = new WebClient();

            var parsedString = wc.DownloadString("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");

            dynamic valuesArray = JsonConvert.DeserializeObject(parsedString);

            return valuesArray;
        }
    }
}
