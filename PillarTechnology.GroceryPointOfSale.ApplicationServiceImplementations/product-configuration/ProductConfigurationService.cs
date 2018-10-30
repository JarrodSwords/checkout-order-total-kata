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

        public ProductDto CreateProduct(UpsertProductDto dto)
        {
            ValidateCreateProductDto(dto);

            var product = _mapper.Map<UpsertProductDto, Product>(dto);
            var persistedProduct = _productRepository.CreateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        public ProductDto UpdateProduct(UpsertProductDto dto)
        {
            ValidateUpdateProductDto(dto);

            var product = _productRepository.FindProduct(dto.Name);
            _mapper.Map<UpsertProductDto, Product>(dto, product);
            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        private void ValidateCreateProductDto(UpsertProductDto dto)
        {
            var validator = new CreateProductDtoValidator(_productRepository);
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                throw new ArgumentException(String.Join(";\n", validationResult.Errors.Select(x => x.ErrorMessage)));
        }

        private void ValidateUpdateProductDto(UpsertProductDto dto)
        {
            var validator = new UpdateProductDtoValidator(_productRepository);
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                throw new ArgumentException(String.Join(";\n", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}