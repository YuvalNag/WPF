using DP;
using PL.ViewModels;
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
    public class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<HistoryDTO> currencies = (values[2] as ObservableCollection<HistoryDTO>);
            HistoryDTO source;
            HistoryDTO target;
            Country state=(values[0] as Country);
            Country raltive = (values[1] as Country);

            source = currencies.FirstOrDefault(t => String.Equals(t.Currency.IssuedCountryCode, state.Code));
            target = currencies.FirstOrDefault(t => String.Equals(t.Currency.IssuedCountryCode, raltive.Code));


            if (source != null && target != null)
            {
                double result = Math.Round((source.Currency.Value / target.Currency.Value), 3);
                return result.ToString();
            }

            if (source != null)
                return Math.Round(source.Currency.Value, 3).ToString();
            return "0";

            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
