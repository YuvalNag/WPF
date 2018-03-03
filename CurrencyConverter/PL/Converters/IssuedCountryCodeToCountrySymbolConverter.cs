using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace PL.Converters
{
    class IssuedCountryCodeToCountrySymbolConverter : IValueConverter
    {
        public  Dictionary<string, string> Currencies = new Dictionary<string, string>() {
                                                    {"AED", "د.إ.‏"},
                                                    {"AFN", "؋ "},
                                                    {"BTC","฿" },
                                                    {"ALL", "Lek"},
                                                    {"AMD", "դր."},
                                                    {"ARS", "$"},
                                                    {"AUD", "$"},
                                                    {"AZN", "man."},
                                                    {"BAM", "KM"},
                                                    {"BDT", "৳"},
                                                    {"BGN", "лв."},
                                                    {"BHD", "د.ب.‏ "},
                                                    {"BND", "$"},
                                                    {"BOB", "$b"},
                                                    {"BRL", "R$"},
                                                    {"BYR", "р."},
                                                    {"BZD", "BZ$"},
                                                    {"CAD", "$"},
                                                    {"CHF", "fr."},
                                                    {"CLP", "$"},
                                                    {"CNY", "¥"},
                                                    {"COP", "$"},
                                                    {"CRC", "₡"},
                                                    {"CSD", "Din."},
                                                    {"CZK", "Kč"},
                                                    {"DKK", "kr."},
                                                    {"DOP", "RD$"},
                                                    {"DZD", "DZD"},
                                                    {"EEK", "kr"},
                                                    {"EGP", "ج.م.‏ "},
                                                    {"ETB", "ETB"},
                                                    {"EUR", "€"},
                                                    {"GBP", "£"},
                                                    {"GEL", "Lari"},
                                                    {"GTQ", "Q"},
                                                    {"HKD", "HK$"},
                                                    {"HNL", "L."},
                                                    {"HRK", "kn"},
                                                    {"HUF", "Ft"},
                                                    {"IDR", "Rp"},
                                                    {"ILS", "₪"},
                                                    {"INR", "रु"},
                                                    {"IQD", "د.ع.‏ "},
                                                    {"IRR", "ريال "},
                                                    {"ISK", "kr."},
                                                    {"JMD", "J$"},
                                                    {"JOD", "د.ا.‏ "},
                                                    {"JPY", "¥"},
                                                    {"KES", "S"},
                                                    {"KGS", "сом"},
                                                    {"KHR", "៛"},
                                                    {"KRW", "₩"},
                                                    {"KWD", "د.ك.‏ "},
                                                    {"KZT", "Т"},
                                                    {"LAK", "₭"},
                                                    {"LBP", "ل.ل.‏ "},
                                                    {"LKR", "රු."},
                                                    {"LTL", "Lt"},
                                                    {"LVL", "Ls"},
                                                    {"LYD", "د.ل.‏ "},
                                                    {"MAD", "د.م.‏ "},
                                                    {"MKD", "ден."},
                                                    {"MNT", "₮"},
                                                    {"MOP", "MOP"},
                                                    {"MVR", "ރ."},
                                                    {"MXN", "$"},
                                                    {"MYR", "RM"},
                                                    {"NIO", "N"},
                                                    {"NOK", "kr"},
                                                    {"NPR", "रु"},
                                                    {"NZD", "$"},
                                                    {"OMR", "ر.ع.‏ "},
                                                    {"PAB", "B/."},
                                                    {"PEN", "S/."},
                                                    {"PHP", "PhP"},
                                                    {"PKR", "Rs"},
                                                    {"PLN", "zł"},
                                                    {"PYG", "Gs"},
                                                    {"QAR", "ر.ق.‏ "},
                                                    {"RON", "lei"},
                                                    {"RSD", "Din."},
                                                    {"RUB", "р."},
                                                    {"RWF", "RWF"},
                                                    {"SAR", "ر.س.‏ "},
                                                    {"SEK", "kr"},
                                                    {"SGD", "$"},
                                                    {"SYP", "ل.س.‏ "},
                                                    {"THB", "฿"},
                                                    {"TJS", "т.р."},
                                                    {"TMT", "m."},
                                                    {"TND", "د.ت.‏ "},
                                                    {"TRY", "TL"},
                                                    {"TTD", "TT$"},
                                                    {"TWD", "NT$"},
                                                    {"UAH", "₴"},
                                                    {"USD", "$"},
                                                    {"UYU", "$U"},
                                                    {"UZS", "so'm"},
                                                    {"VEF", "Bs. F."},
                                                    {"VND", "₫"},
                                                    {"XOF", "XOF"},
                                                    {"YER", "ر.ي.‏ "},
                                                    {"ZAR", "R"},
                                                    {"ZWL", "Z$"} };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //RegionInfo myRI1 = new RegionInfo(value as string);
            //return myRI1.ISOCurrencySymbol;
            if (Currencies.ContainsKey((string)value))
                return Currencies[(string)value];
            else
                return (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
