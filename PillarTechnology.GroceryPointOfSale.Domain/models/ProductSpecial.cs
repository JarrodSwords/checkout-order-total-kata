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
            if (!Special.GetIsBestDiscount(Product))
                yield break;

            var itemsRequired = Special.GetScannedItemsRequired();
            var validSpecials = scannedItems.Count() / itemsRequired;

            for (var i = 0; i < validSpecials; i++)
            {
                if (Special.Limit != null && Special.Limit < (i + 1) * itemsRequired)
                    yield break;

                yield return Special.CreateLineItem(Product, scannedItems, i);
            }
        }
    }
}
