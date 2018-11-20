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
            var productRepository = CreateProductRepository();
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
                CreateProductRepository(),
                ProductFactoryProvider(),
                ValidatorProvider.CreateProductArgsValidator(),
                ValidatorProvider.UpdateProductArgsValidator()
            );

        public static IProductServiceProvider ProductFactoryProvider() =>
            new ProductServiceProvider(Mapper());

        public static ISpecialServiceProvider SpecialServiceProvider() =>
            new SpecialServiceProvider(
                DateTimeProvider(),
                Mapper()
            );

        public static IProductMarkdownConfigurationService ProductMarkdownConfigurationService()
        {
            var markdownFactory = new Markdown.Factory(new BasicDateTimeProvider());
            var productRepository = CreateProductRepository();
            var upsertProductMarkdownArgsValidator = new UpsertProductMarkdownArgsValidator(productRepository, ValidatorProvider.CreateTemporalValidator());

            return new ProductMarkdownConfigurationService(Mapper(), markdownFactory, productRepository, upsertProductMarkdownArgsValidator);
        }

        public static IProductSpecialConfigurationService ProductSpecialConfigurationService() =>
            new ProductSpecialConfigurationService(
                Mapper(),
                CreateProductRepository(),
                ProductFactoryProvider(),
                SpecialServiceProvider(),
                ValidatorProvider.CreateSpecialArgsValidator()
            );

        public static IProductRepository CreateProductRepository() => new InMemoryProductRepositoryFactory().CreateSeededRepository();

        public static IProductService CreateProductService()
        {
            return new PointOfSale.Implementations.Basic.ProductService(Mapper(), CreateProductRepository());
        }
    }
}
