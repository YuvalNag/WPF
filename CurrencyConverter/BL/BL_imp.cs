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
      
        public async Task<List<HistoryDTO>> getHRatesAsync(string sourceCountryCode, string targetCountryCode="USD")
        {
            IDAL dal = new DAL_imp();
            List<HistoryDTO> listSource = await dal.getHRatesAsync(sourceCountryCode);
            if (targetCountryCode != "USD")
            {
                List<HistoryDTO> listTarget = await dal.getHRatesAsync(targetCountryCode);
                for (int i = 0; i < listSource.Count; i++)
                {
                    listSource[i].Currency.Value /= listTarget[i].Currency.Value;
                } 
            }
            return listSource;


        }
        public Task<Currencies> getRTRatesAsync()
        {
            return new DAL_imp().getRTRatesAsync();
        }

    }
}
