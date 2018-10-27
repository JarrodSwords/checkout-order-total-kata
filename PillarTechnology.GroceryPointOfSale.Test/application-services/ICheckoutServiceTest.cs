using System;
using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class ICheckoutServiceTest
    {
        protected IOrderRepository _orderRepository;
        protected ICheckoutService _checkoutService;

        [Theory]
        [InlineData(1, "can of soup")]
        public void ScanItem_AddsItemToOrder(long orderId, string productName)
        {
            var scannedItem = _checkoutService.Scan(orderId, productName);

            var order = _orderRepository.FindOrder(orderId);
            order.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "lean ground beef", 1)]
        public void ScanWeightedItem_AddsWeightedItemToOrder(long orderId, string productName, decimal weight)
        {
            var scannedItem = _checkoutService.Scan(orderId, productName, weight);

            var order = _orderRepository.FindOrder(orderId);
            order.ScannedItems.Should().Contain(scannedItem);
        }
    }
}