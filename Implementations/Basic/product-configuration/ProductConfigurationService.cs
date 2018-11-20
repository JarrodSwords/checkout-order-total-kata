using AutoMapper;
using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductConfigurationService : IProductConfigurationService
    {
        #region Dependencies

        private readonly IProductRepository _productRepository;
        private readonly IProductServiceProvider _productServiceProvider;
        private readonly CreateProductArgsValidator _createProductArgsValidator;
        private readonly UpdateProductArgsValidator _updateProductArgsValidator;

        public ProductConfigurationService(
            IProductRepository productRepository,
            IProductServiceProvider productServiceProvider,
            CreateProductArgsValidator createProductArgsValidator,
            UpdateProductArgsValidator updateProductArgsValidator
        )
        {
            _productServiceProvider = productServiceProvider;
            _productRepository = productRepository;
            _createProductArgsValidator = createProductArgsValidator;
            _updateProductArgsValidator = updateProductArgsValidator;
        }

        #endregion Dependencies

        public ProductDto CreateProduct(UpsertProductArgs args)
        {
            _createProductArgsValidator.ValidateAndThrow(args);

            var productService = _productServiceProvider.GetService(args);
            var product = productService.Create();
            product = _productRepository.CreateProduct(product);
            return productService.CreateProductDto(product);
        }

        public ProductDto UpdateProduct(UpsertProductArgs args)
        {
            _updateProductArgsValidator.ValidateAndThrow(args);

            var product = _productRepository.FindProduct(args.ProductName);
            var productService = _productServiceProvider.GetService(args);
            product = productService.UpdateProduct(product);
            product = _productRepository.UpdateProduct(product);
            return productService.CreateProductDto(product);
        }
    }
}
