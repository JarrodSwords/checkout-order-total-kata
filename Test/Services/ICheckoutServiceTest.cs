using System.Linq;
using FluentAssertions;
using PointOfSale.Domain;
using PointOfSale.Services;
using Xunit;

namespace PointOfSale.Test.Services
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

            var removedScannedItem = _checkoutService.RemoveScannedItem(
                new RemoveScannedItemArgs { OrderId = orderId, ScannedItemId = itemId }
            );

            var persistedScannedItems = _orderRepository.FindOrder(orderId).ScannedItems.Select(x => x.Id);
            persistedScannedItems.Should().NotContain(scannedItemToRemove.Id);
        }

        [Fact]
        public void ScanItem_ScannedItemIsAddedToPersistedOrder()
        {
            var orderId = 1;
            var scannedItem = _checkoutService.ScanItem(
                new ScanItemArgs { OrderId = orderId, ProductName = "can of soup" }
            );

            var persistedScannedItems = _orderRepository.FindOrder(orderId).ScannedItems.Select(x => x.Id);
            persistedScannedItems.Should().Contain(scannedItem.Id);
        }

        [Fact]
        public void ScanWeightedItem_WeightedItemIsAddedToPersistedOrder()
        {
            var orderId = 1;
            var scannedItem = _checkoutService.ScanWeightedItem(
                new ScanWeightedItemArgs { OrderId = orderId, ProductName = "lean ground beef", MassAmount = 1m }
            );

            var persistedScannedItems = _orderRepository.FindOrder(orderId).ScannedItems.Select(x => x.Id);
            persistedScannedItems.Should().Contain(scannedItem.Id);
        }
    }
}
