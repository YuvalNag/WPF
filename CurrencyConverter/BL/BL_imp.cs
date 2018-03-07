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
        public async Task<List<HistoryDTO>> getHRatesAsync(string sourceCountryCode, string targetCountryCode="USD")
        {
            IDAL dal = new DAL_imp();
            List<HistoryDTO> listSource = await dal.getHRatesAsync(sourceCountryCode);
            if (!String.Equals(targetCountryCode,"USD"))
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

        private List<Object> FilterByCodeCountry(List<Object> originaList,Predicate<Object> predicate)
        {
            List<Object> filterdList = new List<object>();
            List<string> codesList = new List<string>() { "USD", "ILS", "AED" ,"AFN" ,"ALL" ,"AMD" ,"ANG" ,"AOA" ,"ARS" ,"AUD" ,"SGD" ,"GBP" };
            foreach (var code in codesList)
            {
                filterdList.Add(originaList.Find(predicate));  
            }
            return filterdList;
        }
        
        //private Predicate<Country> CountryPredicate()
        //{

        //}
    }
}
