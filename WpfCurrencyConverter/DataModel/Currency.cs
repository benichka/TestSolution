namespace WpfCurrencyConverter.DataModel
{
    /// <summary>
    /// Class describing a currency
    /// </summary>
    public class Currency
    {
        public string Title { get; set; }
        public decimal Rate { get; set; }

        /// <summary>
        /// Build a currency based on a title and a change rate against euro
        /// </summary>
        /// <param name="title">Currency title (euro, dollar...)</param>
        /// <param name="rate">Exchange rate against euro</param>
        public Currency(string title, decimal rate)
        {
            Title = title;
            Rate = rate;
        }

        /// <summary>
        /// Represent the currency as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Title;
        }
    }
}
