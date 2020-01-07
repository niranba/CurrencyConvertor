using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiranTest
{
    public  class  RedisCache
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

         static  RedisCache()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost" }
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCacheDB => Connection.GetDatabase();
        public static void saveRate(string from, string to,decimal rate)
        {
            //saving rate to redis in this format key "converstionDictionary::USD-ILS" and value is the convertion rate and then the date separeted by ":" 


            var redis = RedisCache.RedisCacheDB;

            redis.StringSet("converstionDictionary::" + from + "-" + to, rate.ToString() + ":" + DateTime.Now.ToString());

            /* in order to test to see if it saved correctly run this
             var val = redis.StringGet("converstionDictionary::" + arrFrom[i] + "-" + arrTo[i]);

         for example 
             var val = redis.StringGet("converstionDictionary::USD-ILS");
                */



        }

        public static void getAllRates()
        {
            // get all values stored in under converstionDictionary
            foreach (string key in LazyConnection.Value.GetServer(LazyConnection.Value.GetEndPoints()[0]).Keys(pattern: "converstionDictionary*"))
            {
              string value=  RedisCacheDB.StringGet(key);
                var splitedKey = key.Split(':');
                var splitedValue = value.Split(':');
                string line = splitedKey[2] + " value: " + splitedValue[0] + " updated at: " + splitedValue[1] + ":" + splitedValue[2] + ":" + splitedValue[3];
                Console.WriteLine(line); // printing out the output line, example: "EUR-JPY value: 121.299 updated at: 07/01/2020 15:40:58"
            }


        }
    }


}
