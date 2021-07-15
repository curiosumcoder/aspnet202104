using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Northwind.Store.UI.Web.Intranet.Controllers
{
    public class DemoController : Controller
    {
        private readonly ILogger<DemoController> _logger;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly UserManager<IdentityUser> _um;

        public DemoController(ILogger<DemoController> logger, RoleManager<IdentityRole> rm, UserManager<IdentityUser> um)
        {
            _logger = logger;
            _rm = rm;
            _um = um;
        }

        /// <summary>
        /// Crear roles y asignarle usuario
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            // Crear roles en caso de que no existan
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _rm.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Crear roles en la base de datos
                    roleResult = await _rm.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Crear usuarios en caso de que no existan            
            string[] usersNames = { "gbermude@outlook.com" };
            foreach (var userName in usersNames)
            {
                var user = await _um.FindByNameAsync(userName);

                if (user == null)
                {
                    user = new IdentityUser { UserName = userName, Email = userName };

                    var result = await _um.CreateAsync(user, "Demo@123");
                    if (result.Succeeded)
                    {

                    }
                }
            }

            return View("Index");
        }

        /// <summary>
        /// Permite agregar un usuario a un rol específico.
        /// https://localhost:5001/Demo/AddToRol?username=gbermude@outlook.com&roleName=Admin
        /// </summary>
        public async Task<IActionResult> AddToRol(string userName, string roleName)
        {
            // Buscar al usuario
            var user = await _um.FindByNameAsync(userName);

            if (user != null)
            {
                // Se asigna un rol al usuario
                if (!string.IsNullOrEmpty(roleName))
                {
                    await _um.AddToRoleAsync(user, roleName);
                }
            }

            return View("Index");
        }

        /// <summary>
        /// Permite agregar un usuario a un rol específico.
        /// https://localhost:5001/Demo/AddClaimToUser?username=gbermude@outlook.com&claimType=DateOfBirth&claimValue=19/09/1977
        /// https://localhost:5001/Demo/AddClaimToUser?username=gbermude@outlook.com&claimType=http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth&claimValue=19/09/1977
        /// </summary>
        public async Task<IActionResult> AddClaimToUser(string userName, string claimType, string claimValue)
        {
            // Buscar al usuario
            var user = await _um.FindByNameAsync(userName);

            if (user != null)
            {
                // Se asigna el claim al usuario

                var claim = new Claim(claimType, claimValue);
                var result = await _um.AddClaimAsync(user, claim);

                if (result.Succeeded)
                {
                    var userClaims = await _um.GetClaimsAsync(user);
#if DEBUG
                    foreach (var item in userClaims)
                    {
                        System.Diagnostics.Debug.WriteLine($"{item.Type}, {item.Value}");
                    }
#endif
                }
            }

            return View("Index");
        }
    }
}
