using PointOfSale.Test.Services;

namespace PointOfSale.Test.Implementations.Basic
{
    public class OrderServiceTest : IOrderServiceTest
    {
        public OrderServiceTest()
        {
            _orderService = DependencyProvider.OrderService();
        }
    }
}
