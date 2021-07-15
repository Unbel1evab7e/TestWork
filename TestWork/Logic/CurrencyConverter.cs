using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestWork.Logic
{
    public class CurrencyConverter
    {
        public decimal CalculateConvert(decimal amount, string baseCurrency, string secondCurrency)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = httpClient.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js").Result;

                if (amount != 0)
                {
                    if (baseCurrency == secondCurrency)
                        return amount;
                    else if (baseCurrency == "RUB")
                    {
                        decimal value = 0;

                        foreach (var item in JsonConvert.DeserializeObject<JToken>(response.Content.ReadAsStringAsync().Result)["Valute"])
                        {
                            if (item.Children()["CharCode"].ElementAt(0).ToString() == secondCurrency)
                            {
                                decimal.TryParse(item.Children()["Value"].ElementAt(0).ToString(), out value);
                            }
                        }
                        if (value != 0)
                            return ((1 / value) * amount);
                    }

                    else if (secondCurrency == "RUB")
                    {
                        decimal value = 0;

                        foreach (var item in JsonConvert.DeserializeObject<JToken>(response.Content.ReadAsStringAsync().Result)["Valute"])
                        {
                            if (item.Children()["CharCode"].ElementAt(0).ToString() == baseCurrency)
                            {
                                decimal.TryParse(item.Children()["Value"].ElementAt(0).ToString(), out value);
                            }
                        }
                        return (value * amount);
                    }
                    else
                    {
                        decimal baseValue = 0;
                        decimal secondValue = 0;

                        foreach (var item in JsonConvert.DeserializeObject<JToken>(response.Content.ReadAsStringAsync().Result)["Valute"])
                        {
                            if (item.Children()["CharCode"].ElementAt(0).ToString() == baseCurrency)
                            {
                                decimal.TryParse(item.Children()["Value"].ElementAt(0).ToString(), out baseValue);
                            }
                            if (item.Children()["CharCode"].ElementAt(0).ToString() == secondCurrency)
                            {
                                decimal.TryParse(item.Children()["Value"].ElementAt(0).ToString(), out secondValue);
                            }
                        }

                        if (secondValue != 0)
                            return ((baseValue / secondValue) * amount);
                    }

                }
                
                return 0m;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}

