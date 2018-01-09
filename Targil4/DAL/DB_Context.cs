using DataProtocol;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class DB_Context : DbContext
    {
        public DB_Context():base()
        {
        }

        public DbSet<Worker> workers { get; set; }

    }
}
