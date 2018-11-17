using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Implementations.Basic;
using PointOfSale.Services;

namespace PointOfSale.Test
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
