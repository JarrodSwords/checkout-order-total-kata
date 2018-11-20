using System;
using System.Collections.Generic;
using AutoMapper;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public class ProductServiceProvider : IProductServiceProvider
    {
        private IDictionary<string, Func<UpsertProductArgs, ProductService>> _factories;

        public IEnumerable<string> SellByTypes =>
            _factories.Keys;

        public ProductServiceProvider(IMapper mapper)
        {
            _factories = new Dictionary<string, Func<UpsertProductArgs, ProductService>>
            { 
                { "eaches", args => new EachesProductService(args, mapper) },
                { "mass", args => new MassProductService(args, mapper) }
            };
        }

        public ProductService GetService(UpsertProductArgs args) =>
            _factories[args.SellByType](args);
    }
}
