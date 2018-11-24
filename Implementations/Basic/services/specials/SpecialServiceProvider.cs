using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class SpecialServiceProvider : ISpecialServiceProvider
    {
        private IDictionary<string, Func<CreateSpecialArgs, SpecialService>> _factories;

        public IEnumerable<string> SpecialTypes =>
            _factories.Keys;

        public SpecialServiceProvider(IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _factories = new Dictionary<string, Func<CreateSpecialArgs, SpecialService>>
            {
                { "BuyNForXAmount", args => new BuyNAtXAmountSpecialService(args, dateTimeProvider, mapper) },
                { "BuyNGetMAtXPercentOff", args => new BuyNGetMAtXPercentOffSpecialService(args, dateTimeProvider, mapper) },
                { "BuyNGetMOfEqualOrLesserValueAtXPercentOff", args => new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialService(args, dateTimeProvider, mapper) }
            };
        }

        public SpecialService GetService(CreateSpecialArgs args) =>
            _factories[args.SpecialType](args);
    }
}
