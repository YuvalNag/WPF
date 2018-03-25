using DP;
using PL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.ViewModels
{
    public class RTChartVM:BaseVM
    {

        #region Properties

        //history rates wraped in NotifyTaskCompletion class
        private NotifyTaskCompletion<ICollectionView> _taskCurrencies;
        public NotifyTaskCompletion<ICollectionView> taskCurrencies
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

        //string to filter the history rates
        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged();
                if (taskCurrencies != null && taskCurrencies.IsCompleted)
                    taskCurrencies.Result.Refresh();
            }
        }




        private CurrenciesRTModel RTModel { set; get; }

        

        #endregion




        public RTChartVM()
        {

            RTModel = new CurrenciesRTModel();
            taskCurrencies = new NotifyTaskCompletion<ICollectionView>(ConvertToICollectionViewAsync());

        }

        //get in the backround the history rates
        private async Task<ICollectionView> ConvertToICollectionViewAsync()
        {


            string[] currenciescodes = { "AED", "BTC", "ILS", "EUR", "BRL", "AUD", "BZD", "CAD", "GBP", "CZK", "HKD", "SAR", "SEK", "SGD", "USD" };
            Currencies tempCurrencies = await (RTModel.GetCurrencies(currenciescodes));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(tempCurrencies.CurrenciesList);
            collectionView.Filter = CurrenciesFilter;
            return collectionView;


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

//string[] d = { "AED",
//                         //"AFN",
//                         "BTC",
//                         //"ALL",
//                         //"AMD",
//                         //"ARS",
//                         "AUD",
//                         //"AZN",
//                         //"BAM",
//                         //"BDT",
//                         //"BGN",
//                         //"BHD",
//                         //"BND",
//                         //"BOB",
//                         "BRL",
//                         //"BYR",
//                         "BZD",
//                         "CAD",
//                         //"CHF",
//                         //"CLP",
//                         //"CNY",
//                         //"COP",
//                         //"CRC",
//                         //"CSD",
//                         "CZK",
//                         //"DKK",
//                         //"DOP",
//                         //"DZD",
//                         //"EEK",
//                         //"EGP",
//                         //"ETB",
//                         "EUR",
//                         "GBP",
//                         //"GEL",
//                         //"GTQ",
//                         "HKD",
//                         //"HNL",
//                         //"HRK",
//                         //"HUF",
//                         //"IDR",
//                         "ILS",
//                         //"INR",
//                         //"IQD",
//                         //"IRR",
//                         //"ISK",
//                         //"JMD",
//                         //"JOD",
//                         //"JPY",
//                         //"KES",
//                         //"KGS",
//                         //"KHR",
//                         //"KRW",
//                         //"KWD",
//                         //"KZT",
//                         //"LAK",
//                         //"LBP",
//                         //"LKR",
//                         //"LTL",
//                         //"LVL",
//                         //"LYD",
//                         //"MAD",
//                         //"MKD",
//                         //"MNT","MOP","MVR","MXN","MYR","NIO","NOK","NPR","NZD","OMR","PAB","PEN","PHP","PKR","PLN","PYG","QAR","RON","RSD","RUB","RWF",
//                         "SAR", "SEK","SGD",
//                         //"SYP","THB","TJS","TMT","TND","TRY","TTD","TWD","UAH",
//                         "USD",// "UYU","UZS","VEF","VND","XOF","YER","ZAR","ZWL"
//            };