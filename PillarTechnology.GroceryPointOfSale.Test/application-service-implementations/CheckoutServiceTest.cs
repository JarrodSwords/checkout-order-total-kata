using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class CheckoutServiceTest : ICheckoutServiceTest
    {
        public CheckoutServiceTest()
        {
            _orderRepository = new InMemoryOrderRepositoryFactory().CreateSeededRepository();
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var removeScannedItemArgsValidator = new RemoveScannedItemArgsValidator(_orderRepository);
            var scanItemArgsValidator = new ScanItemArgsValidator(productRepository);
            var scanWeightedItemArgsValidator = new ScanWeightedItemArgsValidator(productRepository);

            _checkoutService = new CheckoutService(_orderRepository, productRepository, removeScannedItemArgsValidator, scanItemArgsValidator, scanWeightedItemArgsValidator);
        }
    }
}