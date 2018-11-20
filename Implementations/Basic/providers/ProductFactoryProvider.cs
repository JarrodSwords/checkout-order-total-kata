using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public class ProductFactoryProvider : IProductFactoryProvider
    {
        private IDictionary<string, Func<IUpsertProductArgs, ProductFactory>> _factories;

        public ProductFactoryProvider(IMapper mapper)
        {
            _factories = new Dictionary<string, Func<IUpsertProductArgs, ProductFactory>>
            { { "eaches", args => new EachesProductFactory(args, mapper) },
                { "mass", args => new MassProductFactory(args, mapper) }
            };
        }

        public ProductFactory GetFactory(IUpsertProductArgs args) =>
            _factories[args.SellByType](args);

        public IEnumerable<string> GetSellByTypes() =>
            _factories.Keys;
    }
}
