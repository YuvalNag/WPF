using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class CoverterVM:INotifyPropertyChanged
    {

        //private DateTime _choosenDate;
        //public DateTime ChoosenDate {
        //    get { return _choosenDate; }
        //    set { _choosenDate = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChoosenDate"));
        //    }
        //}
        
        private double _result;
        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Result"));
            }
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Amount"));
            }
        }
        ObservableCollection<int> ListOfCountries { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;



    }
}
