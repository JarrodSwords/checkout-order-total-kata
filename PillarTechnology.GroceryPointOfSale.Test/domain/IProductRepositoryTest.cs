using System;
using FluentAssertions;
using Moq;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductRepositoryTest
    {
        protected IProductRepository _productRepository;
        private ProductTestData _productTestData = new ProductTestData();

        [Fact]
        public void CreateProduct_CreatesPersistedProduct()
        {
            var product = new Product("milk", Money.USDollar(1.99m));

            _productRepository.CreateProduct(product);

            var persistedProduct = _productRepository.FindProduct(product.Name);
            persistedProduct.Should().Be(product);
        }

        [Fact]
        public void CreateProduct_WhenProductExists_ThrowsArgumentException()
        {
            _productTestData.SeedRepository(ref _productRepository);
            var existingProduct = _productTestData.GetProductSoldByUnit();

            Action addExistingProduct = () => _productRepository.CreateProduct(existingProduct);

            addExistingProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product already exists");
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void UpdateProduct_UpdatesNonIdentityFieldsInPersistedProduct(string productName)
        {
            _productTestData.SeedRepository(ref _productRepository);
            var retailPrice = Money.USDollar(0.89m);

            var product = _productRepository.FindProduct(productName);
            product.RetailPrice = retailPrice;
            _productRepository.UpdateProduct(product);

            var persistedProduct = _productRepository.FindProduct(product.Name);
            persistedProduct.RetailPrice.Should().Be(retailPrice);
        }
    }
}