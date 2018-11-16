using System.Collections.Generic;
using System.Linq;

namespace GroceryPointOfSale.Domain
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
            if (!GetIsBestDiscount())
                yield break;

            var validSpecials = scannedItems.Count() / Special.ScannedItemsRequired;

            for (var i = 0; i < validSpecials; i++)
            {
                if (Special.Limit != null && Special.Limit < (i + 1) * Special.ScannedItemsRequired)
                    yield break;

                yield return Special.CreateLineItem(Product, scannedItems, i);
            }
        }
        
        public bool GetIsBestDiscount()
        {
            var specialDiscount = Special.CalculateTotalDiscount(Product);

            if (specialDiscount > 0)
                return false;

            if (!Product.HasActiveMarkdown)
                return true;

            var markdownDiscount = Special.ScannedItemsRequired * -Product.Markdown.AmountOffRetail;

            return specialDiscount < markdownDiscount;
        }
    }
}
