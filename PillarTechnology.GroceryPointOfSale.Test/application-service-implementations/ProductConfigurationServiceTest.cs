using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductConfigurationServiceTest : IProductConfigurationServiceTest
    {
        public ProductConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var seededProductRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();

            _productConfigurationService = new ProductConfigurationService(mapper, seededProductRepository);
            _productService = new ProductService(mapper, seededProductRepository);
        }
    }
}