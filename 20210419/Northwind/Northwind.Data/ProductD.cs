using Northwind.Model;
using System;
using System.Collections.Generic;

namespace Northwind.Data
{
    public class ProductD
    {
        public List<Product> List()
        {
            return new List<Product>() { 
                new Product() { Id = 1, Name = "Chia", Price = 100 },
                new Product() { Id = 2, Name = "Café", Price = 1000 },
                new Product() { Id = 3, Name = "Chocolate", Price = 1000 }
            };
        }
    }
}
