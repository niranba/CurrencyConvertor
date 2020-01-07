﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiranTest
{
    public class CurrencyManager
    {
        static string[] arrFrom = new string[] { "USD", "GBP", "EUR", "EUR" };
        static string[] arrTo = new string[] { "ILS", "EUR", "JPY", "USD" };
        public static void getAllConvertions()
        {
            for (int i = 0; i < arrFrom.Length; i++)
            {
                decimal rate = -1;
                if (arrTo[i] != null)
                {
                    //checking curreny at xe currency
                    rate = XECurrencyAPI.CurrencyConversion(arrFrom[i], arrTo[i]);

                    if (rate == -1) //an error ocured at xe , check at yahoo currency
                    {
                        rate = YahooCurrencyAPI.YahooConversion(arrFrom[i], arrTo[i]);

                        if (rate == -1) //an error ocured at yahoo , check at google currency
                        {
                            rate = GoogleCurrecyAPI.CurrencyConvert(arrFrom[i], arrTo[i]);
                        }

                    }

                    if (rate == -1) //the error wasnt fixed by checking other services
                    {
                        //log the error and dont save the rate 
                    }

                    else // the rate is correct so we need to save
                    {
                        //I chose redis since its fast rliable and sinchronised, 
                        //currently im saving the data localy in redis but if we'll pay for a server every one would be able to connect and get the currency they need
                       




                        //saving rate to redis in this format key "converstionDictionary::USD-ILS" and value is the convertion rate and then the date separeted by ":" 




                        var redis = RedisCache.RedisCacheDB;

                        redis.StringSet("converstionDictionary::"+ arrFrom[i] +"-"+ arrTo[i], rate.ToString()+":"+DateTime.Now.ToString());

                        /* in order to test to see if it saved correctly run this
                         var val = redis.StringGet("converstionDictionary::" + arrFrom[i] + "-" + arrTo[i]);

                     for example 
                         var val = redis.StringGet("converstionDictionary::USD-ILS");


                         */

                    }

                }


            }


        }
    }
}

