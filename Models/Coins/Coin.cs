using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace RedQueen.Models.Coins
{
    public class Coin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal LastPrice { get; set; }
    }
}
