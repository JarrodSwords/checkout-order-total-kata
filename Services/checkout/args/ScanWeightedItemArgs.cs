namespace PointOfSale.Services
{
    public class ScanWeightedItemArgs : IMassArgs, IOrderIdArgs, IProductNameArgs
    {
        public double? MassAmount { get; set; }
        public string MassUnit { get; set; }
        public long? OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
