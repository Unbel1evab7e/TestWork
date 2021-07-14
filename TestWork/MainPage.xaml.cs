using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestWork.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestWork
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private Currency _currency;
        private CurrencyViewModel currencies;
        private List<object> _parametrs;
        private string _currencyType;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currencies = e.Parameter as CurrencyViewModel;
            if(currencies!=null)
            {
                BaseCurrencyNameTextBlock.Text = currencies.BaseCurrency.CharCode;
                SecondCurrencyNameTextBlock.Text = currencies.SecondCurrency.CharCode;
            }
            

        }

        private void BaseCurrencyButton_Click(object sender, RoutedEventArgs e)
        {
            CurrencyViewModel currencyViewModel = new CurrencyViewModel();
            currencyViewModel.SecondCurrency = new Currency();
            currencyViewModel.BaseCurrency = new Currency();
            var listParametrs = new List<object>();
            var a = SecondCurrencyNameTextBlock;
            currencyViewModel.SecondCurrency.CharCode = SecondCurrencyNameTextBlock.Text;
            listParametrs.Add(currencyViewModel);
            listParametrs.Add("Base");
            Frame.Navigate(typeof(Currencies),listParametrs);
        }

        private void SecondCurrencyButton_Click(object sender, RoutedEventArgs e)
        {
            CurrencyViewModel currencyViewModel = new CurrencyViewModel();
            currencyViewModel.BaseCurrency = new Currency();
            currencyViewModel.SecondCurrency = new Currency();
            currencyViewModel.BaseCurrency.CharCode = BaseCurrencyNameTextBlock.Text;
            var listParametrs = new List<object>();
            listParametrs.Add(currencyViewModel);
            listParametrs.Add("Second");
            Frame.Navigate(typeof(Currencies),listParametrs);
        }

    }
}
