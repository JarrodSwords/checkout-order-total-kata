using System;
using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IOrderRepositoryTest
    {
        protected IOrderRepository _orderRepository;

        [Fact]
        public void CreateOrder_AddsOrderToStorage()
        {
            var order = new Order();

            var storedOrder = _orderRepository.CreateOrder(order);

            storedOrder.Should().NotBeNull();
            storedOrder.Id.Should().BePositive();
        }
    }
}