namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class ScannedWeightedItem : ScannedItem
    {
        private decimal _weight;

        public decimal Weight { get { return _weight; } }

        public ScannedWeightedItem(Product product, decimal weight) : base(product)
        {
            _weight = weight;
        }
    }
}