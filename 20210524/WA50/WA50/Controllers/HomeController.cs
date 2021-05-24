using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Data;
using Northwind.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WA50.Models;

namespace WA50.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GET /
        /// GET /Home
        /// GET /Home/Index
        /// GET /Home/Index/123
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string filter = "")
        {
            List<Product> products = new List<Product>();

            using (var db = new NWContext())
            {
                products = db.Products.Where(p => p.ProductName.Contains(filter)).ToList();
            }

            //ViewData["products"] = products;
            //ViewBag.products = products;

            // aquí nuestro código
            //return View();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
