namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderServiceTest : IOrderServiceTest
    {
        public OrderServiceTest()
        {
            _orderService = DependencyProvider.CreateOrderService();
        }
    }
}
