namespace PointOfSale.Test
{
    public class OrderServiceTest : IOrderServiceTest
    {
        public OrderServiceTest()
        {
            _orderService = DependencyProvider.CreateOrderService();
        }
    }
}
