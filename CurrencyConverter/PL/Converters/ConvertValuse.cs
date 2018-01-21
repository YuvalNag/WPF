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
                return Math.Round((source.Value / target.Value),3).ToString();
            return Math.Round(source.Value,3).ToString();



        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}