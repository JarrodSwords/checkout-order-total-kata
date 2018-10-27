using System;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CreateProductValidator : ICreateProductValidator
    {
        private readonly IProductRepository _productRepository;

        public CreateProductValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Validate(ProductDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new ArgumentException("Product name is required");

            if (productDto.RetailPrice == null)
                throw new ArgumentException("Product retail price is required");

            if (_productRepository.Exists(productDto.Name))
                throw new ArgumentException("Product already exists");
        }
    }
}