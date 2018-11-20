using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Implementations;
using PointOfSale.Implementations.Basic;
using PointOfSale.Services;
using PointOfSale.Test.Infrastructure.InMemory;

namespace PointOfSale.Test.Implementations.Basic
{
    public class DependencyProvider
    {
        public static ICheckoutService CheckoutService(IOrderRepository orderRepository)
        {
            var productRepository = ProductRepository();
            var removeScannedItemArgsValidator = new RemoveScannedItemArgsValidator(orderRepository);
            var scanItemArgsValidator = new ScanItemArgsValidator(productRepository);
            var scanWeightedItemArgsValidator = new ScanWeightedItemArgsValidator(productRepository);

            return new CheckoutService(Mapper(), orderRepository, productRepository, removeScannedItemArgsValidator, scanItemArgsValidator, scanWeightedItemArgsValidator);
        }

        public static IDateTimeProvider DateTimeProvider() => new BasicDateTimeProvider();

        public static IMapper Mapper() => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));

        public static IOrderRepository OrderRepository() => new InMemoryOrderRepositoryFactory().CreateSeededRepository();

        public static IOrderService OrderService()
        {
            return new OrderService(Mapper(), OrderRepository());
        }

        public static IProductConfigurationService ProductConfigurationService() =>
            new ProductConfigurationService(
                ProductRepository(),
                ProductServiceProvider(),
                ValidatorProvider.CreateProductArgsValidator(),
                ValidatorProvider.UpdateProductArgsValidator()
            );

        public static IProductServiceProvider ProductServiceProvider() =>
            new ProductServiceProvider(Mapper());

        public static ISpecialServiceProvider SpecialServiceProvider() =>
            new SpecialServiceProvider(
                DateTimeProvider(),
                Mapper()
            );

        public static IProductMarkdownConfigurationService ProductMarkdownConfigurationService()
        {
            var markdownFactory = new Markdown.Factory(new BasicDateTimeProvider());
            var productRepository = ProductRepository();

            return new ProductMarkdownConfigurationService(
                Mapper(),
                markdownFactory,
                productRepository,
                ValidatorProvider.UpsertProductMarkdownArgsValidator(productRepository)
            );
        }

        public static IProductSpecialConfigurationService ProductSpecialConfigurationService() =>
            new ProductSpecialConfigurationService(
                Mapper(),
                ProductRepository(),
                ProductServiceProvider(),
                SpecialServiceProvider(),
                ValidatorProvider.CreateSpecialArgsValidator()
            );

        public static IProductRepository ProductRepository() =>
            new InMemoryProductRepositoryFactory().CreateSeededRepository();

        public static IProductService ProductService() =>
            new PointOfSale.Implementations.Basic.ProductService(Mapper(), ProductRepository());
    }
}
