namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedItem : Item
    {
        private decimal _weight;

        public decimal Weight { get { return _weight; } }

        public WeightedItem(Product product, decimal weight) : base(product)
        {
            _weight = weight;
        }
    }
}