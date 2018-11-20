namespace PointOfSale.Services
{
    public interface IUpsertMassProductArgs : IProductNameArgs, ISellByTypeArgs
    {
        double? MassAmount { get; set; }
        string MassUnit { get; set; }
        decimal? RetailPricePerUnit { get; set; }
    }
}
