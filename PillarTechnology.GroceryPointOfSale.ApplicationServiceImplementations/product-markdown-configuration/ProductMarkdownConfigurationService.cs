using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class ProductMarkdownConfigurationService : IProductMarkdownConfigurationService
    {
        #region Dependencies

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private UpsertProductMarkdownArgsValidator _upsertProductMarkdownArgsValidator;

        public ProductMarkdownConfigurationService(IMapper mapper, IProductRepository productRepository, UpsertProductMarkdownArgsValidator upsertProductMarkdownArgsValidator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _upsertProductMarkdownArgsValidator = upsertProductMarkdownArgsValidator;
        }

        #endregion Dependencies

        public ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args)
        {
            _upsertProductMarkdownArgsValidator.ValidateAndThrow<UpsertProductMarkdownArgs>(args);

            var product = _productRepository.FindProduct(args.ProductName);

            if (product.Markdown == null)
                product.Markdown = _mapper.Map<Markdown>(args);
            else
                _mapper.Map<UpsertProductMarkdownArgs, Markdown>(args, product.Markdown);

            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }
    }
}
