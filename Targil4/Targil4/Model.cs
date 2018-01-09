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

        public Model()
        {
            bl = new BL_imp();
        }
        public bool addWorker(Worker worker )
        {
            return bl.setWorker(worker);
        }
    }
}
