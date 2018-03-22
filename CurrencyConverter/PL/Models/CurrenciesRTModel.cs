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
        private Currencies localCurencies { get; set; }

        public  Task<Currencies> GetCurrencies(string[] currenciesToReturn=null)
        { 
            return   Task.Run(async ()=> {
                if (localCurencies == null)
                    localCurencies = await new BL_imp().getRTRatesAsync();     
                if(currenciesToReturn != null)
                return new Currencies { CurrenciesList = localCurencies.CurrenciesList.Where(t => currenciesToReturn.Contains(t.IssuedCountryCode)).ToList(), date = localCurencies.date } ;
                return localCurencies ;
            });
        }
        
        
    }
}
