using DP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class HistoryVM:INotifyPropertyChanged
    {
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("raltiveCountry"));

            }
        }
        public HistoryVM()
        {
            //country.Code = "USA";
            hModel = new Models.HIstoryModel();
           // stockPriceDetails = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(ConvertStockToObservableCollection());
            countries = new NotifyTaskCompletion<ObservableCollection<Country>>(ConvertCountriesToObservableCollection());
        }

        private async Task<ObservableCollection<Country>> ConvertCountriesToObservableCollection()
        {
            List<Country> countriesTemp = await  hModel.GetCountries();
            country = countriesTemp.Find(t => t.Code.CompareTo("USA")==0);
            return new ObservableCollection<Country>(countriesTemp);
        }

        private async Task<ObservableCollection<HistoryDTO>> ConvertStockToObservableCollection()
        {
            List<HistoryDTO> tempCurrencies = await hModel.GetCurrencies(country.Code);
            return new ObservableCollection<HistoryDTO>(tempCurrencies);
            
        }
    }
}
