using PointOfSale.Test.Services;

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
