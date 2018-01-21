﻿using DP;
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

        public async Task<List<Country>> getCountriesAsync()
        {
            List<Country> Countries;
            using (DB_Context context = new DB_Context())
            {

                Countries = await context.Countries.ToListAsync();


            }
            if (Countries.Any())
                return Countries;
            else
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
        //public async Task<Currencies> getRTRates()
        //{
        //    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
        //    Currencies DbRates;
        //    using (DB_Context context = new DB_Context())
        //    {

        //        DbRates = await context.CurrenciesByDate.OrderBy(t => t.date).FirstOrDefaultAsync();
        //        if (DbRates != null)
        //        {
        //            List<Currency> list = (List<Currency>)DbRates.CurrenciesList;

        //        }



        //    }

        //    if (DbRates != null && DbRates.date.ToUniversalTime().AddHours(1) > DateTime.UtcNow)
        //    {

        //        return DbRates;
        //    }

        //    else
        //    {
        //        var RTRates = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live");
        //        if (RTRates.Success == true)
        //            return await Task.Run<Currencies>(async () => await ConvertToCurrencies(RTRates, DbRates));
        //        else return DbRates;
        //    }

        //}

        //private async Task<Currencies> ConvertToCurrencies(LiveModel rTRates, Currencies oldCurrencies)
        //{

        //    List<Country> countries = await getCountries();
        //    List<Currency> CurrenciesList = new List<Currency>();

        //    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    dateTime = dateTime.AddSeconds(rTRates.Timestamp);

        //    if (oldCurrencies != null)
        //    {
        //        Dictionary<string, float> dictionaryCurrencies = oldCurrencies.CurrenciesList.ToDictionary(key => key.IssuesCountry.Code, value => value.Value);
        //        foreach (var qoute in rTRates.quotes)
        //        {
        //            Country issuesCountry = countries.Find(t => t.Code == qoute.Key.Substring(3));
        //            Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, };
        //            addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
        //            CurrenciesList.Add(newCurrency);
        //        }
        //        using (DB_Context context = new DB_Context())
        //        {

        //            Currencies newCurrencies = context.CurrenciesByDate.Find(oldCurrencies.id);
        //            newCurrencies.CurrenciesList = CurrenciesList;
        //            newCurrencies.date = dateTime;
        //            context.SaveChanges();
        //            return newCurrencies;
        //        }


        //    }
        //    else
        //    {
        //        foreach (var qoute in rTRates.quotes)
        //        {
        //            Country issuesCountry = countries.Find(t => t.Code == qoute.Key.Substring(3));
        //            Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, Direction = '+', Magnitude = 0 };
        //            CurrenciesList.Add(newCurrency);
        //        }

        //        using (DB_Context context = new DB_Context())
        //        {

        //            Currencies newCurrencies = new Currencies();
        //            newCurrencies.CurrenciesList = CurrenciesList;
        //            newCurrencies.date = dateTime;
        //            context.CurrenciesByDate.Add(newCurrencies);
        //            context.SaveChanges();
        //            return newCurrencies;
        //        }
        //    }
        //}
        //public async Task<Currencies> getRTRatesRefactor()
        //{
        //    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
        //    Currencies DbRates;
        //    using (DB_Context context = new DB_Context())
        //    {

        //        DbRates = await context.CurrenciesByDate.OrderBy(t => t.date).FirstOrDefaultAsync();
        //        if (DbRates != null)
        //        {


        //        }



        //    }

        //    if (DbRates != null && DbRates.date.ToUniversalTime().AddHours(1) > DateTime.UtcNow)
        //    {

        //        return DbRates;
        //    }

        //    else
        //    {
        //        var RTRates = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live");
        //        if (RTRates.Success == true)
        //            return await Task.Run<Currencies>(async () => await ConvertCodeToName(RTRates, DbRates));
        //        else return DbRates;
        //    }

        //}

        //private async Task<Currencies> ConvertToCodeToName(LiveModel rTRates, Currencies oldCurrencies)
        //{

        //    List<Country> countries = await getCountries();
        //    List<Currency> CurrenciesList = new List<Currency>();

        //    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    dateTime = dateTime.AddSeconds(rTRates.Timestamp);

        //    if (oldCurrencies != null)
        //    {
        //        Dictionary<string, float> dictionaryCurrencies = oldCurrencies.CurrenciesList.ToDictionary(key => key.IssuesCountry.Code, value => value.Value);
        //        foreach (var qoute in rTRates.quotes)
        //        {
        //            Country issuesCountry = countries.Find(t => t.Code == qoute.Key.Substring(3));
        //            Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, };
        //            addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
        //            CurrenciesList.Add(newCurrency);
        //        }
        //        using (DB_Context context = new DB_Context())
        //        {

        //            Currencies newCurrencies = context.CurrenciesByDate.Find(oldCurrencies.id);
        //            newCurrencies.CurrenciesList = CurrenciesList;
        //            newCurrencies.date = dateTime;
        //            context.SaveChanges();
        //            return newCurrencies;
        //        }


        //    }
        //    else
        //    {
        //        foreach (var qoute in rTRates.quotes)
        //        {
        //            Country issuesCountry = countries.Find(t => t.Code == qoute.Key.Substring(3));
        //            Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, Direction = '+', Magnitude = 0 };
        //            CurrenciesList.Add(newCurrency);
        //        }

        //        using (DB_Context context = new DB_Context())
        //        {

        //            Currencies newCurrencies = new Currencies();
        //            newCurrencies.CurrenciesList = CurrenciesList;
        //            newCurrencies.date = dateTime;
        //            context.CurrenciesByDate.Add(newCurrencies);
        //            context.SaveChanges();
        //            return newCurrencies;
        //        }
        //    }
        //}
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
                currency.Magnitude =(float) Math.Round(-(newValue - oldValue),2);
            }
        }
        public async Task<List<HistoryDTO>> getHRatesAsync(string code)
        {
            List<HistoryDTO> historyRates;
            Currencies oldHistoryCurrencies;
            DateTime lastHistoryDate;

            using (DB_Context context = new DB_Context())
            {
                oldHistoryCurrencies = await context.CurrenciesByDate.OrderBy(t => t.date).FirstOrDefaultAsync();

                if(oldHistoryCurrencies == null)
                {
                    lastHistoryDate = DateTime.Now.AddDays(-1);
                    oldHistoryCurrencies= await getRTRatesAsync();
                }
                lastHistoryDate = oldHistoryCurrencies.date;
               
                if (lastHistoryDate.AddYears(1) > DateTime.Now.AddDays(1))
                {
                    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                    List<Country> Countries = await getCountriesAsync().ConfigureAwait(false);
                   
                    for (DateTime start = lastHistoryDate; start.AddYears(1) > DateTime.Now;start= start.AddDays(-1))
                    {
                        var HRate = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", new Dictionary<string, string> { { "date", start.ToString("yyyy-MM-dd") } }).ConfigureAwait(false);
                        Currencies currencies = new Currencies();
                        currencies.date = start;
                        currencies.CurrenciesList = ConverterHRate(Countries, HRate, oldHistoryCurrencies);
                        context.CurrenciesByDate.Add(currencies);
                    }
                    await context.SaveChangesAsync();
                }
                 historyRates= await context.CurrenciesByDate.OrderBy(t => t.date).Select(t=>new HistoryDTO() {Currency=t.CurrenciesList.FirstOrDefault(x=>x.IssuedCountryCode==code),date=t.date}).ToListAsync();
                 
            }
           
            return historyRates;

        }

        private List<Currency> ConverterHRate(List<Country> Countries, HistoryModel RTRates, Currencies DbRates)
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

        public async Task<Currencies> getRTRatesAsync()
        {
            DB_Context context = new DB_Context();
                Currencies DbRates = await context.CurrenciesByDate.OrderByDescending(t => t.date).Include("CurrenciesList").FirstOrDefaultAsync().ConfigureAwait(false);

            

                if (DbRates != null && isUpdatedInLastHours(DbRates.date))
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


                    if (RTRates == null)
                    {
                        if (DbRates == null)
                            throw new Exception("error");

                        context.Dispose();
                        return DbRates;
                    }


                    if (DbRates == null)
                    {

                        CurrenciesList = FirstEntryConverter(Countries, RTRates);



                    }



                    else
                    {

                        CurrenciesList = Converter(Countries, RTRates, DbRates);

                        if (checkIfInTheSameDay(DbRates.date.ToLocalTime()))
                            context.CurrenciesByDate.Remove(DbRates);


                    }

                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    dateTime = dateTime.AddSeconds(RTRates.Timestamp).ToLocalTime();

                    Currencies newCurrencies = new Currencies();
                    newCurrencies.CurrenciesList = CurrenciesList;
                    newCurrencies.date = dateTime;
                    context.CurrenciesByDate.Add(newCurrencies);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                    context.Dispose();
                    return newCurrencies;
                }



            

        }

       

        private bool isUpdatedInLastHours(DateTime start)
        {
           
            start = start.ToLocalTime();
            DateTime time = DateTime.Now;
            return start.AddHours(1) > DateTime.Now && checkIfInTheSameDay(start);
        }

        private bool checkIfInTheSameDay(DateTime start)
        {
          return  start.DayOfYear == DateTime.Now.DayOfYear &&
            start.Year == DateTime.Now.Year;
        }
        private  List<Currency> FirstEntryConverter(List<Country> Countries, LiveModel RTRates)
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


        private  List<Currency> Converter(List<Country> Countries, LiveModel RTRates, Currencies DbRates)
        {
            List<Currency> CurrenciesList = new List<Currency>();
            Dictionary<string, float> dictionaryCurrencies = DbRates.CurrenciesList.ToDictionary(key => key.IssuedCountryCode, value => value.Value);
            foreach (var qoute in RTRates.quotes)
            {
                string issuesCountryName = Countries.Find(t => t.Code == qoute.Key.Substring(3)).Name;
                Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuedCountryCode = qoute.Key.Substring(3), IssuedCountryName = issuesCountryName};
                addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
                CurrenciesList.Add(newCurrency);
            }
            return CurrenciesList;
        }
    }

}
