namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class CreateBuyNForXAmountSpecialArgs : CreateSpecialArgs
    {
        public int? DiscountedItems { get; set; }
        public decimal? GroupSalePrice { get; set; }
    }
}
