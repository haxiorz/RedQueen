using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RedQueen.Models.Coins;
using RedQueen.Models.CoinsViewModels;

namespace RedQueen.Data
{
    public class Repository : IRepository
    {
        private readonly RedQueenDbContext _context;
        public Repository(RedQueenDbContext context)
        {
            _context = context;
        }

        public List<Coin> GetAllCoinsNames()
        {
            return _context.Coins.ToList();
        }

        public string CoinChangeSinceLastCheck(string name, decimal currentPrice)
        {
            var lastPrice = _context.Coins.Where(c => c.Name == name).Select(c => c.LastPrice).FirstOrDefault();
            decimal increaseOrDecrease = 0;
            string orientation = "";
            if (lastPrice == 0)
                lastPrice = currentPrice;
            else if (lastPrice > currentPrice)
            {
                increaseOrDecrease = lastPrice - currentPrice;
                orientation = "-";
            }
            else if (lastPrice < currentPrice)
            {
                increaseOrDecrease = currentPrice - lastPrice;
                orientation = "+";
            }
            else if (lastPrice == currentPrice)
                return "0%";

            var change = (increaseOrDecrease / lastPrice * 100).ToString("#.##");
            if (!String.IsNullOrEmpty(orientation) && change != "")
                change = change.Insert(0, orientation);
            else
                change = "0";

            if (change == "")
                change = "0";

            try
            {
                if (change[1] == ',')
                    change = change.Insert(1, "0");
            }
            catch (Exception e) { }

            UpdateCoinLastPrice(name, currentPrice);

            return change + "%";
        }

        private void UpdateCoinLastPrice(string name, decimal currentPrice)
        {
            var coin = _context.Coins.FirstOrDefault(c => c.Name == name);
            coin.LastPrice = currentPrice;
            _context.SaveChanges();
        }

        public void AddNewCoin(AllCoins newCoin)
        {
            _context.Coins.Add(new Coin() {Name = newCoin.NewCoin, Quantity = newCoin.NewCoinQt});
            _context.SaveChanges();
        }
    }
}