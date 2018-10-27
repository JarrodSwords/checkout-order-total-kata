using System;
using System.Linq;
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
        [InlineData(2, 1)]
        public void RemoveItem_RemovesItemFromOrder(long orderId, int itemId)
        {
            var order = _orderRepository.FindOrder(orderId);
            var scannableToRemove = order.ScannedItems.Single(x => x.Id == itemId);

            var removedScannable = _checkoutService.RemoveScannedItem(orderId, itemId);

            removedScannable.Should().Be(scannableToRemove);
            var storedOrder = _orderRepository.FindOrder(orderId);
            storedOrder.ScannedItems.Should().NotContain(scannableToRemove);
        }

        [Theory]
        [InlineData(1, "can of soup")]
        public void ScanItem_AddsItemToOrder(long orderId, string productName)
        {
            var scannedItem = _checkoutService.Scan(orderId, productName);

            var storedOrder = _orderRepository.FindOrder(orderId);
            storedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "lean ground beef")]
        public void ScanItem_ForWeightedItem_ThrowsArgumentException(long orderId, string productName)
        {
            Action scanInvalidItem = () => _checkoutService.Scan(orderId, productName);

            scanInvalidItem.Should().Throw<ArgumentException>()
                .WithMessage("Cannot add an item sold by weight without a weight");
        }

        [Theory]
        [InlineData(1, "lean ground beef", 1)]
        public void ScanItemAndAWeight_AddsWeightedItemToOrder(long orderId, string productName, decimal weight)
        {
            var scannedItem = _checkoutService.Scan(orderId, productName, weight);

            scannedItem.Should().BeOfType(typeof(WeightedItem));
            var storedOrder = _orderRepository.FindOrder(orderId);
            storedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "can of soup", 1)]
        public void ScanItemAndAWeight_ForNonweightedItem_ThrowsArgumentException(long orderId, string productName, decimal weight)
        {
            Action scanInvalidItem = () => _checkoutService.Scan(orderId, productName, weight);

            scanInvalidItem.Should().Throw<ArgumentException>()
                .WithMessage("Cannot add an item sold by unit as an item sold by weight");
        }
    }
}