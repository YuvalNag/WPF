using DP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{
    class ConvertValuse : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            Currency source, target;
            source = values[0] as Currency;
            target = values[1] as Currency;


            if (source != null && target != null)
                return (source.Value / target.Value).ToString();
            return source.Value.ToString();



        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}