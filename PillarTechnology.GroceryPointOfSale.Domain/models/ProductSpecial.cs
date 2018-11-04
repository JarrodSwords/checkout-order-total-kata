using System.Collections.Generic;
using System.Linq;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class ProductSpecial
    {
        public Product Product { get; }
        public Special Special { get; }

        public ProductSpecial(Product product, Special special)
        {
            Product = product;
            Special = special;
        }

        public IEnumerable<LineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            var validSpecials = scannedItems.Count() / Special.GetScannedItemsRequired();
            var description = Special.GetLineItemDescription(Product);
            var salePrice = Special.CalculateSalePrice(Product);

            for (var i = 0; i < validSpecials; i++)
                yield return new SpecialLineItem(description, salePrice);
        }
    }
}