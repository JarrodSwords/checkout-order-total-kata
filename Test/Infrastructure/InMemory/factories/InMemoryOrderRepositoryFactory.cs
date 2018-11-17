using PointOfSale.Infrastructure.InMemory;

namespace PointOfSale.Test.Infrastructure.InMemory
{
    public class InMemoryOrderRepositoryFactory : RepositoryFactory<InMemoryOrderRepository>
    {
        protected override void Seed(ref InMemoryOrderRepository repository)
        {
            repository.CreateOrder(OrderProvider.CreateOrderWithScannedItems());
        }
    }
}
