using DP;
using PL.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.ViewModels
{
    public class HistoryVM : BaseVM, IHistoryVM
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
                OnPropertyChanged();

            }
        }

       
        private NotifyTaskCompletion<ObservableCollection<HistoryDTO>> _historyRates;
        public NotifyTaskCompletion<ObservableCollection<HistoryDTO>> historyRates
        {
            set
            {
                _historyRates = value;
                OnPropertyChanged();

            }
            get
            {
                return _historyRates;
            }
        }

        private NotifyTaskCompletion<ObservableCollection<Country>> _countries;
        public NotifyTaskCompletion<ObservableCollection<Country>> countries
        {
            set
            {
                _countries = value;
                OnPropertyChanged();

            }
            get
            {
                return _countries;
            }
        }

        private Models.HIstoryModel hModel { set; get; }


        private Country _baseCountry;
        public Country baseCountry
        {
            get
            {
                return _baseCountry;
            }
            set
            {
                _baseCountry = value;
                historyRates = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(ConvertHistoryToObservableCollection());
                OnPropertyChanged();


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
                historyRates = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(ConvertHistoryToObservableCollection());
                OnPropertyChanged();


            }
        }
        public HistoryVM()
        {
           
            hModel = new Models.HIstoryModel();
           
            countries = new NotifyTaskCompletion<ObservableCollection<Country>>(ConvertCountriesToObservableCollection());
            switchCommand = new SwitchCurrencyCommand(this);
            
        }
        


        private async Task<ObservableCollection<Country>> ConvertCountriesToObservableCollection()
        {
            List<Country> countriesTemp = await  hModel.GetCountries();

            //update the country and relative country
            baseCountry = countriesTemp.Find(t => String.Equals(t.Code, "ILS"));

            //skip the property  
            _raltiveCountry = countriesTemp.Find(t => String.Equals(t.Code,"USD"));
            OnPropertyChanged(nameof(raltiveCountry));



            return new ObservableCollection<Country>(countriesTemp);
        }


        public void SwitchSourceCurrencyAndRelative()
        {
            Country tempRelative = raltiveCountry;
            Country tempsource = baseCountry;
            
            //skip the property 
            _baseCountry = tempRelative;
            raltiveCountry = tempsource;
            OnPropertyChanged(nameof(baseCountry));



        }
        private async Task<ObservableCollection<HistoryDTO>> ConvertHistoryToObservableCollection()
        {
            List<HistoryDTO> tempCurrencies;
            if (raltiveCountry != null)
                   tempCurrencies = await hModel.GetHRates(baseCountry.Code,raltiveCountry.Code);
            else
                tempCurrencies = await hModel.GetHRates(baseCountry.Code);

            return new ObservableCollection<HistoryDTO>(tempCurrencies);
            
        }
    }
}
