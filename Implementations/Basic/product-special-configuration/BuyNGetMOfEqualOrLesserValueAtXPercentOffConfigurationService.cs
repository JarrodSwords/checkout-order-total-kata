using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService : ProductSpecialConfigurationService
    {
        private readonly BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory _factory;

        public BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService(
            IMapper mapper,
            IProductRepository productRepository,
            BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory factory,
            CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator validator
        ) : base(mapper, productRepository, validator)
        {
            _factory = factory;
        }

        public override ISpecialFactory GetConfiguredSpecialFactory(CreateSpecialArgs args)
        {
            return _factory.Configure(
                args.DiscountedItems.Value,
                args.EndTime.Value,
                args.PercentageOff.Value,
                args.PreDiscountItems.Value,
                args.StartTime.Value,
                args.Limit
            );
        }

        public override ISpecialDto CreateSpecialDto(Special special)
        {
            return _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(special);
        }
    }
}
