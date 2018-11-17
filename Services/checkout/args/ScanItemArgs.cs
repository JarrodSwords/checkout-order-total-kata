namespace PointOfSale.Services
{
    public class ScanItemArgs
    {
        public long? OrderId { get; set; }
        public string ProductName { get; set; }

        public ScanItemArgs() { }

        public ScanItemArgs(long? orderId, string productName)
        {
            OrderId = orderId;
            ProductName = productName;
        }
    }
}