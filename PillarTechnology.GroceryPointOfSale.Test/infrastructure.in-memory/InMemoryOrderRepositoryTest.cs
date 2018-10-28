using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryOrderRepositoryTest : IOrderRepositoryTest
    {
        public InMemoryOrderRepositoryTest()
        {
            _orderRepository = new InMemoryOrderRepository();
        }
    }
}