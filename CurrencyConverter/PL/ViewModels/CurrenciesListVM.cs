
using DP;
using PL.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.ViewModels
{
   public class CurrenciesListVM : BaseVM
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
                    OnPropertyChanged();
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
                //update the list accordingly
                if (_showTop15)
                {
                    currenciesList = Top15CollectioView;
                    currenciesList.Refresh();
                    relativeCurrenciesListCollection = Top15List;
                }
                else
                {
                    currenciesList = allCollectioView;
                    currenciesList.Refresh();
                    relativeCurrenciesListCollection = allList;
                }

                OnPropertyChanged();
            }
        }

        //the list for the main display
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
                OnPropertyChanged();


            }
        }

        //the list for te combo box (needs to be diffrent the the other list)_
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
                OnPropertyChanged();


            }
        }

        //a task prop for progress bar
        private NotifyTaskCompletion<Currencies> _taskCurrencies;
        public NotifyTaskCompletion<Currencies> taskCurrencies
        {
            set
            {
                _taskCurrencies = value;
                OnPropertyChanged();
            }
            get
            {
                return _taskCurrencies;
            }
        }


        //a prop for binding and filtering the list
        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged();

                if (_currenciesList != null)
                    currenciesList.Refresh();
            }
        }

        //props for supporting swap 
        private ObservableCollection<Currency> Top15List { get; set; }
        private ICollectionView Top15CollectioView { get; set; }

        private ObservableCollection<Currency> allList { get; set; }
        private ICollectionView allCollectioView { get; set; }


        private CurrenciesRTModel RTModel { set; get; }



        #endregion




        public CurrenciesListVM()
        {
            
            RTModel = new CurrenciesRTModel();
            taskCurrencies=new NotifyTaskCompletion<Currencies>(ConvertToICollectionViewAsync());
        }

        
        private async Task<Currencies> ConvertToICollectionViewAsync()
        {

            Currencies tempCurrencies = await (RTModel.GetCurrencies());
            allList = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            allCollectioView = CollectionViewSource.GetDefaultView(tempCurrencies.CurrenciesList);
            allCollectioView.Filter = CurrenciesFilter;

            string[] currenciesCodes = {  "AED", "BTC", "ILS", "EUR", "BRL", "AUD", "BZD", "CAD", "GBP", "CZK", "HKD", "SAR", "SEK", "SGD", "USD" };
            tempCurrencies = await (RTModel.GetCurrencies(currenciesCodes));
            Top15List = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            Top15CollectioView = CollectionViewSource.GetDefaultView(tempCurrencies.CurrenciesList);
            Top15CollectioView.Filter = CurrenciesFilter;

           
            showTop15 = true;
            relativeCurrency = Top15List.FirstOrDefault(t => t.IssuedCountryCode == "USD");


            return tempCurrencies;

        }
        
      
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
//available currencies { "AED",
//             //"AFN",
//             "BTC",
//             //"ALL",
//             //"AMD",
//             //"ARS",
//             "AUD",
//             //"AZN",
//             //"BAM",
//             //"BDT",
//             //"BGN",
//             //"BHD",
//             //"BND",
//             //"BOB",
//             "BRL",
//             //"BYR",
//             "BZD",
//             "CAD",
//             //"CHF",
//             //"CLP",
//             //"CNY",
//             //"COP",
//             //"CRC",
//             //"CSD",
//             "CZK",
//             //"DKK",
//             //"DOP",
//             //"DZD",
//             //"EEK",
//             //"EGP",
//             //"ETB",
//             "EUR",
//             "GBP",
//             //"GEL",
//             //"GTQ",
//             "HKD",
//             //"HNL",
//             //"HRK",
//             //"HUF",
//             //"IDR",
//             "ILS",
//             //"INR",
//             //"IQD",
//             //"IRR",
//             //"ISK",
//             //"JMD",
//             //"JOD",
//             //"JPY",
//             //"KES",
//             //"KGS",
//             //"KHR",
//             //"KRW",
//             //"KWD",
//             //"KZT",
//             //"LAK",
//             //"LBP",
//             //"LKR",
//             //"LTL",
//             //"LVL",
//             //"LYD",
//             //"MAD",
//             //"MKD",
//             //"MNT","MOP","MVR","MXN","MYR","NIO","NOK","NPR","NZD","OMR","PAB","PEN","PHP","PKR","PLN","PYG","QAR","RON","RSD","RUB","RWF",
//             "SAR", "SEK","SGD",
//             //"SYP","THB","TJS","TMT","TND","TRY","TTD","TWD","UAH",
//             "USD",// "UYU","UZS","VEF","VND","XOF","YER","ZAR","ZWL"
//};