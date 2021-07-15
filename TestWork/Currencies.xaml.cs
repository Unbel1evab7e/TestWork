using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TestWork.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TestWork
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Currencies : Page
    {

        private CurrencyViewModel _currency;
        public  Currencies()
        {
            
            InitializeComponent();
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js").Result;
            
            foreach (var item in JsonConvert.DeserializeObject<JToken>(response.Content.ReadAsStringAsync().Result)["Valute"])
            {
                var CharCode = item.Children()["CharCode"].ElementAt(0).ToString();
                var Name = item.Children()["Name"].ElementAt(0).ToString();
                var tempCurrency = new Currency() {CharCode=CharCode ,Name =Name};
                CurrenciesList.Items.Add(tempCurrency);
            }
            CurrenciesList.Items.Add(new Currency() { CharCode = "RUB", Name = "Российский рубль" });
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _currency = e.Parameter as CurrencyViewModel;
        }
        private void RelativePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (_currency.Side == CurrencySide.BaseCurrency)
            {
                _currency.BaseCurrency.CharCode = ((Currency)CurrenciesList.SelectedItem).CharCode;
            }
            if (_currency.Side == CurrencySide.SecondCurrency)
            {
                _currency.SecondCurrency.CharCode = ((Currency)CurrenciesList.SelectedItem).CharCode;
            }
            Frame.Navigate(typeof(LoadingPage),_currency);
        }
    }
}
