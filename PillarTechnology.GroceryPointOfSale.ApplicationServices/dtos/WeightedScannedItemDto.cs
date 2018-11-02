namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class WeightedScannedItemDto : ScannedItemDto
    {
        public decimal Weight { get; set; }
    }
}