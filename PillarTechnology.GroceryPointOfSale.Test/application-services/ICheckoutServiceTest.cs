using System;
using System.Linq;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class ICheckoutServiceTest
    {
        protected ICheckoutService _checkoutService;
        protected IOrderRepository _orderRepository;

        [Theory]
        [InlineData(1, 1)]
        public void RemoveScannedItem_ScannedItemIsRemovedFromPersistedOrder(long orderId, int itemId)
        {
            var order = _orderRepository.FindOrder(orderId);
            var scannableToRemove = order.ScannedItems.Single(x => x.Id == itemId);

            var removedScannable = _checkoutService.RemoveScannedItem(orderId, itemId);

            removedScannable.Should().Be(scannableToRemove);
            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().NotContain(scannableToRemove);
        }

        [Theory]
        [InlineData(1, "can of soup")]
        public void ScanItem_ScannedItemIsAddedToPersistedOrder(long orderId, string productName)
        {
            var scannedItem = _checkoutService.ScanItem(orderId, productName);

            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "lean ground beef")]
        public void ScanItem_ForWeightedItem_ThrowsArgumentException(long orderId, string productName)
        {
            Action scanInvalidItem = () => _checkoutService.ScanItem(orderId, productName);

            scanInvalidItem.Should().Throw<ArgumentException>()
                .WithMessage("Cannot add an item sold by weight without a weight");
        }

        [Theory]
        [InlineData(1, "lean ground beef", 1)]
        public void ScanItemAndWeight_WeightedItemIsAddedToPersistedOrder(long orderId, string productName, decimal weight)
        {
            var scannedItem = _checkoutService.ScanItem(orderId, productName, weight);

            scannedItem.Should().BeOfType(typeof(WeightedItem));
            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "can of soup", 1)]
        public void ScanItemAndWeight_ForNonweightedItem_ThrowsArgumentException(long orderId, string productName, decimal weight)
        {
            Action scanInvalidItem = () => _checkoutService.ScanItem(orderId, productName, weight);

            scanInvalidItem.Should().Throw<ArgumentException>()
                .WithMessage("Cannot add an item sold by unit as an item sold by weight");
        }
    }
}