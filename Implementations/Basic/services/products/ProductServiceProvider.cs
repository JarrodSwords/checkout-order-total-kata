using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductServiceProvider : IProductServiceProvider
    {
        private IDictionary<string, Func<UpsertProductArgs, ProductHelperService>> _factories;

        public IEnumerable<string> SellByTypes =>
            _factories.Keys;

        public ProductServiceProvider(IMapper mapper)
        {
            _factories = new Dictionary<string, Func<UpsertProductArgs, ProductHelperService>>
            { 
                { "eaches", args => new EachesProductService(args, mapper) },
                { "mass", args => new MassProductService(args, mapper) }
            };
        }

        public ProductHelperService GetService(UpsertProductArgs args) =>
            _factories[args.SellByType](args);
    }
}
