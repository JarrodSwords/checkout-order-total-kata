using PointOfSale.Test.Domain;

namespace PointOfSale.Test.Infrastructure.InMemory
{
    public class InMemoryOrderRepositoryTest : IOrderRepositoryTest
    {
        public InMemoryOrderRepositoryTest()
        {
            _orderRepository = new InMemoryOrderRepositoryFactory().CreateSeededRepository();
        }
    }
}
