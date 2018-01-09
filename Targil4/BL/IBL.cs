using System.Collections.Generic;
using DataProtocol;

namespace BL
{
    public interface IBL
    {
        List<Worker> findWorker(string lastName);
        List<Worker> getWorker();
        bool setWorker(Worker worker);
    }
}