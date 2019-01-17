using System.Collections.Generic;
using RedQueen.Models.Coins;
using RedQueen.Models.CoinsViewModels;

namespace RedQueen.Data
{
    public interface IRepository
    {
        List<Coin> GetAllCoinsNames();
        string CoinChangeSinceLastCheck(string name, decimal currentPrice);
        void AddNewCoin(AllCoins newCoin);
    }
}
