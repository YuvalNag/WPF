using DataProtocol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Targil4
{
    class EnumConverter : IValueConverter
    {
        //ItemsSource="{Binding Source={x:Type DP:EDUCUATION},Converter={StaticResource enumConverter}}"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetValues(typeof(EDUCUATION)).Cast<EDUCUATION>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
