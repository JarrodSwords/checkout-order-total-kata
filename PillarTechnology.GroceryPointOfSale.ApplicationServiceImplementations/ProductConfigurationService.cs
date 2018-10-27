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

        public ProductConfigurationService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
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