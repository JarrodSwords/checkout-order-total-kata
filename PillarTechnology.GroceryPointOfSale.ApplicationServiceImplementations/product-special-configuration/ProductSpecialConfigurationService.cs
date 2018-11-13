using System;
using AutoMapper;
using FluentValidation;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public abstract class ProductSpecialConfigurationService<T> : IProductSpecialConfigurationService<T>
    {
        protected readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<T> _validator;

        public ProductSpecialConfigurationService(IMapper mapper, IProductRepository productRepository, IValidator<T> validator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _validator = validator;
        }

        public ProductDto CreateSpecial(T args)
        {
            _validator.ValidateAndThrow<T>(args);

            var product = _productRepository.FindProduct(GetProductName(args));
            var specialFactory = GetConfiguredSpecialFactory(args);
            product.Special = specialFactory.CreateSpecial();

            var persistedProduct = _productRepository.UpdateProduct(product);

            var productDto = _mapper.Map<ProductDto>(persistedProduct);
            productDto.Special = CreateSpecialDto(persistedProduct.Special);
            return productDto;
        }

        public abstract ISpecialFactory GetConfiguredSpecialFactory(T args);
        public abstract ISpecialDto CreateSpecialDto(Special special);
        public abstract string GetProductName(T args);
    }
}
