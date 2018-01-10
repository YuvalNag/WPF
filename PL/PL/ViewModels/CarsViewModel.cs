using DataProtocol;
using PL.Commands;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class CarsViewModel : INotifyPropertyChanged, ICarsViewModel
    {
        public ObservableCollection<Car> Cars { get; set; }
        public CarsModel CurrentModel { get; set; }

        public AddCarCommand AddCar { get; set; }
        public CarsViewModel()
        {
            CurrentModel = new CarsModel();
            Cars = new ObservableCollection<Car>(CurrentModel.Cars);
            Cars.CollectionChanged += Cars_CollectionChanged;
            //AddCar = new AddCarCommand(this);
            AddCar = new AddCarCommand();
            AddCar.CarAdded += AddCar_CadAdded;

        }

        private void AddCar_CadAdded(string Description)
        {            
            CurrentModel.AddCar(Description);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string City
        {
            get { return CurrentModel.City; }
            set { CurrentModel.City = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("City"));
            }

        }


        private void Cars_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var x = e.Action;
            if (x == NotifyCollectionChangedAction.Add)
            {
                var Description = (e.NewItems[0] as Car).Description;
                CurrentModel.AddCar(Description);
            }
            //כאן עדכן את המודל בשינויים שנעשו בוויו
        }
    }
}
