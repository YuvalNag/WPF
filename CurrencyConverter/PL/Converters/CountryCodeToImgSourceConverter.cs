using DP;
using PL.ViewModels;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PL.Converters
{

    public class CountryCodeToImgSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Currency currency = (value as ChartAdornment).Item as Currency;
            return @"/PL;component/FlagsImages/" + currency.IssuedCountryCode + ".png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        //public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        //{

        //    Currency currency = (values[0] as ChartAdornment).Item as Currency;
        //    // ObservableCollection<Currency> name = ((values[1] as CategoryAxis).DataContext as CurrenciesListVM).relativeCurrenciesListCollection;
        //    float factor;
        //    if (!float.TryParse((string)values[1], out factor))
        //        factor = 1;
        //    currency.Value *= factor;


        //    ImageSourceConverter conv = new ImageSourceConverter();

        //    //concatenate the values



        //    // bool g = File.Exists(@"/FlagsImages/" + name[index].IssuedCountryCode + ".png");
        //    //return ImageSource from filename
        //    BitmapImage bitmapImage = new BitmapImage(new Uri(@"/PL;component/FlagsImages/" + currency.IssuedCountryCode + ".png", UriKind.RelativeOrAbsolute));
        //    return bitmapImage;

        //    //  return @"/PL;component/FlagsImages/" + name[index].IssuedCountryCode + ".png"; ;


        //}

        //public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
