using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public abstract class SpecialService : IFactory<Special>
    {
        protected readonly CreateSpecialArgs _args;
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly IMapper _mapper;

        public SpecialService(
            CreateSpecialArgs args,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper
        )
        {
            _args = args;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }

        public abstract Special Create();
        public abstract ISpecialDto ToDto(Special special);
    }

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
            return new BuyNForXAmountSpecial.Factory(_dateTimeProvider)
                .Configure(
                    _args.DiscountedItems.Value,
                    _args.EndTime.Value,
                    _args.GroupSalePrice.Value,
                    _args.StartTime.Value,
                    _args.Limit
                )
                .CreateSpecial();
        }

        public override ISpecialDto ToDto(Special special) =>
            _mapper.Map<BuyNForXAmountSpecialDto>((BuyNForXAmountSpecial) special);
    }

    public class BuyNGetMAtXPercentOffSpecialService : SpecialService
    {
        public BuyNGetMAtXPercentOffSpecialService(
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
            var args = (ICreateBuyNGetMAtXPercentOffSpecialArgs) _args;
            return new BuyNGetMAtXPercentOffSpecial.Factory(_dateTimeProvider)
                .Configure(
                    _args.DiscountedItems.Value,
                    _args.EndTime.Value,
                    _args.PercentageOff.Value,
                    _args.PreDiscountItems.Value,
                    _args.StartTime.Value,
                    _args.Limit
                )
                .CreateSpecial();
        }

        public override ISpecialDto ToDto(Special special) =>
            _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>((BuyNGetMAtXPercentOffSpecial) special);
    }

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
            return new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory(_dateTimeProvider)
                .Configure(
                    _args.DiscountedItems.Value,
                    _args.EndTime.Value,
                    _args.PercentageOff.Value,
                    _args.PreDiscountItems.Value,
                    _args.StartTime.Value,
                    _args.Limit
                )
                .CreateSpecial();
        }

        public override ISpecialDto ToDto(Special special) =>
            _mapper.Map<BuyNGetMOfEqualOrLesserValueAtXPercentOffDto>((BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial) special);
    }

    public interface ISpecialServiceProvider
    {
        IEnumerable<string> SpecialTypes { get; }

        SpecialService GetService(CreateSpecialArgs args);
    }

    public class SpecialServiceProvider : ISpecialServiceProvider
    {
        private IDictionary<string, Func<CreateSpecialArgs, SpecialService>> _factories;

        public IEnumerable<string> SpecialTypes =>
            _factories.Keys;

        public SpecialServiceProvider(IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _factories = new Dictionary<string, Func<CreateSpecialArgs, SpecialService>>
            { { "BuyNForXAmount", args => new BuyNAtXAmountSpecialService(args, dateTimeProvider, mapper) },
                { "BuyNGetMAtXPercentOff", args => new BuyNGetMAtXPercentOffSpecialService(args, dateTimeProvider, mapper) },
                { "BuyNGetMOfEqualOrLesserValueAtXPercentOff", args => new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialService(args, dateTimeProvider, mapper) }
            };
        }

        public SpecialService GetService(CreateSpecialArgs args) =>
            _factories[args.SpecialType](args);
    }
}
