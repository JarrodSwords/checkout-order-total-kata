using GroceryPointOfSale.Infrastructure.InMemory;

namespace GroceryPointOfSale.Test
{
    public class InMemoryOrderRepositoryFactory : RepositoryFactory<InMemoryOrderRepository>
    {
        protected override void Seed(ref InMemoryOrderRepository repository)
        {
            repository.CreateOrder(OrderProvider.CreateOrderWithScannedItems());
        }
    }
}