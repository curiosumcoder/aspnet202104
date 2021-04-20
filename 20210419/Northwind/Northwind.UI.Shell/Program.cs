using System;

namespace Northwind.UI.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            var pD = new Northwind.Data.ProductD();
            var products = pD.List();

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} {p.Name} {p.Price}");
            }

            Console.WriteLine("Ready!");
            Console.ReadKey();
        }
    }
}
