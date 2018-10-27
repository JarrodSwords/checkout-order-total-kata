using System;
using FluentAssertions;
using Moq;
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
        public void CreateOrder_AddsOrderToStorage()
        {
            var storedOrder = _orderRepository.CreateOrder(_order);

            storedOrder.Should().NotBeNull();
            storedOrder.Id.Should().BePositive();
        }

        [Fact]
        public void UpdateOrder_UpdatesNonIdentityOrderFieldsInStorage()
        {
            var order = _orderRepository.CreateOrder(_order);
            var dummyScannable = new Mock<IScannable>().Object;
            order.AddScannable(dummyScannable);

            var storedOrder = _orderRepository.UpdateOrder(order);

            storedOrder.ScannedItems.Should().Equal(order.ScannedItems);
        }
    }
}