using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiranTest
{
    public class XECurrencyAPI
    {
        private const string urlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";
        public static decimal CurrencyConversion(string fromCurrency, string toCurrency)
        {
            try
            {

                string url = string.Format(urlPattern, fromCurrency, toCurrency);//creating get requesdt to xe api

                using (var wc = new WebClient())
                {
                    var json = wc.DownloadString(url);

                    Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(json);
                    decimal exchangeRate = (decimal)token.SelectToken("rate"); //parsing the result to decimal

                    return exchangeRate;
                }
            }

            catch (Exception e)
            {
                //here i would normaly log the error beore returning -1;
                return -1;
            }
        }

    }
}
