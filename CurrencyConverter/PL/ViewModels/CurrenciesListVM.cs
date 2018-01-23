
using DP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.ViewModels
{
    class CurrenciesListVM:INotifyPropertyChanged
    {
        private ObservableCollection<Currency> Currencies_ObservableCollection;

        private ICollectionView _currenciesList;
        public ICollectionView currenciesList {
            get
            {
                return _currenciesList;
            }
            set
            {
                _currenciesList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("currenciesList"));

            }
        }
       
        private NotifyTaskCompletion<Currencies> _currencies;
        public NotifyTaskCompletion<Currencies> currencies
        {
            set
            {
                _currencies = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("currencies"));
            }
            get
            {
                return _currencies;
            }
        }

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilterString"));
                if(_currenciesList!=null)
                   _currenciesList.Refresh();
            }
        }

     
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private Models.CurrenciesRTModel rTModel;
        public CurrenciesListVM()
        {
            rTModel = new Models.CurrenciesRTModel();
            currencies=new NotifyTaskCompletion<Currencies>(ConvertToObservableCollection());
            //CountriesList.Filter = WorkerFilter;
            //CountriesList.CollectionChanged += CountriesList_CollectionChanged;

        }

        private async Task<Currencies> ConvertToObservableCollection()
        {
            
            Currencies tempCurrencies = await (rTModel.GetCurrencies());
            tempCurrencies.CurrenciesList = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            currenciesList = CollectionViewSource.GetDefaultView(tempCurrencies.CurrenciesList);
            currenciesList.Filter = CurrenciesFilter;
            Date =tempCurrencies.date;
            return tempCurrencies;

        }



        private bool CurrenciesFilter(object item)
        {
            Currency currency = item as Currency;
            if (String.IsNullOrWhiteSpace(_filterString) || currency == null)
                return true;
            return currency.IssuedCountryName.ToLower().Contains(_filterString.ToLower());
        }
    }
}
