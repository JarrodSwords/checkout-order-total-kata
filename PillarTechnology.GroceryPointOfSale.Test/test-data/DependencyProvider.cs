using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class DependencyProvider
    {
        public static ICheckoutService CreateCheckoutService(IOrderRepository orderRepository)
        {
            var productRepository = CreateProductRepository();
            var removeScannedItemArgsValidator = new RemoveScannedItemArgsValidator(orderRepository);
            var scanItemArgsValidator = new ScanItemArgsValidator(productRepository);
            var scanWeightedItemArgsValidator = new ScanWeightedItemArgsValidator(productRepository);

            return new CheckoutService(CreateMapper(), orderRepository, productRepository, removeScannedItemArgsValidator, scanItemArgsValidator, scanWeightedItemArgsValidator);
        }

        public static IMapper CreateMapper() => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));

        public static IOrderRepository CreateOrderRepository() => new InMemoryOrderRepositoryFactory().CreateSeededRepository();

        public static IOrderService CreateOrderService()
        {
            return new OrderService(CreateMapper(), CreateOrderRepository());
        }

        public static IProductConfigurationService CreateProductConfigurationService()
        {
            var productRepository = CreateProductRepository();
            var createProductArgsValidator = new CreateProductArgsValidator(productRepository);
            var updateProductArgsValidator = new UpdateProductArgsValidator(productRepository);
            return new ProductConfigurationService(CreateMapper(), productRepository, createProductArgsValidator, updateProductArgsValidator);
        }

        public static IProductMarkdownConfigurationService CreateProductMarkdownConfigurationService()
        {
            var markdownFactory = new Markdown.Factory(new BasicDateTimeProvider());
            var productRepository = CreateProductRepository();
            var upsertProductMarkdownArgsValidator = new UpsertProductMarkdownArgsValidator(productRepository);

            return new ProductMarkdownConfigurationService(CreateMapper(), markdownFactory, productRepository, upsertProductMarkdownArgsValidator);
        }

        public static IProductRepository CreateProductRepository() => new InMemoryProductRepositoryFactory().CreateSeededRepository();

        public static IProductService CreateProductService()
        {
            return new ProductService(CreateMapper(), CreateProductRepository());
        }

        public static IProductSpecialConfigurationService CreateProductSpecialConfigurationService()
        {
            var productRepository = CreateProductRepository();
            var createSpecialArgsValidator = new CreateSpecialArgsValidator(productRepository);
            var createBuyNForXAmountSpecialArgsValidator = new CreateBuyNForXAmountSpecialArgsValidator(productRepository, createSpecialArgsValidator);
            var createBuyNGetMAtXPercentOffSpecialArgsValidator = new CreateBuyNGetMAtXPercentOffSpecialArgsValidator(productRepository, createSpecialArgsValidator);
            var createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator = new CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator(productRepository, createSpecialArgsValidator);

            return new ProductSpecialConfigurationService(CreateMapper(), productRepository, createBuyNForXAmountSpecialArgsValidator, createBuyNGetMAtXPercentOffSpecialArgsValidator, createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator);
        }
    }
}
