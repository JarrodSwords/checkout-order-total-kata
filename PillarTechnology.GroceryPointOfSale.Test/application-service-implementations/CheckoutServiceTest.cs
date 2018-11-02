using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class CheckoutServiceTest : ICheckoutServiceTest
    {
        public CheckoutServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            _orderRepository = new InMemoryOrderRepositoryFactory().CreateSeededRepository();
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var removeScannedItemArgsValidator = new RemoveScannedItemArgsValidator(_orderRepository);
            var scanItemArgsValidator = new ScanItemArgsValidator(productRepository);
            var scanWeightedItemArgsValidator = new ScanWeightedItemArgsValidator(productRepository);

            _checkoutService = new CheckoutService(mapper, _orderRepository, productRepository, removeScannedItemArgsValidator, scanItemArgsValidator, scanWeightedItemArgsValidator);
        }
    }
}