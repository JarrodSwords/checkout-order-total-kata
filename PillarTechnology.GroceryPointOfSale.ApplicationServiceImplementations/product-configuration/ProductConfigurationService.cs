using System;
using System.Linq;
using AutoMapper;
using FluentValidation;
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
            Validate(dto, new CreateProductDtoValidator(_productRepository));

            var product = _mapper.Map<UpsertProductDto, Product>(dto);
            var persistedProduct = _productRepository.CreateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        public ProductDto UpdateProduct(UpsertProductDto dto)
        {
            Validate(dto, new UpdateProductDtoValidator(_productRepository));

            var product = _productRepository.FindProduct(dto.Name);
            _mapper.Map<UpsertProductDto, Product>(dto, product);
            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        public ProductDto UpsertProductMarkdown(UpsertProductMarkdownDto dto)
        {
            Validate(dto, new UpsertProductMarkdownDtoValidator(_productRepository));

            var product = _productRepository.FindProduct(dto.ProductName);
            
            if (product.Markdown == null)
                product.Markdown = _mapper.Map<UpsertProductMarkdownDto, Markdown>(dto);
            else
                _mapper.Map<UpsertProductMarkdownDto, Markdown>(dto, product.Markdown);

            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<Product, ProductDto>(persistedProduct);
        }

        private void Validate(object dto, IValidator validator)
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                throw new ArgumentException(String.Join(";\n", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}