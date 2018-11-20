using System.Collections.Generic;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public interface IProductServiceProvider
    {
        IEnumerable<string> SellByTypes { get; }

        ProductService GetService(UpsertProductArgs args);
    }
}
