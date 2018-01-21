using System.Collections.Generic;
using System.Threading.Tasks;
using DP;

namespace DAL
{
    public interface IDAL
    {
        Task<List<Country>> getCountriesAsync();
        Task<List<HistoryDTO>> getHRatesAsync(string countryCode);
        Task<Currencies> getRTRatesAsync();
    }
}