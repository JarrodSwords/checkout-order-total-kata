using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductConfigurationServiceTest : IProductConfigurationServiceTest
    {
        public ProductConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var createProductArgsValidator = new CreateProductArgsValidator(productRepository);
            var updateProductArgsValidator = new UpdateProductArgsValidator(productRepository);
            var upsertProductMarkdownArgsValidator = new UpsertProductMarkdownArgsValidator(productRepository);

            _productConfigurationService = new ProductConfigurationService(mapper, productRepository, createProductArgsValidator, updateProductArgsValidator, upsertProductMarkdownArgsValidator);
            _productService = new ProductService(mapper, productRepository);
        }
    }
}