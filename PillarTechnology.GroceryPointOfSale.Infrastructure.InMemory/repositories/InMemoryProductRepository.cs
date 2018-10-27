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
            if (Exists(product))
                throw new ArgumentException("Product already exists");

            _products.Add(product);
        }

        private bool Exists(Product product)
        {
            return _products.Any(x => x.Name == product.Name);
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