using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class CheckoutServiceTest : ICheckoutServiceTest
    {
        public CheckoutServiceTest()
        {
            _orderRepository = new InMemoryOrderRepositoryFactory().CreateSeededRepository();
            _productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            _checkoutService = new CheckoutService(_orderRepository, _productRepository);
        }
    }
}