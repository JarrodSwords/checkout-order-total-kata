using System;
using AutoMapper;
using FluentValidation;
using GroceryPointOfSale.ApplicationServices;
using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.ApplicationServiceImplementations
{
    public abstract partial class ProductSpecialConfigurationService : IProductSpecialConfigurationService
    {
        protected readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateSpecialArgs> _validator;

        public ProductSpecialConfigurationService(IMapper mapper, IProductRepository productRepository, IValidator<CreateSpecialArgs> validator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _validator = validator;
        }

        public ProductDto CreateSpecial(CreateSpecialArgs args)
        {
            _validator.ValidateAndThrow<CreateSpecialArgs>(args);

            var product = _productRepository.FindProduct(args.ProductName);
            var specialFactory = GetConfiguredSpecialFactory(args);
            product.Special = specialFactory.CreateSpecial();

            var persistedProduct = _productRepository.UpdateProduct(product);

            var productDto = _mapper.Map<ProductDto>(persistedProduct);
            productDto.Special = CreateSpecialDto(persistedProduct.Special);
            return productDto;
        }

        public abstract ISpecialFactory GetConfiguredSpecialFactory(CreateSpecialArgs args);
        public abstract ISpecialDto CreateSpecialDto(Special special);
    }
}
