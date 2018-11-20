using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public interface IProductFactoryProvider
    {
        ProductFactory GetFactory(IUpsertProductArgs args);
    }
}
