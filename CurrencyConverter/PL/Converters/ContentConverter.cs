using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{
    public class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //string source = parameter as string;
            //if (String.Equals(source, "content"))
            {
                int index = (int)value;
                if (index == 0)
                    return "RT";
                else
                    return "History";
            }
            //else
            //{
            //    int index = (int)value;
            //    if (index == 1)
            //        return "Left";
            //    else
            //        return "Right";
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
