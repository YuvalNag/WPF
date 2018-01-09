using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_imp : IDAL
    {
        public List<Worker> loadWorkers()
        {
            DB_Context context = new DB_Context();
            return context.workers.ToList();
        }
        public bool storeWorker(Worker worker )
        {
            DB_Context context = new DB_Context();
            context.workers.Add(worker);
            return context.SaveChanges() != 0;
        }
    }
}
