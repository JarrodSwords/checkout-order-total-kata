using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class ProductConfigurationService : IProductConfigurationService
    {
        #region Dependencies

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private CreateProductArgsValidator _createProductArgsValidator;
        private UpdateProductArgsValidator _updateProductArgsValidator;

        public ProductConfigurationService(IMapper mapper, IProductRepository productRepository, CreateProductArgsValidator createProductArgsValidator, UpdateProductArgsValidator updateProductArgsValidator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _createProductArgsValidator = createProductArgsValidator;
            _updateProductArgsValidator = updateProductArgsValidator;
        }

        #endregion Dependencies

        public ProductDto CreateProduct(UpsertProductArgs args)
        {
            _createProductArgsValidator.ValidateAndThrow<UpsertProductArgs>(args);

            var product = _mapper.Map<Product>(args);
            var persistedProduct = _productRepository.CreateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }

        public ProductDto UpdateProduct(UpsertProductArgs args)
        {
            _updateProductArgsValidator.ValidateAndThrow<UpsertProductArgs>(args);

            var product = _productRepository.FindProduct(args.Name);
            _mapper.Map<UpsertProductArgs, Product>(args, product);
            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }
    }
}
