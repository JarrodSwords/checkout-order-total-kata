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
        #region Dependencies

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private CreateProductArgsValidator _createProductArgsValidator;
        private UpdateProductArgsValidator _updateProductArgsValidator;
        private UpsertProductMarkdownArgsValidator _upsertProductMarkdownArgsValidator;

        public ProductConfigurationService(IMapper mapper, IProductRepository productRepository, CreateProductArgsValidator createProductArgsValidator, UpdateProductArgsValidator updateProductArgsValidator, UpsertProductMarkdownArgsValidator upsertProductMarkdownArgsValidator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _createProductArgsValidator = createProductArgsValidator;
            _updateProductArgsValidator = updateProductArgsValidator;
            _upsertProductMarkdownArgsValidator = upsertProductMarkdownArgsValidator;
        }

        #endregion Dependencies

        public ProductDto CreateProduct(UpsertProductArgs args)
        {
            _createProductArgsValidator.ValidateAndThrow<UpsertProductArgs>(args);

            var product = _mapper.Map<Product>(args);
            var persistedProduct = _productRepository.CreateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }

        public ProductDto UpdateProduct(UpsertProductArgs args)
        {
            _updateProductArgsValidator.ValidateAndThrow<UpsertProductArgs>(args);

            var product = _productRepository.FindProduct(args.Name);
            _mapper.Map<UpsertProductArgs, Product>(args, product);
            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }

        public ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args)
        {
            _upsertProductMarkdownArgsValidator.ValidateAndThrow<UpsertProductMarkdownArgs>(args);

            var product = _productRepository.FindProduct(args.ProductName);

            if (product.Markdown == null)
                product.Markdown = _mapper.Map<Markdown>(args);
            else
                _mapper.Map<UpsertProductMarkdownArgs, Markdown>(args, product.Markdown);

            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }
    }
}