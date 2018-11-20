namespace PointOfSale.Services
{
    public interface IUpsertEachesProductArgs : IProductNameArgs, ISellByTypeArgs
    {
        decimal? RetailPrice { get; set; }
    }
}
