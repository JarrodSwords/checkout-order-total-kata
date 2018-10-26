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
        public void CreateProduct_WhenProductDoesNotExist_AddsProductToStorage()
        {
            var product = new Product { Name = "can of soup" };
            
            _productRepository.CreateProduct(product);

            var storedProduct = _productRepository.FindProduct(product.Name);
            storedProduct.Should().Be(product);
        }
    }
}