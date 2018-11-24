namespace PointOfSale.Services
{
    public class UpsertProductArgs : IUpsertEachesProductArgs, IUpsertMassProductArgs
    {
        public double? MassAmount { get; set; }
        public string MassUnit { get; set; }
        public string ProductName { get; set; }
        public decimal? RetailPrice { get; set; }
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
            ProductName = name;
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
        public UpsertProductArgs(double? massAmount, string massUnit, string name, decimal? retailPrice, string sellByType)
        {
            MassAmount = massAmount;
            MassUnit = massUnit;
            ProductName = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
        }
    }
}
