using System;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private ICollection<Product> _products = new List<Product>();

        public Product CreateProduct(Product product)
        {
            _products.Add(product);
            return FindProduct(product.Name);
        }

        public bool Exists(string productName)
        {
            return _products.Any(x => x.Name == productName);
        }

        public Product FindProduct(string productName)
        {
            return _products.First(x => x.Name == productName);
        }

        public Product UpdateProduct(Product product)
        {
            return FindProduct(product.Name);
        }
    }
}