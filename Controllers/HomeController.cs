using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedQueen.Data;
using RedQueen.Models;
using RedQueen.Models.CoinsViewModels;

namespace RedQueen.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        string url = "https://www.worldcoinindex.com/apiservice/json?key=i4lnJIyN3TdtfG4Rn17alYtxV6Zi0G";

        private readonly IRepository _repository;

        public HomeController(IRepository repostiory)
        {
            client = new HttpClient {BaseAddress = new Uri(url)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _repository = repostiory;
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var allCoins = _repository.GetAllCoinsNames();
                var list = allCoins.Select(c => c.Name).ToList();

                var apiCoins = JsonConvert.DeserializeObject<AllCoins>(responseData).Markets.Where(c => list.Contains(c.Name)).ToList();
                foreach (var coin in apiCoins)
                {
                    coin.Quantity = (decimal)allCoins.Where(c => c.Name == coin.Name).Select(c => c.Quantity).FirstOrDefault();
                    coin.ChangeSinceLastCheck = _repository.CoinChangeSinceLastCheck(coin.Name, coin.Price_usd);
                }

                var trackedCoinsNames = apiCoins.Select(c => c.Name).ToList();

                var newmodel = new AllCoins()
                {
                    Markets = apiCoins,
                    CoinsToAdd = JsonConvert.DeserializeObject<AllCoins>(responseData).Markets.Where(c => !trackedCoinsNames.Contains(c.Name)).Select(c => c.Name).ToList()
            };

                return View(newmodel);
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Index(AllCoins AllCoins)
        {
            _repository.AddNewCoin(AllCoins);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}