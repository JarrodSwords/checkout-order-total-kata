namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedScannedItem : ScannedItem
    {
        private decimal _weight;

        public decimal Weight { get { return _weight; } }

        public WeightedScannedItem(Product product, decimal weight) : base(product)
        {
            _weight = weight;
        }
    }
}