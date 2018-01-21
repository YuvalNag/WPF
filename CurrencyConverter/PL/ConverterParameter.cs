using DP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    class ConverterParameter : IMultiValueConverter
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

    //public static class CurrencyCodeMapper
    //{
    //    private static readonly Dictionary<string, string> SymbolsByCode;

    //    public static string GetSymbol(string code)
    //    {
    //        if (SymbolsByCode.ContainsKey(code))
    //        {
    //            return SymbolsByCode[code];
    //        }
    //        else
    //        {

    //            return String.Format("({0}) ", code);
    //        }
    //    }

    //    static CurrencyCodeMapper()
    //    {
    //        SymbolsByCode = new Dictionary<string, string>();

    //        var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
    //                      .Select(x => new RegionInfo(x.LCID));

    //        foreach (var region in regions)
    //            if (!SymbolsByCode.ContainsKey(region.ISOCurrencySymbol))
    //                SymbolsByCode.Add(region.ISOCurrencySymbol, region.CurrencySymbol);
    //    }
    }


