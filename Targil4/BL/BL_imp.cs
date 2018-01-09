using DAL;
using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL_imp : IBL
    {
        
        public List<Worker> findWorker(string lastName)
        {
            IDAL dal = new DAL_imp();
            return dal.loadWorkers().FindAll(worker=>lastName.ToUpper().StartsWith(worker.lastName.ToUpper()));
        }

        public List<Worker> getWorker()
        {
            IDAL dal = new DAL_imp();
            return dal.loadWorkers();
        }

        public bool setWorker(Worker worker)
        {
            IDAL dal = new DAL_imp();
            return dal.storeWorker(worker);
        }

    }
}
