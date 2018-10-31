using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryOrderRepositoryFactory : RepositoryFactory<InMemoryOrderRepository>
    {
        protected override void Seed(ref InMemoryOrderRepository repository)
        {
            foreach (var order in OrderProvider.Orders)
                repository.CreateOrder(order);
        }
    }
}