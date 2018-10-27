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

        [Fact]
        public void CreateProduct_CreatesPersistedProduct()
        {
            var product = new Product("milk");
            
            _productRepository.CreateProduct(product);

            var persistedProduct = _productRepository.FindProduct(product.Name);            
            persistedProduct.Should().Be(product);
        }

        [Fact]
        public void CreateProduct_WhenProductExists_ThrowsArgumentException()
        {
            var products = new ProductTestData().GetEnumerator();
            products.MoveNext();
            _productRepository.CreateProduct(products.Current);

            Action addExistingProduct = () => _productRepository.CreateProduct(products.Current);

            addExistingProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product already exists");
        }
    }
}