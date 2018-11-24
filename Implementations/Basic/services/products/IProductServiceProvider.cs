using System.Collections.Generic;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public interface IProductServiceProvider
    {
        IEnumerable<string> SellByTypes { get; }

        ProductHelperService GetService(UpsertProductArgs args);
    }
}
