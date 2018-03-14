
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
    class CurrenciesListVM : INotifyPropertyChanged, ICurrenciesListVM
    {



        #region Properties

        private int _selectedUC;
        public int selectedUC
        {
            set
            {
                _selectedUC = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("selectedUC"));
            }
            get
            {
                return _selectedUC;
            }
        }

        private ObservableCollection<UserControl> _UC;
        public ObservableCollection<UserControl> UC
        {
            get
            {
                return _UC;
            }
            set
            {
                _UC = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UC"));
            }
        }
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
        private bool _showTop12;
        public bool showTop12
        {
            get
            {
                return _showTop12;
            }
            set
            {

                _showTop12 = value;
              
                    if (_showTop12)
                    {

                        currenciesList = Top12CollectioView;
                        FilterString = _filterString;
                    
                        currenciesListCollection = Top12List;

                    }
                    else
                    {

                        currenciesList = allCollectioView;
                        FilterString = _filterString;
                        currenciesListCollection = allList;

                    }
               
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("showTop12"));

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

        private ObservableCollection<Currency> _currenciesListCollection;
        public ObservableCollection<Currency> currenciesListCollection
        {
            get
            {
                return _currenciesListCollection;
            }
            set
            {
                _currenciesListCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("currenciesListCollection"));

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
            UC = new ObservableCollection<UserControl>();
            UC.Add(new ColumnsChartUS());

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
                         //"CZK",
                         //"DKK",
                         //"DOP",
                         //"DZD",
                         //"EEK",
                         //"EGP",
                         //"ETB",
                         //"EUR",
                         //"GBP",
                         //"GEL",
                         //"GTQ",
                         //"HKD",
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
                         //"MNT",
                         //"MOP",
                         //"MVR",
                         //"MXN",
                         //"MYR",
                         //"NIO",
                         //"NOK",
                         //"NPR",
                         //"NZD",
                         //"OMR",
                         //"PAB",
                         //"PEN",
                         //"PHP",
                         //"PKR",
                         //"PLN",
                         //"PYG",
                         //"QAR",
                         //"RON",
                         //"RSD",
                         //"RUB",
                         //"RWF",
                         "SAR",
                         "SEK",
                         "SGD",
                         //"SYP",
                         //"THB",
                         //"TJS",
                         //"TMT",
                         //"TND",
                         //"TRY",
                         //"TTD",
                         //"TWD",
                         //"UAH",
                         "USD",
                //"UYU",
                //"UZS",
                //"VEF",
                //"VND",
                //"XOF",
                //"YER",
                //"ZAR",
                //"ZWL"
            };
            Currencies tempCurrencies = await (rTModel.GetCurrencies());

            Top12List = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList.Where(t => d.Contains(t.IssuedCountryCode)));
            Top12CollectioView=CollectionViewSource.GetDefaultView( new ObservableCollection<Currency>(Top12List));
            Top12CollectioView.Filter = CurrenciesFilter;

            allList = new ObservableCollection<Currency>(tempCurrencies.CurrenciesList);
            allCollectioView = CollectionViewSource.GetDefaultView(new ObservableCollection<Currency>(allList));
            allCollectioView.Filter = CurrenciesFilter;


            showTop12 = true;
           
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
            return currency.IssuedCountryName.Contains(FilterString);
        }

        public void SwitchRTListAndRTChart()
        {
         if (selectedUC == 0)
            {
                if (UC.Count == 1)
                    UC.Add(new ColumnsChartUS());
                selectedUC = 1;
            }
            else
                selectedUC = 0;

        }
    }
}
