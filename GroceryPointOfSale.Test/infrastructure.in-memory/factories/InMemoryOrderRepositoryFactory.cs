using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryOrderRepositoryFactory : RepositoryFactory<InMemoryOrderRepository>
    {
        protected override void Seed(ref InMemoryOrderRepository repository)
        {
            repository.CreateOrder(OrderProvider.CreateOrderWithScannedItems());
        }
    }
}