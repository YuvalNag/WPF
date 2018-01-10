using BL;
using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil4
{
    class Model
    {
        public IBL bl { get; set; }

        public List<Worker> workerList { get; set; }
        public Model()
        {
            bl = new BL_imp();
            load();
            
        }
        public void load()
        {
            workerList = bl.getWorker();
        }

        
        public bool addWorker(Worker worker )
        {
            return bl.setWorker(worker);
        }
    }
}
