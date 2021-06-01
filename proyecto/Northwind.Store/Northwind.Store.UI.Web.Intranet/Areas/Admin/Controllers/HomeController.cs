﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin, Conta")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
