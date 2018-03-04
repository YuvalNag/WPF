using DP;
using PL.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.ViewModels
{
   public class HistoryVM : INotifyPropertyChanged, IHistoryVM
    {
        private ICommand _switchCommand;
        public ICommand switchCommand {
            get
            {
                return _switchCommand;
            }
            set
            {
                _switchCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("switchCommand"));
            }
        }
        //public ObservableCollection<HistoryDTO> StockPriceDetails { get; set; }
        private NotifyTaskCompletion<ObservableCollection<HistoryDTO>> _stockPriceDetails;
        public NotifyTaskCompletion<ObservableCollection<HistoryDTO>> stockPriceDetails
        {
            set
            {
                _stockPriceDetails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("stockPriceDetails"));
            }
            get
            {
                return _stockPriceDetails;
            }
        }
        private NotifyTaskCompletion<ObservableCollection<Country>> _countries;
        public NotifyTaskCompletion<ObservableCollection<Country>> countries
        {
            set
            {
                _countries = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("countries"));
            }
            get
            {
                return _countries;
            }
        }
        private Models.HIstoryModel hModel { set; get; }


        
        public event PropertyChangedEventHandler PropertyChanged;

        private Country _country;
        public Country country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                stockPriceDetails = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(ConvertStockToObservableCollection());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("country"));
                
            }
        }

        private Country _raltiveCountry;
        public Country raltiveCountry
        {
            get
            {
                return _raltiveCountry;
            }
            set
            {
                _raltiveCountry = value;
                stockPriceDetails = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(ConvertStockToObservableCollection());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("raltiveCountry"));

            }
        }
        public HistoryVM()
        {
            //country.Code = "USA";
            hModel = new Models.HIstoryModel();
           // stockPriceDetails = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(ConvertStockToObservableCollection());
            countries = new NotifyTaskCompletion<ObservableCollection<Country>>(ConvertCountriesToObservableCollection());
            switchCommand = new SwitchCurrencyCommand(this);
        }

        private void stopTaskIfRunning(NotifyTaskCompletion<Task> task)
        {

        }


        private async Task<ObservableCollection<Country>> ConvertCountriesToObservableCollection()
        {
            List<Country> countriesTemp = await  hModel.GetCountries();

            //update the country and relative country
            country = countriesTemp.Find(t => String.Equals(t.Code, "ILS"));

            //skip the property  
            _raltiveCountry = countriesTemp.Find(t => String.Equals(t.Code,"USD"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("raltiveCountry"));


            return new ObservableCollection<Country>(countriesTemp);
        }


        public void switchSourchCurrencyAndRelative()
        {
            Country tempRelative = raltiveCountry;
            Country tempsource = country;
            _country = tempRelative;
            raltiveCountry = tempsource;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("country"));
           

        }
        private async Task<ObservableCollection<HistoryDTO>> ConvertStockToObservableCollection()
        {
            List<HistoryDTO> tempCurrencies;
            if (raltiveCountry != null)
                   tempCurrencies = await hModel.GetCurrenciesR(country.Code,raltiveCountry.Code);
            else
                tempCurrencies = await hModel.GetCurrenciesR(country.Code);

            return new ObservableCollection<HistoryDTO>(tempCurrencies);
            
        }
    }
}
