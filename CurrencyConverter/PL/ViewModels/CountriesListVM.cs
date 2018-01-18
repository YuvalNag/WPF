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
        private ObservableCollection<Currency> Countries_ObservableCollection;
        public ICollectionView CountriesList { set; get; }


      

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilterString"));
                CountriesList.Refresh();
            }
        }



        
        public event PropertyChangedEventHandler PropertyChanged;

        public CountriesListVM()
        {
            //Countries_ObservableCollection = new ObservableCollection<Country>(model.workerList);
            CountriesList = CollectionViewSource.GetDefaultView(Countries_ObservableCollection);
         //   CountriesList.Filter = WorkerFilter;
            //CountriesList.CollectionChanged += CountriesList_CollectionChanged;

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
