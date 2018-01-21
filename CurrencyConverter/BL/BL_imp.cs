using DAL;
using DP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL_imp:IBL
    {
        public Task<List<Country>> getCountriesAsync()
        {
         return new DAL_imp().getCountriesAsync();
        }
        public Task<List<HistoryDTO>> getHRatesAsync(string countryCode)
        {
            return new DAL_imp().getHRatesAsync( countryCode);
        }
        public Task<Currencies> getRTRatesAsync()
        {
            return new DAL_imp().getRTRatesAsync();
        }

    }
}
