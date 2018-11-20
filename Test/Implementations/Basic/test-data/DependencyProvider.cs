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

        public static IProductConfigurationService CreateProductConfigurationService()
        {
            var mapper = CreateMapper();
            var productRepository = CreateProductRepository();

            var createProductNameValidator = new CreateProductNameValidator(productRepository);
            var productFactoryProvider = new ProductFactoryProvider(mapper);
            var sellByTypeValidator = new SellByTypeValidator(productFactoryProvider);
            var retailPriceValidator = new RetailPriceValidator();
            var iUpsertMassProductArgsValidator = new IUpsertMassProductArgsValidator();
            var createProductArgsValidator = new CreateProductArgsValidator(createProductNameValidator, sellByTypeValidator, retailPriceValidator, iUpsertMassProductArgsValidator);

            var updateProductNameValidator = new UpdateProductNameValidator(productRepository);
            var updateProductArgsValidator = new UpdateProductArgsValidator(updateProductNameValidator, sellByTypeValidator, retailPriceValidator, iUpsertMassProductArgsValidator);

            return new ProductConfigurationService(productFactoryProvider, productRepository, createProductArgsValidator, updateProductArgsValidator);
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

        public static BuyNForXAmountConfigurationService CreateBuyNForXAmountConfigurationService()
        {
            var productRepository = CreateProductRepository();
            var createSpecialArgsValidator = new CreateSpecialArgsValidator(productRepository);

            return new BuyNForXAmountConfigurationService(
                CreateMapper(),
                productRepository,
                new BuyNForXAmountSpecial.Factory(new BasicDateTimeProvider()),
                new CreateBuyNForXAmountSpecialArgsValidator(productRepository, createSpecialArgsValidator)
            );
        }

        public static BuyNGetMAtXPercentOffConfigurationService CreateBuyNGetMAtXPercentOffConfigurationService()
        {
            var productRepository = CreateProductRepository();
            var createSpecialArgsValidator = new CreateSpecialArgsValidator(productRepository);

            return new BuyNGetMAtXPercentOffConfigurationService(
                CreateMapper(),
                productRepository,
                new BuyNGetMAtXPercentOffSpecial.Factory(new BasicDateTimeProvider()),
                new CreateBuyNGetMAtXPercentOffSpecialArgsValidator(productRepository, createSpecialArgsValidator)
            );
        }

        public static BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService()
        {
            var productRepository = CreateProductRepository();
            var createSpecialArgsValidator = new CreateSpecialArgsValidator(productRepository);

            return new BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService(
                CreateMapper(),
                productRepository,
                new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory(new BasicDateTimeProvider()),
                new CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator(productRepository, createSpecialArgsValidator)
            );
        }
    }
}
