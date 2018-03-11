using DAL;
using DP;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ChackWindow.xaml
    /// </summary>
    public partial class ChackWindow : Window,INotifyPropertyChanged
    {
        private NotifyTaskCompletion<Currencies> myProperty;

        private DAL_imp dal;

        public NotifyTaskCompletion<Currencies> MyProperty
        {
            set
            {
                myProperty = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyProperty"));
            }
            get
            {
                return myProperty;
            }
        }

        private NotifyTaskCompletion<ObservableCollection<HistoryDTO>> myPropertyH;


        public NotifyTaskCompletion<ObservableCollection<HistoryDTO>> MyPropertyH
        {
            set
            {
                myPropertyH = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyPropertyH"));
            }
            get
            {
                return myPropertyH;
            }
        }

        public ChackWindow()
        {
            InitializeComponent();

            dal = new DAL_imp();
            //MyProperty = new NotifyTaskCompletion<Currencies>(ab2());
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //foreach(Currency c in GetMyProperty().Result.CurrenciesList)
            //{
            //    c.Value *= 0;

            //}
            //GetMyProperty().InvokeTresultChanged();
            MyProperty = new NotifyTaskCompletion<Currencies>(ab());
            MyPropertyH = new NotifyTaskCompletion<ObservableCollection<HistoryDTO>>(abc("ILS"));

        }

        private void ChackWindow_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine(e.Action);
        }

        private async Task<ObservableCollection<HistoryDTO>> abc(string code)
        {
         return new ObservableCollection<HistoryDTO>(await dal.getHRatesAsync(code));
        }


        private async Task<Currencies> ab()
        {

            //Currencies a = await Task.Run(async () => await dAL.RTRatesAsync()); a.CurrenciesList = new ObservableCollection<Currency>(a.CurrenciesList); return a;
            Currencies a = await new DAL_imp().getRTRatesAsync();
            a.CurrenciesList = new ObservableCollection<Currency>(a.CurrenciesList);
            return a;

        }

        private async Task<Currencies> ab2()
        {

            //Currencies a = await Task.Run(async () => await dAL.RTRatesAsync()); a.CurrenciesList = new ObservableCollection<Currency>(a.CurrenciesList); return a;
            Currencies a =await dal.getRTRatesAsync();
            a.CurrenciesList = new ObservableCollection<Currency>(a.CurrenciesList);
            return a;
        }

            private async void dfgdf()
        {
            DAL_imp dAL = new DAL_imp();
            // var co = await dAL.getCountries();
            var co1 = await dAL.getRTRatesAsync();
        //    var co2 = await dAL.getHRates();

           
        }
    }
}
