using System;
using System.Threading.Tasks;

namespace CA41
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine(p.ProductName);
                }

                var sales = await db.GetProcedures().SalesbyYearAsync(new DateTime(1990, 1, 1), DateTime.Today);

                foreach (var s in sales)
                {
                    Console.WriteLine(s.OrderID);
                }
            }



            Console.WriteLine("READY!");
            Console.ReadLine();
        }
    }
}
