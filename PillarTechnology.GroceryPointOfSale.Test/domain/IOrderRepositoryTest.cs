using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IOrderRepositoryTest
    {
        protected Order _order;
        protected IOrderRepository _orderRepository;

        public IOrderRepositoryTest()
        {
            _order = new Order();
        }

        [Fact]
        public void CreateOrder_CreatesPersistedOrder()
        {
            var order = _orderRepository.CreateOrder(_order);

            var persistedOrder = _orderRepository.FindOrder(order.Id);
            persistedOrder.Should().NotBeNull();
            persistedOrder.Id.Should().BePositive();
        }

        [Fact]
        public void UpdateOrder_UpdatesNonIdentityOrderFieldsInPersistedOrder()
        {
            var order = _orderRepository.CreateOrder(_order);
            var dummyScannedItem = new ScannedItemProvider().GetScannedItem();
            order.AddScannedItem(dummyScannedItem);

            order = _orderRepository.UpdateOrder(order);

            var persistedOrder = _orderRepository.FindOrder(order.Id);
            persistedOrder.ScannedItems.Should().Equal(order.ScannedItems);
        }
    }
}