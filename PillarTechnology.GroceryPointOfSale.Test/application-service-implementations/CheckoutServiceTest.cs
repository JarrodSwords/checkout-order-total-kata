using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class CheckoutServiceTest : ICheckoutServiceTest
    {
        public CheckoutServiceTest()
        {
            _orderRepository = new InMemoryOrderRepositoryFactory().CreateSeededRepository();
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            _checkoutService = new CheckoutService(_orderRepository, productRepository);
        }
    }
}