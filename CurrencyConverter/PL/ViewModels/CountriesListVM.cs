using BL;
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
    class CountriesListVM
    {
        private ObservableCollection<Currency> Currencies_ObservableCollection;
        public ICollectionView CurrenciesList { set; get; }


        private NotifyTaskCompletion<Currencies> _currencies;

        private BL_imp bl;

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
                if(CurrenciesList!=null)
                CurrenciesList.Refresh();
            }
        }



        
        public event PropertyChangedEventHandler PropertyChanged;

        public CountriesListVM()
        {
            currencies = new NotifyTaskCompletion<Currencies>(getRTRatesAsync());
            //CountriesList.Filter = WorkerFilter;
            //CountriesList.CollectionChanged += CountriesList_CollectionChanged;

        }

        private async Task<Currencies> getRTRatesAsync()
        {

            //Currencies a = await Task.Run(async () => await dAL.RTRatesAsync()); a.CurrenciesList = new ObservableCollection<Currency>(a.CurrenciesList); return a;
            Currencies a = await new BL_imp().getRTRatesAsync();
            a.CurrenciesList = new ObservableCollection<Currency>(a.CurrenciesList);
            CurrenciesList = CollectionViewSource.GetDefaultView(a.CurrenciesList);
            return a;

        }



        //private bool WorkerFilter(object item)
        //{
        //    Country country = item as Country;
        //    if (String.IsNullOrWhiteSpace(_filterString) || country == null || String.IsNullOrWhiteSpace(country.CountryName))
        //        return true;
        //    return country.CountryName.Contains(_filterString);
        //}
    }
}
