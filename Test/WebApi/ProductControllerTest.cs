using FluentAssertions;
using PointOfSale.Implementations.Basic;
using PointOfSale.Services;
using PointOfSale.Test.Implementations.Basic;
using PointOfSale.WebApi;
using Xunit;

namespace PointOfSale.Test.WebApi
{
    public class ProductControllerTest
    {
        private ProductController _productController;

        public ProductControllerTest()
        {
            _productController = new ProductController(
                DependencyProvider.ProductConfigurationService(),
                DependencyProvider.ProductMarkdownConfigurationService(),
                DependencyProvider.CreateProductService(),
                DependencyProvider.ProductSpecialConfigurationService()
            );
        }

        [Fact]
        public void TestName()
        {
            var args = new UpsertProductArgs
            {
                ProductName = "something",
                RetailPrice = 1,
                SellByType = "eaches"
            };

            var productDto = _productController.CreateProduct(args).Value;

            productDto.Name.Should().Be(args.ProductName);
            // productDto.RetailPrice.Should().Be(args.RetailPrice);
            productDto.SellByType.Should().Be(args.SellByType);
        }
    }
}
