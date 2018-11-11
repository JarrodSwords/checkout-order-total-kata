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
        private readonly CreateBuyNForXAmountSpecialArgsValidator _createBuyNForXAmountSpecialArgsValidator;
        private readonly CreateBuyNGetMAtXPercentOffSpecialArgsValidator _createBuyNGetMAtXPercentOffSpecialArgsValidator;
        private readonly CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator _createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator;

        public ProductSpecialConfigurationService(
            IMapper mapper,
            IProductRepository productRepository,
            CreateBuyNForXAmountSpecialArgsValidator createBuyNForXAmountSpecialArgsValidator,
            CreateBuyNGetMAtXPercentOffSpecialArgsValidator createBuyNGetMAtXPercentOffSpecialArgsValidator,
            CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _createBuyNForXAmountSpecialArgsValidator = createBuyNForXAmountSpecialArgsValidator;
            _createBuyNGetMAtXPercentOffSpecialArgsValidator = createBuyNGetMAtXPercentOffSpecialArgsValidator;
            _createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator = createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator;
        }

        public ProductDto CreateBuyNForXAmountSpecial(CreateBuyNForXAmountSpecialArgs args)
        {
            _createBuyNForXAmountSpecialArgsValidator.ValidateAndThrow<CreateBuyNForXAmountSpecialArgs>(args);

            Func<Special> createSpecial = () => new BuyNForXAmountSpecial(args.StartTime.Value, args.EndTime.Value, args.DiscountedItems.Value, args.GroupSalePrice.Value, args.Limit);
            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNForXAmountSpecialDto>(special);
            return CreateSpecial(args, createSpecial, mapToSpecialDto);
        }

        public ProductDto CreateBuyNGetMAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            _createBuyNGetMAtXPercentOffSpecialArgsValidator.ValidateAndThrow<CreateBuyNGetMAtXPercentOffSpecialArgs>(args);

            Func<Special> createSpecial = () => new BuyNGetMAtXPercentOffSpecial(args.StartTime.Value, args.EndTime.Value, args.PreDiscountItems.Value, args.DiscountedItems.Value, args.PercentageOff.Value, args.Limit);
            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(special);
            return CreateSpecial(args, createSpecial, mapToSpecialDto);
        }

        public ProductDto CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            _createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator.ValidateAndThrow<CreateBuyNGetMAtXPercentOffSpecialArgs>(args);
            
            Func<Special> createSpecial = () => new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(args.StartTime.Value, args.EndTime.Value, args.PreDiscountItems.Value, args.DiscountedItems.Value, args.PercentageOff.Value, args.Limit);
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
