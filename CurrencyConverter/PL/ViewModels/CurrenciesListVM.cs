
using DP;
using PL.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModels
{
   public class CurrenciesListVM : INotifyPropertyChanged/*, ICurrenciesListVM*/
    {



        #region Properties

        private Currency _relativeCurrency;
        public Currency relativeCurrency
        {
            get
            {
                return _relativeCurrency;
            }
            set
            {
                if (value != null)
                {
                    _relativeCurrency = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("relativeCurrency"));
                }

            }
        }
        private bool _showTop15;
        public bool showTop15
        {
            get
            {
                return _showTop15;
            }
            set
            {

                _showTop15 = value;
              
                    if (_showTop15)
                    {

                        currenciesList = Top12CollectioView;
                    currenciesList.Refresh();
                    relativeCurrenciesListCollection = Top12List;

                    }
                    else
                    {

                        currenciesList = allCollectioView;
                        currenciesList.Refresh();
                        relativeCurrenciesListCollection = allList;

                    }
               
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("showTop15"));

            }
        }

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

        private ObservableCollection<Currency> _relativeCurrenciesListCollection;
        public ObservableCollection<Currency> relativeCurrenciesListCollection
        {
            get
            {
                return _relativeCurrenciesListCollection;
            }
            set
            {
                _relativeCurrenciesListCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("relativeCurrenciesListCollection"));

            }
        }

        //a task prop for progress bar
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
                    currenciesList.Refresh();
            }
        }
        


        //private DateTime _date;
        //public DateTime Date
        //{
        //    get { return _date; }
        //    set
        //    {
        //        _date = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
        //    }
        //}

        private Models.CurrenciesRTModel rTModel { set; get; }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;



       
        public CurrenciesListVM()
        {
            
            rTModel = new Models.CurrenciesRTModel();
            taskCurrencies=new NotifyTaskCompletion<Currencies>(ConvertToICollectionViewAsync());
            //UC = new ObservableCollection<UserControl>();
            //UC.Add(new ColumnsChartUS());

        }

        //get in the backround the 
        private async Task<Currencies> ConvertToICollectionViewAsync()
        {
            string[] d = { "AED",
                         //"AFN",
                         "BTC",
                         //"ALL",
                         //"AMD",
                         //"ARS",
                         "AUD",
                         //"AZN",
                         //"BAM",
                         //"BDT",
                         //"BGN",
                         //"BHD",
                         //"BND",
                         //"BOB",
                         "BRL",
                         //"BYR",
                         "BZD",
                         "CAD",
                         //"CHF",
                         //"CLP",
                         //"CNY",
                         //"COP",
                         //"CRC",
                         //"CSD",
                         "CZK",
                         //"DKK",
                         //"DOP",
                         //"DZD",
                         //"EEK",
                         //"EGP",
                         //"ETB",
                         "EUR",
                         "GBP",
                         //"GEL",
                         //"GTQ",
                         "HKD",
                         //"HNL",
                         //"HRK",
                         //"HUF",
                         //"IDR",
                         "ILS",
                         //"INR",
                         //"IQD",
                         //"IRR",
                         //"ISK",
                         //"JMD",
                         //"JOD",
                         //"JPY",
                         //"KES",
                         //"KGS",
                         //"KHR",
                         //"KRW",
                         //"KWD",
                         //"KZT",
                         //"LAK",
                         //"LBP",
                         //"LKR",
                         //"LTL",
                         //"LVL",
                         //"LYD",
                         //"MAD",
                         //"MKD",
                         //"MNT","MOP","MVR","MXN","MYR","NIO","NOK","NPR","NZD","OMR","PAB","PEN","PHP","PKR","PLN","PYG","QAR","RON","RSD","RUB","RWF",
                         "SAR", "SEK","SGD",
                         //"SYP","THB","TJS","TMT","TND","TRY","TTD","TWD","UAH",
                         "USD",// "UYU","UZS","VEF","VND","XOF","YER","ZAR","ZWL"
            };
            Currencies tempCurrencies = await (rTModel.GetCurrencies());
            allList = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            allCollectioView = CollectionViewSource.GetDefaultView(new ObservableCollection<Currency>(allList));
            allCollectioView.Filter = CurrenciesFilter;

            string[] currenciescodes = {  "AED", "BTC", "ILS", "EUR", "BRL", "AUD", "BZD", "CAD", "GBP", "CZK", "HKD", "SAR", "SEK", "SGD", "USD" };
            tempCurrencies = await (rTModel.GetCurrencies(currenciescodes));
            Top12List = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            Top12CollectioView = CollectionViewSource.GetDefaultView(new ObservableCollection<Currency>(Top12List));
            Top12CollectioView.Filter = CurrenciesFilter;

            showTop15 = true;
           
            return tempCurrencies;

        }
        
        private ObservableCollection<Currency> Top12List { get; set; }
        private ICollectionView Top12CollectioView { get; set; }

        private ObservableCollection<Currency> allList { get; set; }
        private ICollectionView allCollectioView { get; set; }

        // a predicate to filter for ICollectionView
        private bool CurrenciesFilter(object item)
        {
            Currency currency = item as Currency;
            if (String.IsNullOrWhiteSpace(FilterString) || currency == null)
                return true;
            return currency.IssuedCountryName.ToLower().Contains(FilterString.ToLower());
        }

    
    }
}
