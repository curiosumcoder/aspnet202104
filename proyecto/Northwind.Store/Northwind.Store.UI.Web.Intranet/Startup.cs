using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.UI.Web.Intranet.Auth;
using Northwind.Store.UI.Web.Intranet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Web.Intranet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<NWContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NW")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            #region Autorización
            //services.AddAuthorization();
            //services.AddAuthorization();
            services.AddAuthorization(options =>
            {
                // Uso de requerimiento y su manejador correspondiente
                // se requiere de services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();
                options.AddPolicy("ManagerPolicy", policy =>
                    policy.Requirements.Add(new OrderRequirement()));

                // Uso de requerimiento y su manejador correspondiente
                // se requiere de services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
                options.AddPolicy("MayorDeEdad", policy =>
                    policy.Requirements.Add(new MinimumAgeRequirement(18)));

                // Política que requiere del claim (EmployeeNumber)
                options.AddPolicy("EmployeeOnly", 
                policy => policy.RequireClaim("EmployeeNumber"));

                // Política que requiere de roles
                options.AddPolicy("ElevatedRights", policy =>
					policy.RequireRole("Admin","Manager"));

                // Requerir autenticación para toda la aplicación
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
                       
            services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddTransient<IAuthorizationHandler, OrderAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, OrderAuthorizationCrudHandler>();
            #endregion

            services.AddTransient<IRepository<Category, int>, BaseRepository<Category, int>>();
            services.AddTransient<CategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseStatusCodePages();
            //app.UseStatusCodePagesWithRedirects("/Home/ErrorWithCode?code={0}");
            app.UseStatusCodePagesWithRedirects("/ErrorStatus.html?code={0}");
            //app.UseStatusCodePagesWithReExecute("/Home/ErrorWithCode", "?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("admin", "admin", "admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
