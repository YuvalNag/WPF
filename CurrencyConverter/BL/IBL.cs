using System.Collections.Generic;
using System.Threading.Tasks;
using DP;

namespace BL
{
    public interface IBL
    {
        Task<List<Country>> getCountriesAsync();
        Task<List<HistoryDTO>> getHRatesAsync(string code,string targetcode);
        Task<Currencies> getRTRatesAsync();
    }
}