namespace PointOfSale.Services
{
    public class ScanItemArgs : IOrderIdArgs, IProductNameArgs
    {
        public long? OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
