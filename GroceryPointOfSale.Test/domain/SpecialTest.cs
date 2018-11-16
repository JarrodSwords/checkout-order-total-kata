using System;
using System.Collections.Generic;
using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.Test
{
    public abstract class SpecialTest
    {
        protected DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected IEnumerable<LineItem> _lineItems;

        protected virtual IEnumerable<ScannedItem> CreateScannedItems(Product product, int count)
        {
            for (var i = 0; i < count; i++)
                yield return new ScannedItem(product) { Id = i + 1 };
        }

        protected void CreateLineItems(Product product, Special special, int scannedItemCount)
        {
            var scannedItems = CreateScannedItems(product, scannedItemCount);
            var productSpecial = new ProductSpecial(product, special);

            _lineItems = productSpecial.CreateLineItems(scannedItems);
        }
    }
}
