using System;
using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductRepositoryTest
    {
        protected IProductRepository _productRepository;
        protected Product _stubProduct;

        public IProductRepositoryTest()
        {
            _stubProduct = new Product { Name = "can of soup" };
        }

        [Fact]
        public void CreateProduct_WhenProductDoesNotExist_AddsProductToStorage()
        {
            _productRepository.CreateProduct(_stubProduct);

            var storedProduct = _productRepository.FindProduct(_stubProduct.Name);
            storedProduct.Should().Be(_stubProduct);
        }

        [Fact]
        public void CreateProduct_WhenProductExists_ThrowsArgumentException()
        {
            _productRepository.CreateProduct(_stubProduct);

            Action addExistingProduct = () => _productRepository.CreateProduct(_stubProduct);

            addExistingProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product already exists");
        }
    }
}