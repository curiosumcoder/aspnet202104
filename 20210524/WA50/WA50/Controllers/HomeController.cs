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
using WA50.ViewModels;
using WA50.Extensions;

namespace WA50.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(HomeIndexViewModel vm)
        {
            if (vm.Filter == null)
            {
                vm = HttpContext.Session.GetObject<HomeIndexViewModel>("vm");
            }
            else
            {
                HttpContext.Session.SetObject<HomeIndexViewModel>("vm", vm);
            }

            using (var db = new NWContext())
            {
                if (vm != null)
                {
                    vm.Products = db.Products.Where(p => p.ProductName.Contains(vm.Filter)).ToList();
                }
            }

            return View(vm);
        }

        /// <summary>
        /// GET /
        /// GET /Home
        /// GET /Home/Index
        /// GET /Home/Index/123
        /// MVVM = Model View *ViewModel
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexOLD(string filter = "")
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
