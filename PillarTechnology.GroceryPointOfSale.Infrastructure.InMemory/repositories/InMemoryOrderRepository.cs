using System;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private ICollection<Order> _orders = new List<Order>();
        private long GetNextId()
        {
            return _orders.Count == 0 ? 1 : _orders.Select(x => x.Id).Max() + 1;
        }

        public Order CreateOrder(Order order)
        {
            order.Id = GetNextId();
            _orders.Add(order);
            return FindOrder(order.Id);
        }

        public Order FindOrder(long orderId)
        {
            return _orders.First(x => x.Id == orderId);
        }

        public Order UpdateOrder(Order order)
        {
            return FindOrder(order.Id);
        }
    }
}