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
        public static ICheckoutService CreateCheckoutService(IOrderRepository orderRepository)
        {
            var productRepository = CreateProductRepository();
            var removeScannedItemArgsValidator = new RemoveScannedItemArgsValidator(orderRepository);
            var scanItemArgsValidator = new ScanItemArgsValidator(productRepository);
            var scanWeightedItemArgsValidator = new ScanWeightedItemArgsValidator(productRepository);

            return new CheckoutService(CreateMapper(), orderRepository, productRepository, removeScannedItemArgsValidator, scanItemArgsValidator, scanWeightedItemArgsValidator);
        }

        public static IDateTimeProvider CreateDateTimeProvider() => new BasicDateTimeProvider();

        public static IMapper CreateMapper() => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));

        public static IOrderRepository CreateOrderRepository() => new InMemoryOrderRepositoryFactory().CreateSeededRepository();

        public static IOrderService CreateOrderService()
        {
            return new OrderService(CreateMapper(), CreateOrderRepository());
        }

        public static IProductConfigurationService ProductConfigurationService() =>
            new ProductConfigurationService(
                ProductFactoryProvider(),
                CreateProductRepository(),
                ValidatorProvider.CreateProductArgsValidator(),
                ValidatorProvider.UpdateProductArgsValidator()
            );

        public static IProductFactoryProvider ProductFactoryProvider() =>
            new ProductFactoryProvider(CreateMapper());

        public static ISpecialServiceProvider SpecialServiceProvider() =>
            new SpecialServiceProvider(
                CreateDateTimeProvider(),
                CreateMapper()
            );

        public static IProductMarkdownConfigurationService CreateProductMarkdownConfigurationService()
        {
            var markdownFactory = new Markdown.Factory(new BasicDateTimeProvider());
            var productRepository = CreateProductRepository();
            var upsertProductMarkdownArgsValidator = new UpsertProductMarkdownArgsValidator(productRepository, CreateTemporalValidator());

            return new ProductMarkdownConfigurationService(CreateMapper(), markdownFactory, productRepository, upsertProductMarkdownArgsValidator);
        }

        public static IProductSpecialConfigurationService CreateProductSpecialConfigurationService() =>
            new ProductSpecialConfigurationService(
                CreateMapper(),
                ProductFactoryProvider(),
                CreateProductRepository(),
                SpecialServiceProvider(),
                ValidatorProvider.CreateSpecialArgsValidator()
            );

        public static ProductMustExistValidator CreateProductNameExistsValidator(IProductRepository productRepository = null) =>
            new ProductMustExistValidator(productRepository ?? CreateProductRepository());

        public static IProductRepository CreateProductRepository() => new InMemoryProductRepositoryFactory().CreateSeededRepository();

        public static IProductService CreateProductService()
        {
            return new ProductService(CreateMapper(), CreateProductRepository());
        }

        public static TemporalValidator CreateTemporalValidator() =>
            new TemporalValidator();
    }
}
