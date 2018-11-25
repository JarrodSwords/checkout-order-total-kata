using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class BuyNAtXAmountSpecialService : SpecialService
    {
        public BuyNAtXAmountSpecialService(
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
            var args = (ICreateBuyNAtXAmountSpecialArgs) _args;
            return new BuyNForXAmountSpecial(
                _args.DiscountedItems.Value,
                _args.EndTime.Value,
                _args.GroupSalePrice.Value,
                _args.StartTime.Value,
                _args.Limit
            );
        }

        public override ISpecialDto ToDto(Special special) =>
            _mapper.Map<BuyNForXAmountSpecialDto>((BuyNForXAmountSpecial) special);
    }
}
