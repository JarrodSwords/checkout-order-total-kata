using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryFactory : RepositoryFactory<InMemoryProductRepository>
    {
        protected override void Seed(ref InMemoryProductRepository repository)
        {
            foreach (var product in ProductProvider.Products)
                repository.CreateProduct(product);
        }
    }
}