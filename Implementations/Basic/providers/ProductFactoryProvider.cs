using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public class ProductFactoryProvider : IProductFactoryProvider
    {
        private IDictionary<string, Func<IProductNameArgs, ProductFactory>> _factories;

        public IEnumerable<string> SellByTypes =>
            _factories.Keys;

        public ProductFactoryProvider(IMapper mapper)
        {
            _factories = new Dictionary<string, Func<IProductNameArgs, ProductFactory>>
            { { "eaches", args => new EachesProductFactory(args, mapper) },
                { "mass", args => new MassProductFactory(args, mapper) }
            };
        }

        public ProductFactory GetFactory(UpsertProductArgs args) =>
            _factories[args.SellByType](args);
    }
}
