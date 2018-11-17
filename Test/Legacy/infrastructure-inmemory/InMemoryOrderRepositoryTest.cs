namespace PointOfSale.Test
{
    public class InMemoryOrderRepositoryTest : IOrderRepositoryTest
    {
        public InMemoryOrderRepositoryTest()
        {
            _orderRepository = DependencyProvider.CreateOrderRepository();
        }
    }
}
