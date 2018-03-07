using BL;
using DP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
   public class CurrenciesRTModel
    {
        

       public async Task<Currencies> GetCurrencies()
        {
            string[] d = { "AED",
                         "AFN",
                         "BTC",
                         "ALL",
                         //"AMD",
                         //"ARS",
                         //"AUD",
                         //"AZN",
                         //"BAM",
                         //"BDT",
                         //"BGN",
                         //"BHD",
                         //"BND",
                         //"BOB",
                         "BRL",
                         "BYR",
                         "BZD",
                         "CAD",
                         //"CHF",
                         //"CLP",
                         //"CNY",
                         //"COP",
                         //"CRC",
                         //"CSD",
                         //"CZK",
                         //"DKK",
                         //"DOP",
                         //"DZD",
                         //"EEK",
                         //"EGP",
                         //"ETB",
                         //"EUR",
                         //"GBP",
                         //"GEL",
                         //"GTQ",
                         //"HKD",
                         //"HNL",
                         //"HRK",
                         //"HUF",
                         //"IDR",
                         "ILS",
                         //"INR",
                         //"IQD",
                         //"IRR",
                         //"ISK",
                         //"JMD",
                         //"JOD",
                         //"JPY",
                         //"KES",
                         //"KGS",
                         //"KHR",
                         //"KRW",
                         //"KWD",
                         //"KZT",
                         //"LAK",
                         //"LBP",
                         //"LKR",
                         //"LTL",
                         //"LVL",
                         //"LYD",
                         //"MAD",
                         //"MKD",
                         //"MNT",
                         //"MOP",
                         //"MVR",
                         //"MXN",
                         //"MYR",
                         //"NIO",
                         //"NOK",
                         //"NPR",
                         //"NZD",
                         //"OMR",
                         //"PAB",
                         //"PEN",
                         //"PHP",
                         //"PKR",
                         //"PLN",
                         //"PYG",
                         //"QAR",
                         //"RON",
                         //"RSD",
                         //"RUB",
                         "RWF",
                         "SAR",
                         "SEK",
                         "SGD",
                         //"SYP",
                         //"THB",
                         //"TJS",
                         //"TMT",
                         //"TND",
                         //"TRY",
                         //"TTD",
                         //"TWD",
                         //"UAH",
                         "USD",
                         //"UYU",
                         //"UZS",
                         //"VEF",
                         //"VND",
                         //"XOF",
                         //"YER",
                         //"ZAR",
                         "ZWL"};
            
            
            
            
            
            
            return  await Task.Run(async ()=> {
                Currencies currencies = await new BL_imp().getRTRatesAsync();
                currencies.CurrenciesList = currencies.CurrenciesList.Where(t => d.Contains(t.IssuedCountryCode)).ToList();
                return currencies;
            });
        }
        
        
    }
}
