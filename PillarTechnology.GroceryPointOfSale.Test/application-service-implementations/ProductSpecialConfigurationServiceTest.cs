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
            var createBuyNGetMAtXPercentOffArgsValidator = new CreateSpecialArgsValidator(productRepository);

            _productSpecialConfigurationService = new ProductSpecialConfigurationService(mapper, productRepository, createBuyNGetMAtXPercentOffArgsValidator);
        }
    }
}