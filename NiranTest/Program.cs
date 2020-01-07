using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiranTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //i used in the project two nugget librarys newtonsoft.json and StackExchange.Redis


            // we can run this cosole application using task scheduler in fixed intervals  (or non stop depending on when and how much to update the data)


            //1.
            CurrencyManager.getAllConvertions();



            //2.
            RedisCache.getAllRates();

        }
    }
}
