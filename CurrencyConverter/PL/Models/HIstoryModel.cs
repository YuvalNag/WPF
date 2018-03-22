using BL;
using DP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    class HIstoryModel
    {

  
        public  Task<List<HistoryDTO>> GetHRates(string sourceCountryCode, string targetCountryCode = "USD")
        {
            return  Task.Run( () =>  new BL_imp().getHRatesAsync(sourceCountryCode, targetCountryCode));
        }

        public  Task<List<Country>> GetCountries()
        {
            return  Task.Run( () =>  new BL_imp().getCountriesAsync());
        }

    }
}
