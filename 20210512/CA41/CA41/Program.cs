using System;
using CA41.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace CA41
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NWContext())
            {
                Console.WriteLine(db.Products.First().ProductName);
            }

            //LeerConfiguracion();
            //Basic();
            //CRUD();
            //LINQToEntities();

            Console.ReadKey();
        }

        #region Demostraciones
        static string LeerConfiguracion()
        {
            // Soporte de archivo de configuración local
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var connStr = configuration.GetConnectionString("NW");

            Console.WriteLine(connStr);
            return connStr;
        }

        private static void Basic()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NWContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NW"));

            //using (var db = new NWContext())
            using (var db = new NWContext(optionsBuilder.Options))
            {
                // query
                // IQueryable<Product>
                // IEnumerable<Product> - materializado
                var q0 = from p in db.Products
                         where p.ProductName.Contains("queso")
                         select p;

                var q1 = (from p in db.Products
                          where p.ProductName.Contains("queso")
                          select p).All(p => p.Discontinued);

                // métodos de extensión + expresiones lambda
                var query = db.Products.Where(p => p.ProductName.Contains("queso"));

                bool discontinued = db.Products.All(p => p.Discontinued);

                //foreach (var p in db.Products)
                foreach (var p in query)
                {
                    Console.WriteLine($"{p.ProductId} {p.ProductName}");
                }
            }
        }

        private static void CRUD()
        {
            using (var db = new NWContext())
            {
                int lastId = db.Products.Max(p => p.ProductId);

                var np = new Product()
                {
                    ProductName = $"Demostración #{++lastId}",
                    Discontinued = false
                };

                db.Products.Add(np);
                db.SaveChanges();

                np.ProductName += " ACTUALIZADO";

                foreach (var e in db.ChangeTracker.Entries<Product>())
                {
                    Console.WriteLine($"{e.Property("ProductName").CurrentValue} {e.State}");
                }

                db.SaveChanges();

                //db.Remove(np);
                db.Products.Remove(np);
                db.SaveChanges();

                foreach (var p in db.Products.Include(p => p.Category))
                {
                    Console.WriteLine($"{p.ProductId}, {p.ProductName}, {p.UnitPrice}, {p.Category?.CategoryName ?? "Sin categoría"}");
                }

                Console.WriteLine($"Total de productos: {db.Products.Count()}");
                Console.WriteLine($"Total de productos, inventario > 100: {db.Products.Count(p => p.UnitsInStock > 100)}");
                Console.WriteLine($"Total de productos, proveedores de Québec: {db.Products.Count(p => p.Supplier.Region == "Québec")}");

                Console.ReadLine();
            }
        }

        static void LINQToEntities()
        {
            using (NWContext db = new NWContext())
            {
                // Filtro
                var q1 = from c in db.Customers
                         where c.Country == "Mexico"
                         select c;

                var q1x = db.Customers.
                    Where(c => c.Country == "Mexico");

                var result = q1.Count();
                var lista = q1.ToList();
                var any = q1.Any();
                var any2 = db.Customers.Any(c => c.Country == "Mexico");

                foreach (var item in q1x)
                {
                    Console.WriteLine($"{item.CustomerId} {item.CompanyName} {item.ContactName} {item.Country}");
                }
            }

            // Proyecciones
            using (NWContext db = new NWContext())
            {
                var q2 = from c in db.Customers
                         select c.Country;
                var q2x = db.Customers.Select(c => c.Country);

                var q2y = from c in db.Customers
                          select new { c.CustomerId, c.ContactName };

                var q2z = db.Customers.Select(c =>
                    new { Id = c.CustomerId, c.ContactName });

                var q2w = db.Customers.Select(c =>
                    new Category() { CategoryName = c.ContactName });

                Console.Clear();
                foreach (var item in q2z)
                {
                    Console.WriteLine($"{item.Id}, {item.ContactName}");
                }
            }
        }
        #endregion
    }
}
