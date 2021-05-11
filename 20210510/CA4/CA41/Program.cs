using System;

namespace CA41
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine(p.ProductName);
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
