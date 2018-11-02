using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderProvider : IEnumerable<object[]>
    {
        private readonly static ICollection<Order> _orders;

        static OrderProvider()
        {
            _orders = new List<Order>
            {
                CreateOrderWithScannedItems()
            };
        }

        public static ICollection<Order> Orders => _orders;

        public static Order CreateOrderWithScannedItems()
        {
            var order = new Order { Id = 1 };

            foreach (var scannedItem in new ScannedItemProvider().ScannedItems)
                order.AddScannedItem(scannedItem);

            return order;
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach(var order in OrderProvider.Orders)
                yield return new object[] { order.Id, 14.75m };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}