using System.Collections.Generic;
using RedQueen.Models.CoinsViewModels;
using Coin = RedQueen.Models.Coins.Coin;

namespace RedQueen.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Coin> Coins { get; set; }
        public AllCoins AllCoins { get; set; }

    }
}
