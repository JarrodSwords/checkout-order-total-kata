namespace PointOfSale.Services
{
    public interface IUpsertProductArgs
    {
        string Name { get; set; }
        string SellByType { get; set; }
    }

    public interface IUpsertEachesProductArgs : IUpsertProductArgs
    {
        decimal? RetailPrice { get; set; }
    }

    public interface IUpsertMassProductArgs : IUpsertProductArgs
    {
        double? MassAmount { get; set; }
        string MassUnit { get; set; }
        decimal? RetailPricePerUnit { get; set; }
    }
}
