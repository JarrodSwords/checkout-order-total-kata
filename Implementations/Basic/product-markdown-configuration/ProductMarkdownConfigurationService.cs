using AutoMapper;
using FluentValidation;
using NodaMoney;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductMarkdownConfigurationService : IProductMarkdownConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private UpsertProductMarkdownArgsValidator _upsertProductMarkdownArgsValidator;

        public ProductMarkdownConfigurationService(
            IMapper mapper,
            IProductRepository productRepository,
            UpsertProductMarkdownArgsValidator upsertProductMarkdownArgsValidator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _upsertProductMarkdownArgsValidator = upsertProductMarkdownArgsValidator;
        }

        public ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args)
        {
            _upsertProductMarkdownArgsValidator.ValidateAndThrow(args);

            var product = _productRepository.FindProduct(args.ProductName);
            product.Markdown = new Markdown(
                Money.USDollar(args.AmountOffRetail.Value),
                args.EndTime.Value,
                args.StartTime.Value
            );

            var persistedProduct = _productRepository.UpdateProduct(product);

            return persistedProduct.GetType() == typeof(EachesProduct) ?
                (ProductDto) _mapper.Map<EachesProductDto>(persistedProduct) :
                (ProductDto) _mapper.Map<MassProductDto>(persistedProduct);
        }
    }
}
