using DP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CurrencyLayerDotNet.Models;

namespace DAL
{
    public class DAL_imp
    {

        public async Task<List<Country>> getCountries()
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
        public async Task<Currencies> getRTRates()
        {
            var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
            Currencies DbRates;
            using (DB_Context context = new DB_Context())
            {

                DbRates = await context.CurrenciesByDate.OrderBy(t => t.date).FirstOrDefaultAsync();
                if (DbRates != null)
                {
                    List<Currency> list = (List<Currency>)DbRates.CurrenciesList;

                }



            }

            if (DbRates != null && DbRates.date.ToUniversalTime().AddHours(1) > DateTime.UtcNow)
            {

                return DbRates;
            }

            else
            {
                var RTRates = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live");
                if (RTRates.Success == true)
                    return await Task.Run<Currencies>(async () => await ConvertToCurrencies(RTRates, DbRates));
                else return DbRates;
            }

        }

        private async Task<Currencies> ConvertToCurrencies(LiveModel rTRates, Currencies oldCurrencies)
        {

            List<Country> countries = await getCountries();
            List<Currency> CurrenciesList = new List<Currency>();

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(rTRates.Timestamp);

            if (oldCurrencies != null)
            {
                Dictionary<string, float> dictionaryCurrencies = oldCurrencies.CurrenciesList.ToDictionary(key => key.IssuesCountry.Code, value => value.Value);
                foreach (var qoute in rTRates.quotes)
                {
                    Country issuesCountry = countries.Find(t => t.Code == qoute.Key.Substring(3));
                    Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, };
                    addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
                    CurrenciesList.Add(newCurrency);
                }
                using (DB_Context context = new DB_Context())
                {

                    Currencies newCurrencies = context.CurrenciesByDate.Find(oldCurrencies.id);
                    newCurrencies.CurrenciesList = CurrenciesList;
                    newCurrencies.date = dateTime;
                    context.SaveChanges();
                    return newCurrencies;
                }


            }
            else
            {
                foreach (var qoute in rTRates.quotes)
                {
                    Country issuesCountry = countries.Find(t => t.Code == qoute.Key.Substring(3));
                    Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, Direction = '+', Magnitude = 0 };
                    CurrenciesList.Add(newCurrency);
                }

                using (DB_Context context = new DB_Context())
                {

                    Currencies newCurrencies = new Currencies();
                    newCurrencies.CurrenciesList = CurrenciesList;
                    newCurrencies.date = dateTime;
                    context.CurrenciesByDate.Add(newCurrencies);
                    context.SaveChanges();
                    return newCurrencies;
                }
            }
        }

        private void addDirectionAndMagnitude(KeyValuePair<string, string> qoute, Dictionary<string, float> currencies, Currency currency)
        {
            float newValue = float.Parse(qoute.Value);
            var oldValue = currencies[qoute.Key.Substring(3)];
            if (newValue > oldValue)
            {
                currency.Direction = '+';
                currency.Magnitude = newValue - oldValue;
            }
            else
            {
                currency.Direction = '-';
                currency.Magnitude = -(newValue - oldValue);
            }
        }
        public async Task<List<Currencies>> getHRates()
        {
            using (DB_Context context = new DB_Context())
            {
                return await context.CurrenciesByDate.OrderBy(t => t.date).ToListAsync();
            }

        }

        public async Task<Currencies> RTtry()
        {
            var instance = new CurrencyLayerDotNet.CurrencyLayerApi();

            using (DB_Context context = new DB_Context())
            {
                List<Country> Countries;


                #region Countries
                Countries = await context.Countries.ToListAsync();



                if (!Countries.Any())
                {
                    var CountriesList = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list");
                    if (CountriesList.Success == true)
                    {
                        Countries = CountriesList.quotes.Select(t => new Country() { Code = t.Key, Name = t.Value }).ToList();


                        
                        context.Countries.AddRange(Countries);




                    }
                }

                    #endregion

                    Currencies DbRates = await context.CurrenciesByDate.OrderBy(t => t.date).FirstOrDefaultAsync();

                    

                    if (DbRates != null && DbRates.date.ToUniversalTime().AddHours(1) > DateTime.UtcNow)
                    {

                        return DbRates;
                    }

                    else if(DbRates == null)
                    {
                        List<Currency> CurrenciesList = new List<Currency>();
                        var RTRates = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live");
                        foreach (var qoute in RTRates.quotes)
                        {
                            Country issuesCountry =await context.Countries.Where(t => t.Code == qoute.Key.Substring(3)).FirstOrDefaultAsync();
                            Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, Direction = '+', Magnitude = 0 };
                            CurrenciesList.Add(newCurrency);
                        }

                        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        dateTime = dateTime.AddSeconds(RTRates.Timestamp);

                            Currencies newCurrencies = new Currencies();
                            newCurrencies.CurrenciesList = CurrenciesList;
                            newCurrencies.date = dateTime;
                            context.CurrenciesByDate.Add(newCurrencies);
                            context.SaveChanges();
                            return newCurrencies;
                        
                    }



                    else
                    {
                        List<Currency> CurrenciesList = new List<Currency>();
                        var RTRates = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live");

                        Dictionary<string, float> dictionaryCurrencies = DbRates.CurrenciesList.ToDictionary(key => key.IssuesCountry.Code, value => value.Value);
                        foreach (var qoute in RTRates.quotes)
                        {
                        Country issuesCountry = await context.Countries.Where(t => t.Code == qoute.Key.Substring(3)).FirstOrDefaultAsync();
                        Currency newCurrency = new Currency() { Value = float.Parse(qoute.Value), IssuesCountry = issuesCountry, };
                            addDirectionAndMagnitude(qoute, dictionaryCurrencies, newCurrency);
                            CurrenciesList.Add(newCurrency);
                        }

                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    dateTime = dateTime.AddSeconds(RTRates.Timestamp);
                    Currencies newCurrencies = context.CurrenciesByDate.Find(DbRates.id);
                            newCurrencies.CurrenciesList = CurrenciesList;
                            newCurrencies.date = dateTime;
                            context.SaveChanges();
                            return newCurrencies;
                        }


                   
            }

        }
    }
}
