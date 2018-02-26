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

        public async Task<List<HistoryDTO>> GetCurrencies(string countryCode)
        {
            return await Task.Run(async () => await new BL_imp().getHRatesAsync(countryCode));
        }

        public async Task<List<Country>> GetCountries()
        {
            return await Task.Run(async () => await new BL_imp().getCountriesAsync());
        }

    }
}
