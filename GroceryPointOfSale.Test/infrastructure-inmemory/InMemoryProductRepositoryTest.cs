namespace GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryTest : IProductRepositoryTest
    {
        public InMemoryProductRepositoryTest()
        {
            _productRepository = DependencyProvider.CreateProductRepository();
        }
    }
}
