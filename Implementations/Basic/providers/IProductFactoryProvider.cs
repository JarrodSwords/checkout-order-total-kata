using System.Collections.Generic;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public interface IProductFactoryProvider
    {
        ProductFactory GetFactory(IUpsertProductArgs args);
        IEnumerable<string> GetSellByTypes();
    }
}
