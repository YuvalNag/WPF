
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
    class CurrenciesListVM:INotifyPropertyChanged,BaseVM
    {
        #region Properties

        private ICollectionView _currenciesList;
        public ICollectionView currenciesList
        {
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

        private NotifyTaskCompletion<Currencies> _taskCurrencies;
        public NotifyTaskCompletion<Currencies> taskCurrencies
        {
            set
            {
                _taskCurrencies = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("taskCurrencies"));
            }
            get
            {
                return _taskCurrencies;
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
                if (_currenciesList != null)
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

        private Models.CurrenciesRTModel rTModel { set; get; }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        public CurrenciesListVM()
        {
            rTModel = new Models.CurrenciesRTModel();
            taskCurrencies=new NotifyTaskCompletion<Currencies>(ConvertToObservableCollection());
        }

        //get in the backround the 
        private async Task<Currencies> ConvertToObservableCollection()
        {
            
            Currencies tempCurrencies = await (rTModel.GetCurrencies());
            tempCurrencies.CurrenciesList = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            currenciesList = CollectionViewSource.GetDefaultView(tempCurrencies.CurrenciesList);
            currenciesList.Filter = CurrenciesFilter;
            Date =tempCurrencies.date;
            return tempCurrencies;

        }

        // a predicate to filter for ICollectionView
        private bool CurrenciesFilter(object item)
        {
            Currency currency = item as Currency;
            if (String.IsNullOrWhiteSpace(_filterString) || currency == null)
                return true;
            return currency.IssuedCountryName.ToLower().Contains(_filterString.ToLower());
        }
    }
}
