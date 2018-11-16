using System.Collections;
using System.Collections.Generic;
using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.Test
{
    public class OrderProvider : IEnumerable<object[]>
    {
        public static Order CreateOrderWithScannedItems()
        {
            var order = new Order { Id = 1 };

            foreach (var scannedItem in new ScannedItemProvider().ScannedItems)
                order.AddScannedItem(scannedItem);

            return order;
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 14.75m };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}