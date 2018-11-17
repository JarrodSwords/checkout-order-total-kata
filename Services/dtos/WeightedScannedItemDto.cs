namespace PointOfSale.Services
{
    public class WeightedScannedItemDto : ScannedItemDto
    {
        public decimal Weight { get; set; }
    }
}