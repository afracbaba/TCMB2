using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TCMB2.Models;
using System.Xml;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TCMB2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            this.configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExchangeRates()
        {
            string cs = configuration.GetConnectionString("DBConnection");
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();
            SqlCommand query = new SqlCommand("SELECT * FROM currencies2", connection);
            SqlDataReader dr = query.ExecuteReader();

            List<Currency> currencies = new List<Currency>();
            while (dr.Read())
            {
                currencies.Add(new Currency
                {
                    code = (string)dr["code"],
                    name = (string)dr["name"],
                    price_buy = (decimal)dr["price_buy"],
                    price_sell = (decimal)dr["price_sell"],
                });
            }
            connection.Close();

            ViewData["currencies"] = currencies;

            return View();
        }

        public class Currency
        {
            public string code { get; set; }
            public string name { get; set; }
            public decimal price_buy { get; set; }
            public decimal price_sell { get; set; }            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
