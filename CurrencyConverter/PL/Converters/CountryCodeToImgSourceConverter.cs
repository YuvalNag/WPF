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

    public class CountryCodeToImgSourceConverter : IMultiValueConverter
    {
       

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] == null)
                return "";
            int index = int.Parse((string)values[0]);

            ObservableCollection<Currency> name = ((values[1] as CategoryAxis).DataContext as CurrenciesListVM).relativeCurrenciesListCollection;
            if (name == null)
                return "";

         
            ImageSourceConverter conv = new ImageSourceConverter();

            //concatenate the values

            string n = name[index].IssuedCountryCode;
            
            bool g = File.Exists(@"/FlagsImages/" + name[index].IssuedCountryCode + ".png");
            //return ImageSource from filename
            BitmapImage bitmapImage= new BitmapImage(new Uri(@"/PL;component/FlagsImages/" + name[index].IssuedCountryCode + ".png", UriKind.RelativeOrAbsolute));
            return bitmapImage;

          //  return @"/PL;component/FlagsImages/" + name[index].IssuedCountryCode + ".png"; ;
           

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
