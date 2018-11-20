namespace PointOfSale.Services
{
    public interface IUpsertEachesProductArgs : IProductArgs
    {
        decimal? RetailPrice { get; set; }
    }
}
