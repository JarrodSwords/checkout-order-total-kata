using System;
using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSale.Test.Domain
{
    public abstract class SpecialTest
    {
        protected DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected IEnumerable<LineItem> _lineItems;

        protected virtual IEnumerable<ScannedItem> CreateScannedItems(Product product, int count)
        {
            for (var i = 0; i < count; i++)
                yield return new EachesScannedItem((EachesProduct) product) { Id = i + 1 };
        }

        protected void CreateLineItems(Product product, int scannedItemCount)
        {
            var scannedItems = CreateScannedItems(product, scannedItemCount);
            _lineItems = product.Special.CreateLineItems(scannedItems);
        }
    }
}
