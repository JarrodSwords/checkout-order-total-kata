using System;
using AutoMapper;
using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductSpecialConfigurationService : IProductSpecialConfigurationService
    {
        private readonly IValidator<CreateSpecialArgs> _createSpecialArgsValidator;
        protected readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductFactoryProvider _productFactoryProvider;
        private readonly ISpecialServiceProvider _specialServiceProvider;

        public ProductSpecialConfigurationService(
            IMapper mapper,
            IProductFactoryProvider productFactoryProvider,
            IProductRepository productRepository,
            ISpecialServiceProvider specialServiceProvider,
            IValidator<CreateSpecialArgs> createSpecialArgsValidator
        )
        {
            _createSpecialArgsValidator = createSpecialArgsValidator;
            _mapper = mapper;
            _productFactoryProvider = productFactoryProvider;
            _productRepository = productRepository;
            _specialServiceProvider = specialServiceProvider;
        }

        public ProductDto CreateSpecial(CreateSpecialArgs args)
        {
            _createSpecialArgsValidator.ValidateAndThrow(args);

            var product = _productRepository.FindProduct(args.ProductName);
            var specialService = _specialServiceProvider.GetService(args);

            product.Special = specialService.Create();

            var persistedProduct = _productRepository.UpdateProduct(product);
            var dummy = new UpsertProductArgs()
            {
                SellByType = persistedProduct.GetType() == typeof(EachesProduct) ? "eaches" : "mass"
            };

            var productDto = _productFactoryProvider.GetFactory(dummy).CreateProductDto(persistedProduct);
            productDto.Special = specialService.ToDto(persistedProduct.Special);
            return productDto;
        }
    }
}
