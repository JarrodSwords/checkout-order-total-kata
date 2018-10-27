using System;
using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class ProductConfigurationService : IProductConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICreateProductValidator _productValidator;

        public ProductConfigurationService(IMapper mapper, IProductRepository productRepository, ICreateProductValidator productValidator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productValidator = productValidator;
        }

        public ProductDto CreateProduct(ProductDto productDto)
        {
            _productValidator.Validate(productDto);

            var product = _mapper.Map<ProductDto, Product>(productDto);
            var persistedProduct = _productRepository.CreateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        public ProductDto UpdateProduct(ProductDto productDto)
        {
            var product = _productRepository.FindProduct(productDto.Name);
            _mapper.Map<ProductDto, Product>(productDto, product);
            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }
    }
}