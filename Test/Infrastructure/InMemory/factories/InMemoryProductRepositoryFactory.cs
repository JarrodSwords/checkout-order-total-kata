using PointOfSale.Infrastructure.InMemory;
using PointOfSale.Test.Domain;

namespace PointOfSale.Test.Infrastructure.InMemory
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
