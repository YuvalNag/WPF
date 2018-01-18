using DP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DB_Context : DbContext
    {

        public DB_Context() : base()
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Currencies> CurrenciesByDate { get; set; }


    }
}
