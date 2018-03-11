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
    public class ConvertValuse : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            Currency source, target;
            source = values[0] as Currency;
            target = values[1] as Currency;
            String factor;
            factor = values[2] as String;

            if (source != null && target != null)
            {
                double result = Math.Round((source.Value / target.Value), 3);
                double dfactor;
                if (!String.IsNullOrWhiteSpace(factor) && 
                    double.TryParse(factor, out dfactor)&&
                    dfactor>0)
                 
                    return (result * dfactor).ToString();
                
                else
                    return result.ToString();
            }

            if (source != null)
                return Math.Round(source.Value,3).ToString();

            return "0";



        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}