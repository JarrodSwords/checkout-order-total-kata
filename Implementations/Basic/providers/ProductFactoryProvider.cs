using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public class ProductFactoryProvider : IProductFactoryProvider
    {
        private IDictionary<string, Func<IProductArgs, ProductFactory>> _factories;

        public ProductFactoryProvider(IMapper mapper)
        {
            _factories = new Dictionary<string, Func<IProductArgs, ProductFactory>>
            { { "eaches", args => new EachesProductFactory(args, mapper) },
                { "mass", args => new MassProductFactory(args, mapper) }
            };
        }

        public ProductFactory GetFactory(IProductArgs args) =>
            _factories[args.SellByType](args);

        public IEnumerable<string> GetSellByTypes() =>
            _factories.Keys;
    }
}
