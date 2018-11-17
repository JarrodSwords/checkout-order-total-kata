using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class BuyNForXAmountConfigurationService : ProductSpecialConfigurationService
    {
        private readonly BuyNForXAmountSpecial.Factory _factory;

        public BuyNForXAmountConfigurationService(
            IMapper mapper,
            IProductRepository productRepository,
            BuyNForXAmountSpecial.Factory factory,
            CreateBuyNForXAmountSpecialArgsValidator validator
        ) : base(mapper, productRepository, validator)
        {
            _factory = factory;
        }

        public override ISpecialFactory GetConfiguredSpecialFactory(CreateSpecialArgs args)
        {
            return _factory.Configure(
                args.DiscountedItems.Value,
                args.EndTime.Value,
                args.GroupSalePrice.Value,
                args.StartTime.Value,
                args.Limit
            );
        }

        public override ISpecialDto CreateSpecialDto(Special special)
        {
            return _mapper.Map<BuyNForXAmountSpecialDto>(special);
        }
    }
}
