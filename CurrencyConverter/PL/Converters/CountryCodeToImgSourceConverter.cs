using DP;
using PL.ViewModels;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{

    public class CountryCodeToImgSourceConverter : IMultiValueConverter
    {
       

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] == null)
                return "";
            int index = int.Parse((string)values[0]);

            ObservableCollection<Currency> name = ((values[1] as CategoryAxis).DataContext as CurrenciesListVM).currenciesListCollection;
            if (name == null)
                return "";

            string n = name[index].IssuedCountryCode;
            return @"/PL;component/FlagsImages/" +n + ".png";
           

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
