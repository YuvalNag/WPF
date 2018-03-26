using DP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Converters
{
    public class ConvertValuse : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            Currency source, target;
            source = values[0] as Currency;
            target = values[1] as Currency;
           

            double dfactor = (values[2]==null) ? 1:(double)values[2];

            if (source != null && target != null)
            {
                double result= (source.Value / target.Value);

                if (dfactor> 1)
                    result= (result * dfactor);

                if (result < 0.0000001)
                    result = Math.Round(result, 10);
                else
                    result = Math.Round(result, 5);

                return result.ToString();
            }

            if (source != null)
                if (source.Value < 0.0000001)
                    return Math.Round(source.Value, 7);
                else
                    return source.Value;

            return "0";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}