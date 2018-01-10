using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
   public class CarsModel
    {
        public List<Car> Cars { get; set; }
        public string City { get; set; }

        public CarsModel()
        {
            //אפשר לגשת להביא ממקורות כמו לוגיקה עסקית
            // Better use Repository & UnitOfWork Patterns
            Cars = new List<Car>();
            Cars.Add(new Car { Id = 1, Description = "nice car" });
            Cars.Add(new Car { Id = 1, Description = "SUV" });
            Cars.Add(new Car { Id = 1, Description = "Truck" });
            this.City = "Tel Aviv";

        }
        // אפשר להוסיף עוד פונקציות
        // Remove , Find , Update ...., Load
        public void AddCar(string CarDescription)
        {
            Cars.Add(new Car { Id = -1, Description = CarDescription });

        }
    }
}
