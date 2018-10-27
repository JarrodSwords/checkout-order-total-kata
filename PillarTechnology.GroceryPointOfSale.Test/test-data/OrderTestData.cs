using System.Collections;
using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderTestData : IEnumerable<Order>
    {
        public static Order CreateOrderWithScannedItems()
        {
            var scannableTestData = new ScannedItemTestData().GetEnumerator();
            var order = new Order();

            while (scannableTestData.MoveNext())
                order.AddScannable(scannableTestData.Current);

            return order;
        }

        public IEnumerator<Order> GetEnumerator()
        {
            yield return new Order();
            yield return CreateOrderWithScannedItems();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}