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

    public class UpsertProductArgs : IUpsertEachesProductArgs, IUpsertMassProductArgs
    {
        public double? MassAmount { get; set; }
        public string MassUnit { get; set; }
        public string Name { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? RetailPricePerUnit { get; set; }
        public string SellByType { get; set; }

        public UpsertProductArgs() { }

        /// <summary>
        /// Create a new IUpsertEachesProductArgs
        /// </summary>
        /// <param name="name"></param>
        /// <param name="retailPrice"></param>
        /// <param name="sellByType"></param>
        public UpsertProductArgs(string name, decimal? retailPrice, string sellByType)
        {
            Name = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
        }

        /// <summary>
        /// Create a new IUpsertMassProductArgs
        /// </summary>
        /// <param name="massAmount"></param>
        /// <param name="massUnit"></param>
        /// <param name="name"></param>
        /// <param name="retailPricePerUnit"></param>
        /// <param name="sellByType"></param>
        public UpsertProductArgs(double? massAmount, string massUnit, string name, decimal? retailPricePerUnit, string sellByType)
        {
            MassAmount = massAmount;
            MassUnit = massUnit;
            Name = name;
            RetailPricePerUnit = retailPricePerUnit;
            SellByType = sellByType;
        }
    }
}
