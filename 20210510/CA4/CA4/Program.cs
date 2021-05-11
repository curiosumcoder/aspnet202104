using System;
using System.Linq;

namespace CA4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NWContext())
            {
                var products = db.Products.ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"{p.ProductId}, {p.ProductName}, {p.UnitPrice}");
                }

                // Change Tracking
                var p1 = products.Single(p => p.ProductId == 1);

                p1.ProductName += "M";

                var newProduct = new Product() { ProductName = "Demostración" };

                db.Products.Add(newProduct);

                var deleteProduct = products.Single(p => p.ProductId == 78);
                //db.Remove(deleteProduct);
                db.Products.Remove(deleteProduct);

                db.SaveChanges();
            }

            Console.WriteLine("READY!");
            Console.ReadLine();
        }
    }
}
