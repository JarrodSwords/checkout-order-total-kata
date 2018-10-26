using System;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private ICollection<Product> _products = new List<Product>();

        public void CreateProduct(Product product)
        {
            _products.Add(product);
        }

        public Product FindProduct(string productName)
        {
            return _products.First(x => x.Name == productName);
        }
    }
}