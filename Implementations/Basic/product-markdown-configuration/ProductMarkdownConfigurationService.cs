using AutoMapper;
using NodaMoney;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductMarkdownConfigurationService : IProductMarkdownConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly IMarkdownFactory _markdownFactory;
        private readonly IProductRepository _productRepository;
        private UpsertProductMarkdownArgsValidator _upsertProductMarkdownArgsValidator;

        public ProductMarkdownConfigurationService(
            IMapper mapper,
            IMarkdownFactory markdownFactory,
            IProductRepository productRepository,
            UpsertProductMarkdownArgsValidator upsertProductMarkdownArgsValidator)
        {
            _mapper = mapper;
            _markdownFactory = markdownFactory;
            _productRepository = productRepository;
            _upsertProductMarkdownArgsValidator = upsertProductMarkdownArgsValidator;
        }

        public ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args)
        {
            _upsertProductMarkdownArgsValidator.ValidateAndThrow<UpsertProductMarkdownArgs>(args);

            var product = _productRepository.FindProduct(args.ProductName);
            product.Markdown = _markdownFactory
                .Configure(
                    Money.USDollar(args.AmountOffRetail.Value),
                    args.EndTime.Value,
                    args.StartTime.Value
                )
                .CreateMarkdown();

            var persistedProduct = _productRepository.UpdateProduct(product);

            return _mapper.Map<ProductDto>(persistedProduct);
        }
    }
}
