using System.Collections.Generic;
using System.Globalization;

namespace RedQueen.Models.CoinsViewModels
{
    public class AllCoins
    {
        private readonly CultureInfo _us;

        public AllCoins()
        {
            _us = CultureInfo.GetCultureInfo("en-US");
        }
        public IEnumerable<CoinVm> Markets { get; set; }
        public string ChangeSinceLastCheck { get; set; }
        public List<string> CoinsToAdd { get; set; }

        private decimal totalValue;

        private decimal TotalValue
        {
            get => GetTotalValue();
            set => totalValue = value;
        }

        public string FormattedTotalValue => string.Format(_us, "{0:C}", TotalValue);

        private decimal GetTotalValue()
        {
            decimal totalVal = 0;
            if (Markets != null)
            {
                foreach (var coin in Markets)
                {
                    totalVal += coin.TotalValue;
                }
            }
            return totalVal;
        }

        public string NewCoin { get; set; }
        public double NewCoinQt { get; set; }
    }
}
