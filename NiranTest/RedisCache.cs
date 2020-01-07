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
    }
}
