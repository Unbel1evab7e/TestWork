using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using TestWork.Logic;
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
      
        private bool _baseTrigger = false;
        private bool _secondTrigger = false;
        private CurrencyViewModel currencies;
   
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
            currencyViewModel.SecondCurrency.CharCode = SecondCurrencyNameTextBlock.Text;
            currencyViewModel.Side = CurrencySide.BaseCurrency;
            Frame.Navigate(typeof(Currencies), currencyViewModel);
        }

        private void SecondCurrencyButton_Click(object sender, RoutedEventArgs e)
        {
            CurrencyViewModel currencyViewModel = new CurrencyViewModel();
            currencyViewModel.BaseCurrency = new Currency();
            currencyViewModel.SecondCurrency = new Currency();
            currencyViewModel.BaseCurrency.CharCode = BaseCurrencyNameTextBlock.Text;
            currencyViewModel.Side = CurrencySide.SecondCurrency;
            Frame.Navigate(typeof(Currencies), currencyViewModel);
        }

        private void BaseCurrencyAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var converter = new CurrencyConverter();
                if (decimal.TryParse(BaseCurrencyAmountTextBox.Text, out var reuslt) && _baseTrigger == true)
                    SecondCurrencyAmountTextBox.Text = decimal.Round(converter.CalculateConvert(reuslt, BaseCurrencyNameTextBlock.Text, SecondCurrencyNameTextBlock.Text),4).ToString();
            }
            catch(Exception ex)
            {

            }
        }

        private void SecondCurrencyAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var converter = new CurrencyConverter();
                if (decimal.TryParse(SecondCurrencyAmountTextBox.Text, out var reuslt)&&_secondTrigger==true)
                    BaseCurrencyAmountTextBox.Text = decimal.Round(converter.CalculateConvert(reuslt, SecondCurrencyNameTextBlock.Text, BaseCurrencyNameTextBlock.Text),4).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void BaseCurrencyAmountTextBox_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _baseTrigger = true;
        }

        private void BaseCurrencyAmountTextBox_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _baseTrigger = false;
        }

        private void SecondCurrencyAmountTextBox_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _secondTrigger = true;
        }

        private void SecondCurrencyAmountTextBox_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _secondTrigger = false;
        }

      

    }
}
