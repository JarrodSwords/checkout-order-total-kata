using AutoMapper;
using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductConfigurationServiceTest : IProductConfigurationServiceTest
    {
        public ProductConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var seededProductRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var productValidator = new CreateProductValidator(seededProductRepository);

            _productConfigurationService = new ProductConfigurationService(mapper, seededProductRepository, productValidator);
            _productService = new ProductService(mapper, seededProductRepository);
        }
    }
}