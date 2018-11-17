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
            var buyNForXAmountConfigurationService = DependencyProvider.CreateBuyNForXAmountConfigurationService();
            var buyNGetMAtXPercentOffConfigurationService = DependencyProvider.CreateBuyNGetMAtXPercentOffConfigurationService();
            var buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService = DependencyProvider.CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService();
            var productSpecialConfigurationServiceProvider = new ProductSpecialConfigurationServiceProvider(
                buyNForXAmountConfigurationService,
                buyNGetMAtXPercentOffConfigurationService,
                buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService
            );
            var productConfigurationService = DependencyProvider.CreateProductConfigurationService();
            var productMarkdownConfigurationService = DependencyProvider.CreateProductMarkdownConfigurationService();
            var productService = DependencyProvider.CreateProductService();
            _productController = new ProductController(
                productConfigurationService,
                productMarkdownConfigurationService,
                productService,
                productSpecialConfigurationServiceProvider
            );
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
