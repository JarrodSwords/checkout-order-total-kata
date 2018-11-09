using System;
using AutoMapper;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductSpecialConfigurationServiceTest : IProductSpecialConfigurationServiceTest
    {
        public ProductSpecialConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var createBuyNGetMAtXPercentOffArgsValidator = new CreateSpecialArgsValidator(productRepository);

            _productSpecialConfigurationService = new ProductSpecialConfigurationService(mapper, productRepository, createBuyNGetMAtXPercentOffArgsValidator);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNForXAmountSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateBuyNForXAmountSpecialArgs { ProductName = productName };

            Action createSpecial = () => _productSpecialConfigurationService.CreateBuyNForXAmountSpecial(args);

            createSpecial.Should().Throw<ArgumentException>(message);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNGetMAtXPercentOffSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs { ProductName = productName };

            Action createSpecial = () => _productSpecialConfigurationService.CreateBuyNGetMAtXPercentOffSpecial(args);

            createSpecial.Should().Throw<ArgumentException>(message);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs { ProductName = productName };

            Action createSpecial = () => _productSpecialConfigurationService.CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(args);

            createSpecial.Should().Throw<ArgumentException>(message);
        }
    }
}
