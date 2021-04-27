using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA30.Pages
{
    public class ServerSideModel : PageModel
    {
        private readonly ILogger<ServerSideModel> _logger;

        public List<Northwind.Model.Product> Products { get; set; } = new List<Northwind.Model.Product>();

        [BindProperty()]
        public string Filter { get; set; }

        public ServerSideModel(ILogger<ServerSideModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string filter)
        {
            var pD = new Northwind.Data.ProductD();
            var pM = pD.List();
            string f = filter;

            // LINQ
            pM = pM.Where(p => p.ProductName.Contains(f ?? "",
                StringComparison.InvariantCultureIgnoreCase)).ToList();

            Products = pM;
        }

        public void OnPost()
        {
            var pD = new Northwind.Data.ProductD();
            var pM = pD.List();
            string f = Filter;

            // LINQ
            pM = pM.Where(p => p.ProductName.Contains(f ?? "",
                StringComparison.InvariantCultureIgnoreCase)).ToList();

            Products = pM;
        }
    }
}
