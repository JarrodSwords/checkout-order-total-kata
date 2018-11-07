using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class ProductSpecialConfigurationService : IProductSpecialConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductSpecialConfigurationService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public ProductDto CreateBuyNGetMAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            var product = _productRepository.FindProduct(args.ProductName);
            product.Special = new BuyNGetMAtXPercentOffSpecial(args.StartTime, args.EndTime, args.PreDiscountItems.Value, args.DiscountedItems.Value, args.PercentageOff.Value, args.Limit);
            var persistedProduct = _productRepository.UpdateProduct(product);
            var productDto = _mapper.Map<ProductDto>(persistedProduct);
            productDto.Special = _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(persistedProduct.Special);
            return productDto;
        }
    }
}
