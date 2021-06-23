using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA70.Controllers
{
    public class CartController : Controller
    {
        private readonly NWContext _db;
        private readonly SessionSettings _ss;

        public CartController(NWContext db, SessionSettings ss)
        {
            _db = db;
            _ss = ss;
        }

        public IActionResult Index()
        {
            return View(_ss.Cart);
        }

        public ActionResult Add(int? id)
        {
            if (id.HasValue)
            {
                #region Session
                var p = _db.Products.Find(id);
                var cart = _ss.Cart;

                cart.Items.Add(p);

                _ss.Cart = cart;
                #endregion
            }

            return RedirectToAction("Index");
        }
    }
}
