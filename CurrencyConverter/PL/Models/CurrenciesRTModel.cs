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
            return  await Task.Run(async ()=>await new BL_imp().getRTRatesAsync());
        }
        
        
    }
}
