using System;
using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class ProductSpecialConfigurationService : IProductSpecialConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly BuyNForXAmountSpecial.Factory _buyNForXAmountSpecialFactory;
        private readonly BuyNGetMAtXPercentOffSpecial.Factory _buyNGetMAtXPercentOffSpecialFactory;
        private readonly BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory _buyNGetMOfEqualOrLesserValueAtXPercentOffSpecialFactory;
        private readonly IProductRepository _productRepository;
        private readonly CreateBuyNForXAmountSpecialArgsValidator _createBuyNForXAmountSpecialArgsValidator;
        private readonly CreateBuyNGetMAtXPercentOffSpecialArgsValidator _createBuyNGetMAtXPercentOffSpecialArgsValidator;
        private readonly CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator _createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator;

        public ProductSpecialConfigurationService(
            IMapper mapper,
            BuyNForXAmountSpecial.Factory buyNForXAmountSpecialFactory,
            BuyNGetMAtXPercentOffSpecial.Factory buyNGetMAtXPercentOffSpecialFactory,
            BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory buyNGetMOfEqualOrLesserValueAtXPercentOffSpecialFactory,
            IProductRepository productRepository,
            CreateBuyNForXAmountSpecialArgsValidator createBuyNForXAmountSpecialArgsValidator,
            CreateBuyNGetMAtXPercentOffSpecialArgsValidator createBuyNGetMAtXPercentOffSpecialArgsValidator,
            CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator)
        {
            _mapper = mapper;
            _buyNForXAmountSpecialFactory = buyNForXAmountSpecialFactory;
            _buyNGetMAtXPercentOffSpecialFactory = buyNGetMAtXPercentOffSpecialFactory;
            _buyNGetMOfEqualOrLesserValueAtXPercentOffSpecialFactory = buyNGetMOfEqualOrLesserValueAtXPercentOffSpecialFactory;
            _productRepository = productRepository;
            _createBuyNForXAmountSpecialArgsValidator = createBuyNForXAmountSpecialArgsValidator;
            _createBuyNGetMAtXPercentOffSpecialArgsValidator = createBuyNGetMAtXPercentOffSpecialArgsValidator;
            _createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator = createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator;
        }

        public ProductDto CreateBuyNForXAmountSpecial(CreateBuyNForXAmountSpecialArgs args)
        {
            _createBuyNForXAmountSpecialArgsValidator.ValidateAndThrow<CreateBuyNForXAmountSpecialArgs>(args);

            _buyNForXAmountSpecialFactory.Configure(
                args.DiscountedItems.Value,
                args.EndTime.Value,
                args.GroupSalePrice.Value,
                args.StartTime.Value,
                args.Limit
            );

            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNForXAmountSpecialDto>(special);
            return CreateSpecial(args, _buyNForXAmountSpecialFactory, mapToSpecialDto);
        }

        public ProductDto CreateBuyNGetMAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            _createBuyNGetMAtXPercentOffSpecialArgsValidator.ValidateAndThrow<CreateBuyNGetMAtXPercentOffSpecialArgs>(args);

            _buyNGetMAtXPercentOffSpecialFactory.Configure(
                args.DiscountedItems.Value,
                args.EndTime.Value,
                args.PercentageOff.Value,
                args.PreDiscountItems.Value,
                args.StartTime.Value,
                args.Limit
            );

            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(special);
            return CreateSpecial(args, _buyNGetMAtXPercentOffSpecialFactory, mapToSpecialDto);
        }

        public ProductDto CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            _createBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator.ValidateAndThrow<CreateBuyNGetMAtXPercentOffSpecialArgs>(args);
            
            _buyNGetMOfEqualOrLesserValueAtXPercentOffSpecialFactory.Configure(
                args.DiscountedItems.Value,
                args.EndTime.Value,
                args.PercentageOff.Value,
                args.PreDiscountItems.Value,
                args.StartTime.Value,
                args.Limit
            );

            Func<Special, ISpecialDto> mapToSpecialDto = special => _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(special);
            return CreateSpecial(args, _buyNGetMOfEqualOrLesserValueAtXPercentOffSpecialFactory, mapToSpecialDto);
        }

        private ProductDto CreateSpecial(CreateSpecialArgs args, ISpecialFactory specialFactory, Func<Special, ISpecialDto> mapToSpecialDto)
        {
            var product = _productRepository.FindProduct(args.ProductName);

            product.Special = specialFactory.CreateSpecial();
            var persistedProduct = _productRepository.UpdateProduct(product);

            var productDto = _mapper.Map<ProductDto>(persistedProduct);
            productDto.Special = mapToSpecialDto(product.Special);
            return productDto;
        }
    }
}
