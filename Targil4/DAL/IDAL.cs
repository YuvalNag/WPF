using System.Collections.Generic;
using DataProtocol;

namespace DAL
{
    public interface IDAL
    {
        List<Worker> loadWorkers();
        bool storeWorker(Worker worker);
    }
}