using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NiranTest
{
    class GoogleCurrecyAPI
    {
        static public decimal CurrencyConvert( string fromCurrency, string toCurrency)
        {
            decimal currency = 0;
   
            try
            {
                string url = string.Format("https://www.google.com/finance/converter?from={0}&to={1}", fromCurrency.ToUpper(), toCurrency.ToUpper());
                WebRequest request = WebRequest.Create(url); //creating get request to google api
                StreamReader streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);
                string result = Regex.Matches(streamReader.ReadToEnd(), "([^<]+)")[0].Groups[1].Value;
                string rs = new Regex(@"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*").Match(result).Groups[1].Value;
                if (decimal.TryParse((new Regex(@"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*").Match(result).Groups[1].Value), out currency))// parsing the rate
                {
                   string convertedAmount = currency.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                //here i would normaly log the error beore returning -1;
                return -1;
            }
            return currency;
        }

    }
}
