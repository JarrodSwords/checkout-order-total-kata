namespace PointOfSale.Test.Infrastructure.InMemory
{
    public class InMemoryProductRepositoryTest : IProductRepositoryTest
    {
        public InMemoryProductRepositoryTest()
        {
            _productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
        }
    }
}
