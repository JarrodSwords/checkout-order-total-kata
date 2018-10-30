using System;
using System.Linq;
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

        public ProductDto CreateProduct(CreateProductDto createProductDto)
        {
            ValidateCreateProductDto(createProductDto);
            
            var product = _mapper.Map<CreateProductDto, Product>(createProductDto);
            var persistedProduct = _productRepository.CreateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        private void ValidateCreateProductDto(CreateProductDto createProductDto)
        {
            var validator = new CreateProductDtoValidator(_productRepository);
            var validationResult = validator.Validate(createProductDto);
            
            if (!validationResult.IsValid)
                throw new ArgumentException(String.Join(";\n", validationResult.Errors.Select(x => x.ErrorMessage)));
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