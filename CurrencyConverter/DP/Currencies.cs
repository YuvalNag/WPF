using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class Currencies
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public virtual IList<Currency> CurrenciesList { get; set; }
    }
}
