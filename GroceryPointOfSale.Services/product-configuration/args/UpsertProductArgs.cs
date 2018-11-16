namespace GroceryPointOfSale.ApplicationServices
{
    public class UpsertProductArgs
    {
        public string Name { get; set; }
        public decimal? RetailPrice { get; set; }
        public string SellByType { get; set; }

        public UpsertProductArgs()
        {
        }

        public UpsertProductArgs(string name, decimal? retailPrice, string sellByType)
        {
            Name = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
        }
    }
}