using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryFactory : RepositoryFactory<InMemoryProductRepository>
    {
        protected override void Seed(ref InMemoryProductRepository repository)
        {
            repository.CreateProduct(new Product("can of soup"));
            repository.CreateProduct(new Product("frozen pizza"));
            repository.CreateProduct(new Product("milk"));
        }
    }
}