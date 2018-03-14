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
            //String factor;
            //factor = values[2] as String;

            double dfactor = values[2]==null?0:(double)values[2];
            if (source != null && target != null)
            {
                double result= (source.Value / target.Value);
                if(result<0.0000001)
                    result = Math.Round(result, 10);
                else
                    result = Math.Round(result, 5);
                //if (!(source.IssuedCountryCode == "BTC"))
                //    result= Math.Round((source.Value / target.Value), 3);
                //else
                // result = Math.Round((source.Value / target.Value),10);

                //if (!String.IsNullOrWhiteSpace(factor) && 
                //    double.TryParse(factor, out dfactor)&&
                //    dfactor>0)

                if (dfactor> 0)
                    return (result * (double)values[2]).ToString();
                
                else
                    return result.ToString();
            }

            if (source != null)
                if (source.Value < 0.0000001)
                    return Math.Round(source.Value, 7);
                else return source.Value;

            return "0";



        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}