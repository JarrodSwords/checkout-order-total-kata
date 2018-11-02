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
            var scannedItemToRemove = order.ScannedItems.Single(x => x.Id == itemId);

            var removedScannedItem = _checkoutService.RemoveScannedItem(orderId, itemId);

            removedScannedItem.Should().Be(scannedItemToRemove);
            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().NotContain(scannedItemToRemove);
        }

        [Theory]
        [InlineData(1, "can of soup")]
        public void ScanItem_ScannedItemIsAddedToPersistedOrder(long orderId, string productName)
        {
            var scannedItem = _checkoutService.ScanItem(new ScanItemArgs { OrderId = orderId, ProductName = productName });

            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "lean ground beef")]
        public void ScanItem_ForWeightedItem_ThrowsArgumentException(long orderId, string productName)
        {
            var args = new ScanItemArgs { OrderId = orderId, ProductName = productName };

            Action scanInvalidItem = () => _checkoutService.ScanItem(args);

            scanInvalidItem.Should().Throw<ArgumentException>()
                .WithMessage($"Product name \"{productName}\" cannot be sold by unit");
        }

        [Theory]
        [InlineData(1, "lean ground beef", 1)]
        public void ScanItemAndWeight_WeightedItemIsAddedToPersistedOrder(long orderId, string productName, decimal weight)
        {
            var args = new ScanWeightedItemArgs { OrderId = orderId, ProductName = productName, Weight = weight };

            var scannedItem = _checkoutService.ScanWeightedItem(args);

            scannedItem.Should().BeOfType(typeof(WeightedScannedItem));
            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(1, "can of soup", 1)]
        public void ScanItemAndWeight_ForEachesItem_ThrowsArgumentException(long orderId, string productName, decimal weight)
        {
            var args = new ScanWeightedItemArgs { OrderId = orderId, ProductName = productName, Weight = weight };

            Action scanInvalidItem = () => _checkoutService.ScanWeightedItem(args);

            scanInvalidItem.Should().Throw<ArgumentException>()
                .WithMessage($"Product name \"{productName}\" cannot be sold by weight");
        }
    }
}