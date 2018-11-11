using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.WebApi;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductControllerTest
    {
        private ProductController _productController;

        public ProductControllerTest()
        {
            var productConfigurationService = DependencyProvider.CreateProductConfigurationService();
            _productController = new ProductController(productConfigurationService);
        }

        [Fact]
        public void TestName()
        {
            var args = new UpsertProductArgs
            {
                Name = "something",
                RetailPrice = 1,
                SellByType = "Unit"
            };

            var productDto = _productController.CreateProduct(args).Value;
            
            productDto.Name.Should().Be(args.Name);
            productDto.RetailPrice.Should().Be(args.RetailPrice);
            productDto.SellByType.Should().Be(args.SellByType);
        }
    }
}
