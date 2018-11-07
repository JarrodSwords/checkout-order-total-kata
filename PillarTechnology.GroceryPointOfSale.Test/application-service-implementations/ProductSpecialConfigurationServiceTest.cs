using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductSpecialConfigurationServiceTest : IProductSpecialConfigurationServiceTest
    {
        public ProductSpecialConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();

            _productSpecialConfigurationService = new ProductSpecialConfigurationService(mapper, productRepository);
        }
    }
}