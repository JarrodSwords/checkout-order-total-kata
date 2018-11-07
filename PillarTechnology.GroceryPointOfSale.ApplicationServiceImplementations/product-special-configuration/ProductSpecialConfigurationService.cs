using System;
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

        public ProductDto CreateBuyNForXAmountSpecial(CreateBuyNForXAmountSpecialArgs args)
        {
            Func<Special> createSpecial = () => new BuyNForXAmountSpecial(args.StartTime, args.EndTime, args.DiscountedItems.Value, args.GroupSalePrice.Value, args.Limit);
            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNForXAmountSpecialDto>(special);
            return CreateSpecial(args, createSpecial, mapToSpecialDto);
        }

        public ProductDto CreateBuyNGetMAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            Func<Special> createSpecial = () => new BuyNGetMAtXPercentOffSpecial(args.StartTime, args.EndTime, args.PreDiscountItems.Value, args.DiscountedItems.Value, args.PercentageOff.Value, args.Limit);
            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(special);
            return CreateSpecial(args, createSpecial, mapToSpecialDto);
        }

        private ProductDto CreateSpecial(CreateSpecialArgs args, Func<Special> createSpecial, Func<Special, ISpecialDto> mapToSpecialDto)
        {
            var product = _productRepository.FindProduct(args.ProductName);

            product.Special = createSpecial();
            var persistedProduct = _productRepository.UpdateProduct(product);

            var productDto = _mapper.Map<ProductDto>(persistedProduct);
            productDto.Special = mapToSpecialDto(product.Special);
            return productDto;
        }
    }
}
