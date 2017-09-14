using System.Collections.Generic;
using WpfCurrencyConverter.DataModel;

namespace WpfCurrencyConverter.ViewModel
{
    /// <summary>
    /// ViewModel for the CurrencyConverter view.
    /// Calculate a target value based on a price in euro and a chosen currency.
    /// </summary>
    public class CurrencyConverterViewModel : ViewModelBase
    {
        private decimal _Euros;
        /// <summary>The value in euro to convert to the chosen currency</summary>
        public decimal Euros
        {
            get { return this._Euros; }
            set
            {
                SetProperty(null, ref this._Euros, value);
                OnEurosChanged();
            }
        }

        private decimal _Converted;
        /// <summary>The converted value, from euro to the chosen currency</summary>
        public decimal Converted
        {
            get { return this._Converted; }
            set
            {
                SetProperty(null, ref this._Converted, value);
            }
        }

        private Currency _SelectedCurrency;
        /// <summary>Selected target currency</summary>
        public Currency SelectedCurrency
        {
            get { return this._SelectedCurrency; }
            set
            {
                SetProperty(null, ref this._SelectedCurrency, value);
                OnSelectedCurrencyChanged();
            }
        }

        private IEnumerable<Currency> _Currencies;
        /// <summary>List of currencies available</summary>
        public IEnumerable<Currency> Currencies
        {
            get { return this._Currencies; }
            set
            {
                SetProperty(null, ref this._Currencies, value);
            }
        }

        private string _ResultText;
        /// <summary>Text where the converted value (from euro to the chosen currency) is placed</summary>
        public string ResultText
        {
            get { return this._ResultText; }
            set
            {
                SetProperty(null, ref this._ResultText, value);
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CurrencyConverterViewModel()
        {
            // Populate the currencies available
            Currencies = new Currency[]
            {
                new Currency("US Dollar", 1.1M),
                new Currency("British Pound", 0.9M),
            };
        }

        /// <summary>
        /// Method executed when the value in euro is changed
        /// </summary>
        private void OnEurosChanged()
        {
            ComputeConverted();
        }

        /// <summary>
        /// Method executed when the selected currency is changed
        /// </summary>
        private void OnSelectedCurrencyChanged()
        {
            ComputeConverted();
        }

        /// <summary>
        /// Calculate the target value based on the price in euro and the selected target currency
        /// </summary>
        private void ComputeConverted()
        {
            if (SelectedCurrency == null)
            {
                return;
            }

            Converted = Euros * SelectedCurrency.Rate;
            ResultText = string.Format($"Amount in {SelectedCurrency.Title}");
        }
    }
}
