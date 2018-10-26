using System;
using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductRepositoryTest
    {
        protected Product _product;
        protected IProductRepository _productRepository;

        public IProductRepositoryTest()
        {
            _product = new Product { Name = "can of soup" };
        }

        [Fact]
        public void CreateProduct_WhenProductDoesNotExist_AddsProductToStorage()
        {
            _productRepository.CreateProduct(_product);

            var storedProduct = _productRepository.FindProduct(_product.Name);
            storedProduct.Should().Be(_product);
        }

        [Fact]
        public void CreateProduct_WhenProductExists_ThrowsArgumentException()
        {
            _productRepository.CreateProduct(_product);

            Action addExistingProduct = () => _productRepository.CreateProduct(_product);

            addExistingProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product already exists");
        }
    }
}