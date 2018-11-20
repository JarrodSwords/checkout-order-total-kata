namespace PointOfSale.Services
{
    public interface IUpsertEachesProductArgs : IProductArgs
    {
        decimal? RetailPrice { get; set; }
    }

    public interface IUpsertMassProductArgs : IProductArgs
    {
        double? MassAmount { get; set; }
        string MassUnit { get; set; }
        decimal? RetailPricePerUnit { get; set; }
    }
}
