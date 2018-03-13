﻿using DP;
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
    public class MinMaxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<HistoryDTO> collection = (value as ObservableCollection<HistoryDTO>);
            string kind = parameter as string;
            double min=0;
            double max=5;
            double interval = 0.1;
            if (collection != null)
            {
                min = collection.Min(t => t.Currency.Value);
                max = collection.Max(t => t.Currency.Value);
                double average = collection.Average(t => t.Currency.Value);
                double sumOfSquaresOfDifferences = collection.Select(val => (val.Currency.Value - average) * (val.Currency.Value - average)).Sum();
                interval= Math.Sqrt(sumOfSquaresOfDifferences / collection.Count);
                max = interval==0?2: max + 3 * interval;
                min -= interval==0?1:interval;
            }
            
            switch (kind)
            {
                case "min":
                    {
                        return min;
                        
                    }
                case "max":
                    {
                        return max;
                       
                    }
                case "interval":
                    {
                        return interval;
                        
                    }
                default:
                    return 6;
                   
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
