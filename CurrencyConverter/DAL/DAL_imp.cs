using DP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CurrencyLayerDotNet.Models;

namespace DAL
{
    public class DAL_imp : IDAL
    {

        #region Countries region
        //get supported countries countries
        public async Task<List<Country>> getCountriesAsync()
        {
            List<Country> Countries;
            using (DB_Context context = new DB_Context())
            {

                Countries = await context.Countries.ToListAsync();


            }

            //check if the countries exists in the local DB
            if (Countries.Any())
                return Countries;
            else //download and save countries 
            {
                var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                var CountriesList = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list");
                if (CountriesList.Success == true)
                {
                    Countries = CountriesList.quotes.Select(t => new Country() { Code = t.Key, Name = t.Value }).ToList();


                    using (DB_Context context = new DB_Context())
                    {

                        context.Countries.AddRange(Countries);
                        await context.SaveChangesAsync();

                    }
                    return Countries;

                }
                else
                    return null;
            }

        } 
        #endregion

        #region History
        //get historical rates
        public async Task<List<HistoryDTO>> getHRatesAsync(string code)
        {
            List<HistoryDTO> historyRates;
            Currencies lastHistory;
            Currencies firstHistory;

            DateTime lastHistoryDate;
            DateTime firstHistoryDate;


            using (DB_Context context = new DB_Context())
            {

                //file the gap from now to the first date we have
                firstHistory = await context.CurrenciesByDate.OrderByDescending(t => t.date).FirstOrDefaultAsync();
                firstHistoryDate = firstHistory.date;

                if (firstHistory != null && !checkIfInTheSameDay(firstHistory.date))
                {
                    await getRTRatesAsync();
                    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                    List<Country> Countries = await getCountriesAsync().ConfigureAwait(false);

                    for (DateTime start = firstHistoryDate; DateTime.Now.AddDays(-1) > start; start = start.AddDays(1))
                    {
                        var HRate = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", new Dictionary<string, string> { { "date", start.ToString("yyyy-MM-dd") } }).ConfigureAwait(false);
                        Currencies currencies = new Currencies();
                        currencies.date = start;
                        currencies.CurrenciesList = ConverterHRate(Countries, HRate, firstHistory);
                        firstHistory = currencies;
                        context.CurrenciesByDate.Add(currencies);
                    }

                }

                //fil the gap from the last date we have to a year
                //gets the last date from DB
                lastHistory = await context.CurrenciesByDate.OrderBy(t => t.date).FirstOrDefaultAsync();

                //if no currecies in the DB so make the first on to be the real time one
                if (lastHistory == null)
                {
                    //lastHistoryDate = DateTime.Now.AddDays(-1);
                    lastHistory = await getRTRatesAsync().ConfigureAwait(false);
                }


                lastHistoryDate = lastHistory.date;

                //fil the gap from the last date we have to a year
                if (lastHistoryDate.AddYears(1) > DateTime.Now.AddDays(1))
                {
                    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                    List<Country> Countries = await getCountriesAsync().ConfigureAwait(false);

                    for (DateTime start = lastHistoryDate; start.AddYears(1) > DateTime.Now; start = start.AddDays(-1))
                    {
                        var HRate = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", new Dictionary<string, string> { { "date", start.ToString("yyyy-MM-dd") } }).ConfigureAwait(false);
                        Currencies currencies = new Currencies();
                        currencies.date = start;
                        currencies.CurrenciesList = ConverterHRate(Countries, HRate, lastHistory);
                        lastHistory = currencies;
                        context.CurrenciesByDate.Add(currencies);
                    }

                }
                //save changes
                await context.SaveChangesAsync();

                //get the historical dates
                historyRates = await context.CurrenciesByDate.OrderBy(t => t.date).Select(t => new HistoryDTO() { Currency = t.CurrenciesList.FirstOrDefault(x => x.IssuedCountryCode == code), date = t.date }).ToListAsync();

            }

            return historyRates;

        }

        //convert from "HistoryModel" to "Currencies" and colculate the magnitude , country 
        private List<Currency> ConverterHRate(List<Country> Countries, HistoryModel HTRates, Currencies olbRates)
        {
            List<Currency> CurrenciesList = new List<Currency>();
            Dictionary<string, float> dictionaryCurrencies = olbRates.CurrenciesList.ToDictionary(key => key.IssuedCountryCode, value => value.Value);
            foreach (var qoute in HTRates.quotes)
            {
                string issuesCountryName = Countries.Find(t => t.Code == qoute.Key.Substring(3)).Name;
                Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuedCountryCode = qoute.Key.Substring(3), IssuedCountryName = issuesCountryName };
                addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
                CurrenciesList.Add(newCurrency);
            }
            return CurrenciesList;
        }

        #endregion

        #region Real Time Region
        //get Real time rates
        public async Task<Currencies> getRTRatesAsync()
        {

            DB_Context context = new DB_Context();

            Currencies DbRates = await context.CurrenciesByDate.OrderByDescending(t => t.date)
                                               .Include("CurrenciesList")//prevent "lazy" loading
                                               .FirstOrDefaultAsync()
                                               .ConfigureAwait(false);



            //if the rates in the DB are updated
            if (DbRates != null && isUpdatedInLastHour(DbRates.date))
            {
                context.Dispose();
                return DbRates;
            }
            else
            {

                var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                List<Country> Countries = await getCountriesAsync().ConfigureAwait(false);
                List<Currency> CurrenciesList;
                var RTRates = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live").ConfigureAwait(false);

                //error in gettings data from the web
                if (RTRates == null)
                {
                    if (DbRates == null)
                        throw new Exception("error");

                    context.Dispose();
                    return DbRates;
                }

                //if the db is empty 
                if (DbRates == null)
                {
                    CurrenciesList = FirstEntryConverter(Countries, RTRates);
                }
                //the db is not empty
                else
                {
                    CurrenciesList = Converter(Countries, RTRates, DbRates);

                    //we save one rate per day there for delete the one thats not updated
                    if (checkIfInTheSameDay(DbRates.date))
                        context.CurrenciesByDate.Remove(DbRates);
                }

                //convert UNIX time to normal time
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dateTime = dateTime.AddSeconds(RTRates.Timestamp).ToLocalTime();

                //create the currecy oblect to return
                Currencies newCurrencies = new Currencies();
                newCurrencies.CurrenciesList = CurrenciesList;
                newCurrencies.date = dateTime;
                context.CurrenciesByDate.Add(newCurrencies);
                await context.SaveChangesAsync().ConfigureAwait(false);


                context.Dispose();
                return newCurrencies;
            }
        }


        private void addDirectionAndMagnitude(KeyValuePair<string, string> qoute, Dictionary<string, float> currencies, Currency currency)
        {
            float newValue = float.Parse(qoute.Value);
            var oldValue = currencies[qoute.Key.Substring(3)];
            if (newValue >= oldValue)
            {
                currency.Direction = "+";
                currency.Magnitude = (float)Math.Round((newValue - oldValue), 2);
            }
            else
            {
                currency.Direction = "-";
                currency.Magnitude = (float)Math.Round(-(newValue - oldValue), 2);
            }
        }

        //help func to check if a date is in the past hour
        private bool isUpdatedInLastHour(DateTime start)
        {

            DateTime time = DateTime.Now.ToLocalTime();
            return start.AddHours(1) > DateTime.Now && checkIfInTheSameDay(start);
        }

        //check if a given date is in the current day
        private bool checkIfInTheSameDay(DateTime start)
        {
            return start.DayOfYear == DateTime.Now.DayOfYear &&
              start.Year == DateTime.Now.Year;
        }

        //convert from "RTModel" to "Currencies" for the first entry (ignor magnitude) 
        private List<Currency> FirstEntryConverter(List<Country> Countries, LiveModel RTRates)
        {
            List<Currency> CurrenciesList = new List<Currency>();
            foreach (var qoute in RTRates.quotes)
            {
                string issuesCountryName = Countries.Find(t => t.Code == qoute.Key.Substring(3)).Name;
                Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuedCountryCode = qoute.Key.Substring(3), IssuedCountryName = issuesCountryName, Direction = "+", Magnitude = 0 };
                CurrenciesList.Add(newCurrency);
            }
            return CurrenciesList;
        }

        //convert from "RTModel" to "Currencies" and colculate the magnitude , country 
        private List<Currency> Converter(List<Country> Countries, LiveModel RTRates, Currencies DbRates)
        {
            List<Currency> CurrenciesList = new List<Currency>();
            Dictionary<string, float> dictionaryCurrencies = DbRates.CurrenciesList.ToDictionary(key => key.IssuedCountryCode, value => value.Value);
            foreach (var qoute in RTRates.quotes)
            {
                string issuesCountryName = Countries.Find(t => t.Code == qoute.Key.Substring(3)).Name;
                Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuedCountryCode = qoute.Key.Substring(3), IssuedCountryName = issuesCountryName };
                addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
                CurrenciesList.Add(newCurrency);
            }
            return CurrenciesList;
        } 
        #endregion
    }

}
