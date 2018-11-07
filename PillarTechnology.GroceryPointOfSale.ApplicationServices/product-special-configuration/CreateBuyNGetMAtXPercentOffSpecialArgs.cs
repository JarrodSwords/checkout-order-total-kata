namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class CreateBuyNGetMAtXPercentOffSpecialArgs : CreateSpecialArgs
    {
        public int? DiscountedItems { get; set; }
        public decimal? PercentageOff { get; set; }
        public int? PreDiscountItems { get; set; }
    }
}
