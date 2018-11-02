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

        [Fact]
        public void RemoveScannedItem_ScannedItemIsRemovedFromPersistedOrder()
        {
            var orderId = 1;
            var order = _orderRepository.FindOrder(orderId);
            var itemId = 1;
            var scannedItemToRemove = order.ScannedItems.Single(x => x.Id == itemId);

            var removedScannedItem = _checkoutService.RemoveScannedItem(new RemoveScannedItemArgs(orderId, itemId));

            removedScannedItem.Should().Be(scannedItemToRemove);
            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().NotContain(scannedItemToRemove);
        }

        [Theory]
        [InlineData(null, "Scanned item id is required")]
        [InlineData(0, "Scanned item id \"0\" does not exist")]
        public void RemoveScannedItem_WithInvalidProductName_ThrowsArgumentException(int? itemId, string message)
        {
            Action scanItem = () => _checkoutService.RemoveScannedItem(new RemoveScannedItemArgs(1, itemId));

            scanItem.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Fact]
        public void ScanItem_ScannedItemIsAddedToPersistedOrder()
        {
            var orderId = 1;
            var scannedItem = _checkoutService.ScanItem(new ScanItemArgs(orderId, "can of soup"));

            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(null, "Product name is required")]
        [InlineData("", "Product name is required")]
        [InlineData(" ", "Product name is required")]
        [InlineData("milk", "Product name \"milk\" does not exist")]
        [InlineData("lean ground beef", "Product name \"lean ground beef\" cannot be sold by unit")]
        public void ScanItem_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action scanItem = () => _checkoutService.ScanItem(new ScanItemArgs(1, productName));

            scanItem.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Fact]
        public void ScanWeightedItem_WeightedItemIsAddedToPersistedOrder()
        {
            var orderId = 1;
            var scannedItem = _checkoutService.ScanWeightedItem(new ScanWeightedItemArgs(orderId, "lean ground beef", 1m));

            scannedItem.Should().BeOfType(typeof(WeightedScannedItem));
            var persistedOrder = _orderRepository.FindOrder(orderId);
            persistedOrder.ScannedItems.Should().Contain(scannedItem);
        }

        [Theory]
        [InlineData(null, "Product name is required")]
        [InlineData("", "Product name is required")]
        [InlineData(" ", "Product name is required")]
        [InlineData("milk", "Product name \"milk\" does not exist")]
        [InlineData("can of soup", "Product name \"can of soup\" cannot be sold by weight")]
        public void ScanWeightedItem_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action scanItem = () => _checkoutService.ScanWeightedItem(new ScanWeightedItemArgs(1, productName, 1m));

            scanItem.Should().Throw<ArgumentException>().WithMessage(message);
        }
    }
}