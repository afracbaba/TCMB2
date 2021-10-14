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
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IConfiguration configuration;

        public ApiController(ILogger<ApiController> logger, IConfiguration config)
        {
            _logger = logger;
            this.configuration = config;
        }

        public IActionResult GetLastCurrencies()
        {
            string cs = configuration.GetConnectionString("DBConnection");
            SqlConnection connection = new SqlConnection(cs);

            //verinin okunup, veritabanına kaydedildiği bölüm
            XmlDocument xml = new XmlDocument();
            xml.Load("https://www.gamermarkt.com/currencies.xml");

            XmlElement doc = xml.DocumentElement;
            XmlNodeList nodes = doc.SelectNodes("/Tarih_Date/Currency");

            connection.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = connection;
            cmd1.CommandText = "DELETE FROM currencies2";
            cmd1.ExecuteNonQuery();
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["Kod"].Value.Trim() != "XDR")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO currencies2 (code, name, price_buy, price_sell) VALUES (@code, @name, @price_buy, @price_sell)";
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = node.Attributes["Kod"].Value.Trim();
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = node["Isim"].InnerText.Trim();
                    cmd.Parameters.Add("@price_buy", SqlDbType.NVarChar).Value = node["ForexBuying"].InnerText.Trim();
                    cmd.Parameters.Add("@price_sell", SqlDbType.NVarChar).Value = node["ForexSelling"].InnerText.Trim();
                    cmd.ExecuteNonQuery();
                }
            }
            connection.Close();

            return Content("success");
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
