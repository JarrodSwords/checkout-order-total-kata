using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialService : SpecialService
    {
        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialService(
            CreateSpecialArgs args,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper
        ) : base(
            args,
            dateTimeProvider,
            mapper
        ) { }

        public override Special Create()
        {
            var args = (ICreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgs) _args;
            return new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(
                _args.DiscountedItems.Value,
                _args.EndTime.Value,
                _args.PercentageOff.Value,
                _args.PreDiscountItems.Value,
                _args.StartTime.Value,
                _args.Limit
            );
        }

        public override ISpecialDto ToDto(Special special) =>
            _mapper.Map<BuyNGetMOfEqualOrLesserValueAtXPercentOffDto>((BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial) special);
    }
}
