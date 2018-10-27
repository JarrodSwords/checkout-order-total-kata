using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;
using Xunit;

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