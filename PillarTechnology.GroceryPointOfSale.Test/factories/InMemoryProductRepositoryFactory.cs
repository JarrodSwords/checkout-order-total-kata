using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryFactory : RepositoryFactory<InMemoryProductRepository>
    {
        protected override void Seed(ref InMemoryProductRepository repository)
        {
            var products = new ProductTestData().GetEnumerator();

            while (products.MoveNext())
                repository.CreateProduct(products.Current);
        }
    }
}