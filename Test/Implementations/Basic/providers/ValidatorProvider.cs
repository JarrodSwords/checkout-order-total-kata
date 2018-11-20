using PointOfSale.Domain;
using PointOfSale.Implementations;
using PointOfSale.Implementations.Basic;

namespace PointOfSale.Test.Implementations.Basic
{
    public static class ValidatorProvider
    {
        public static AmountOffRetailValidator AmountOffRetailValidator() =>
            new AmountOffRetailValidator();

        public static CreateProductArgsValidator CreateProductArgsValidator() =>
            new CreateProductArgsValidator(
                ProductMustNotExistValidator(),
                SellByTypeValidator(),
                IUpsertEachesProductArgsValidator(),
                IUpsertMassProductArgsValidator()
            );

        public static CreateSpecialArgsValidator CreateSpecialArgsValidator(IProductRepository productRepository = null)
        {
            productRepository = productRepository ?? DependencyProvider.ProductRepository();

            return new CreateSpecialArgsValidator(
                new DiscountedItemsValidator(),
                new GroupSalePriceValidator(),
                new IsEachesProductValidator(productRepository),
                new IsMassProductValidator(productRepository),
                new PercentageOffValidator(),
                new PreDiscountItemsValidator(),
                new ProductMustExistValidator(productRepository),
                new TemporalValidator()
            );
        }

        public static ProductMustExistValidator ProductMustExistValidator(IProductRepository productRepository = null) =>
            new ProductMustExistValidator(productRepository ?? DependencyProvider.ProductRepository());

        public static ProductMustNotExistValidator ProductMustNotExistValidator(IProductRepository productRepository = null) =>
            new ProductMustNotExistValidator(productRepository ?? DependencyProvider.ProductRepository());

        public static SellByTypeValidator SellByTypeValidator(IProductServiceProvider productFactoryProvider = null) =>
            new SellByTypeValidator(productFactoryProvider ?? DependencyProvider.ProductServiceProvider());

        public static IUpsertEachesProductArgsValidator IUpsertEachesProductArgsValidator() =>
            new IUpsertEachesProductArgsValidator();

        public static IUpsertMassProductArgsValidator IUpsertMassProductArgsValidator() =>
            new IUpsertMassProductArgsValidator();

        public static TemporalValidator TemporalValidator() =>
            new TemporalValidator();

        public static UpdateProductArgsValidator UpdateProductArgsValidator() =>
            new UpdateProductArgsValidator(
                ProductMustExistValidator(),
                SellByTypeValidator(),
                IUpsertEachesProductArgsValidator(),
                IUpsertMassProductArgsValidator()
            );

        public static UpsertProductMarkdownArgsValidator UpsertProductMarkdownArgsValidator(IProductRepository productRepository = null)
        {
            productRepository = productRepository ?? DependencyProvider.ProductRepository();

            return new UpsertProductMarkdownArgsValidator(
                AmountOffRetailValidator(),
                ProductMustExistValidator(),
                productRepository,
                SellByTypeValidator(),
                TemporalValidator()
            );
        }
    }
}
