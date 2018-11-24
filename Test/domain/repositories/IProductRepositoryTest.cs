using FluentAssertions;
using NodaMoney;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test.Domain
{
    public abstract class IProductRepositoryTest
    {
        protected IProductRepository _productRepository;

        [Fact]
        public void CreateProduct_CreatesPersistedProduct()
        {
            var product = new EachesProduct("milk", 1.99m);

            var persistedProduct = _productRepository.CreateProduct(product);
            persistedProduct.Should().Be(product);
        }

        [Theory]
        [ClassData(typeof(ProductProvider))]
        public void UpdateProduct_UpdatesNonIdentityFieldsInPersistedProduct(string productName)
        {
            var retailPrice = Money.USDollar(0.89m);

            var product = _productRepository.FindProduct(productName);
            product.RetailPrice = retailPrice;
            _productRepository.UpdateProduct(product);

            var persistedProduct = _productRepository.FindProduct(product.Name);
            persistedProduct.RetailPrice.Should().Be(retailPrice);
        }
    }
}
