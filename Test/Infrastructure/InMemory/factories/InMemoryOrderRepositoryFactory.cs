using PointOfSale.Infrastructure.InMemory;
using PointOfSale.Test.Domain;

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
