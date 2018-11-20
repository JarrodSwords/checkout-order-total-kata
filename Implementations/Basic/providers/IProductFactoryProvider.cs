using System.Collections.Generic;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public interface IProductFactoryProvider
    {
        IEnumerable<string> SellByTypes { get; }

        ProductFactory GetFactory(IProductArgs args);
    }
}
