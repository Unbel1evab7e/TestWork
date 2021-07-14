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
        private List<object> _parametrs;
        private string _currencyType;
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
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _parametrs = e.Parameter as List<object>;
            _currency = (CurrencyViewModel)_parametrs.ElementAt(0);
             _currencyType = _parametrs.ElementAt(1).ToString();
        }
        private void RelativePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (_currencyType == "Base")
            {
                _currency.BaseCurrency.CharCode = ((Currency)CurrenciesList.SelectedItem).CharCode;
            }
            if (_currencyType == "Second")
            {
                _currency.SecondCurrency.CharCode = ((Currency)CurrenciesList.SelectedItem).CharCode;
            }
            Frame.Navigate(typeof(MainPage),_currency);
        }
    }
}
