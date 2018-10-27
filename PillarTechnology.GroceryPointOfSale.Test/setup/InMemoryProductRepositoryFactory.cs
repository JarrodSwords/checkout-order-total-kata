using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryFactory : RepositoryFactory<InMemoryProductRepository>
    {
        private string[] _productNames = { "can of soup", "frozen pizza", "milk" };

        protected override void Seed(ref InMemoryProductRepository repository)
        {
            foreach (var productName in _productNames)
                repository.CreateProduct(new Product(productName));
        }
    }
}