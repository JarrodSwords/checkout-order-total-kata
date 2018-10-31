using System.Collections;
using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderProvider : IEnumerable<object[]>
    {
        private static ICollection<Order> _orders = new List<Order>
        {
            CreateOrderWithScannedItems()
        };

        public static ICollection<Order> Orders => _orders;

        public static Order CreateOrderWithScannedItems()
        {
            var order = new Order { Id = 1 };

            foreach (var scannable in ScannedItemProvider.ScannedItems)
                order.AddScannable(scannable);

            return order;
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var order in OrderProvider.Orders)
                yield return new object[] { order };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}