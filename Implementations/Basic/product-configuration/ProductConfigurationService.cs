using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductConfigurationService : IProductConfigurationService
    {
        #region Dependencies

        private readonly IProductFactoryProvider _productFactoryProvider;
        private readonly IProductRepository _productRepository;
        private readonly CreateProductArgsValidator _createProductArgsValidator;
        private readonly UpdateProductArgsValidator _updateProductArgsValidator;

        public ProductConfigurationService(
            IProductFactoryProvider productFactoryProvider,
            IProductRepository productRepository,
            CreateProductArgsValidator createProductArgsValidator,
            UpdateProductArgsValidator updateProductArgsValidator
        )
        {
            _productFactoryProvider = productFactoryProvider;
            _productRepository = productRepository;
            _createProductArgsValidator = createProductArgsValidator;
            _updateProductArgsValidator = updateProductArgsValidator;
        }

        #endregion Dependencies

        public ProductDto CreateProduct(UpsertProductArgs args)
        {
            _createProductArgsValidator.ValidateAndThrow<UpsertProductArgs>(args);

            var productFactory = _productFactoryProvider.GetFactory(args);
            var product = productFactory.Create();
            product = _productRepository.CreateProduct(product);
            return productFactory.CreateProductDto(product);
        }

        public ProductDto UpdateProduct(UpsertProductArgs args)
        {
            _updateProductArgsValidator.ValidateAndThrow<UpsertProductArgs>(args);

            var product = _productRepository.FindProduct(args.ProductName);
            var productFactory = _productFactoryProvider.GetFactory(args);
            product = productFactory.UpdateProduct(product);
            product = _productRepository.UpdateProduct(product);
            return productFactory.CreateProductDto(product);
        }
    }
}
