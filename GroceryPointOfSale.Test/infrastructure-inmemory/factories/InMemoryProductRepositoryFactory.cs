using GroceryPointOfSale.Infrastructure.InMemory;

namespace GroceryPointOfSale.Test
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