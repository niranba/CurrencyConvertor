using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiranTest
{
    class YahooCurrencyAPI
    {

        public static decimal YahooConversion( string fromCurrencyCode, string toCurrencyCode)
        {
            try
            {
                WebClient web = new WebClient(); //create a get request to yahoo 
                const string yahooAPIUrl = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";
                string url = String.Format(yahooAPIUrl, fromCurrencyCode, toCurrencyCode);

                // Get response as string
                string response = new WebClient().DownloadString(url); // calculating 1 unit from one currency code to other.

                decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture); // Convert string to number
                return exchangeRate;


            }
            catch (Exception e)
            {
                //here i would normaly log the error beore returning -1;
                return -1;
            }
        }
    }
}
