using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryTest : IProductRepositoryTest
    {
        public InMemoryProductRepositoryTest()
        {
            _productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
        }
    }
}