using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
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
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var createProductArgsValidator = new CreateProductArgsValidator(productRepository);
            var updateProductArgsValidator = new UpdateProductArgsValidator(productRepository);
            var productConfigurationService = new ProductConfigurationService(mapper, productRepository, createProductArgsValidator, updateProductArgsValidator);
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
