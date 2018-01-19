using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class Currency
    {
        public int id { get; set; }
        public string Direction { get; set; }
        public float Magnitude { get; set; }
        public float Value { get; set; }
        public string IssuedCountryCode { get; set; }
        public string IssuedCountryName { get; set; }
    }
}
