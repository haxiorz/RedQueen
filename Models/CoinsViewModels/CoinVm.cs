using System.Globalization;

namespace RedQueen.Models.CoinsViewModels
{
    public class CoinVm
    {
        private CultureInfo _us;
        public CoinVm()
        {
            _us = CultureInfo.GetCultureInfo("en-US");
        }

        private decimal totalValue;

        public string Label { get; set; }
        public string Name { get; set; }
        public decimal Price_btc { get; set; }
        public decimal Price_cny { get; set; }
        public decimal Price_eur { get; set; }
        public decimal Price_gbp { get; set; }
        public decimal Price_rur { get; set; }
        public decimal Price_usd { get; set; }
        public long Timestamp { get; set; }
        public decimal Volume_24h { get; set; }
        public decimal Quantity { get; set; }
        public string ChangeSinceLastCheck { get; set; }

        public decimal TotalValue
        {
            get => Quantity * Price_usd;
            set => totalValue = value;
        }

        public string FormattedTotalValue => string.Format(_us, "{0:C}", TotalValue);
        public string FormattedPrice => string.Format(_us, "{0:C}", Price_usd);
    }
}
