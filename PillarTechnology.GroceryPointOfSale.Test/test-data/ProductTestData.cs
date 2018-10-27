using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductTestData : IEnumerable<object[]>
    {
        private readonly ICollection<Product> _products = new List<Product>
        {
            new Product("can of soup", Money.USDollar(0.5m)),
            new Product("frozen pizza", Money.USDollar(3m)),
            new Product("lean ground beef", Money.USDollar(2m), SellByType.Weight)
        };

        public ICollection<Product> Products { get { return _products; } }

        public Product GetProductSoldByUnit() => _products.First(x => x.SellByType == SellByType.Unit);
        public Product GetProductSoldByWeight() => _products.First(x => x.SellByType == SellByType.Weight);
        
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var product in _products)
                yield return new object[] { product.Name };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void SeedRepository(ref IProductRepository productRepository)
        {
            foreach (var product in _products)
                productRepository.CreateProduct(product);
        }
    }
}