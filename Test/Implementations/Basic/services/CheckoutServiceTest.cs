using System;
using FluentAssertions;
using FluentValidation;
using PointOfSale.Services;
using PointOfSale.Test.Services;
using Xunit;

namespace PointOfSale.Test.Implementations.Basic
{
    public class CheckoutServiceTest : ICheckoutServiceTest
    {
        public CheckoutServiceTest()
        {
            _orderRepository = DependencyProvider.OrderRepository();
            _checkoutService = DependencyProvider.CheckoutService(_orderRepository);
        }

        [Theory]
        [InlineData(null, "*Scanned item id is required*")]
        [InlineData(0, "*Scanned item id \"0\" does not exist*")]
        public void RemoveScannedItem_WithInvalidProductName_ThrowsArgumentException(int? itemId, string message)
        {
            Action scanItem = () => _checkoutService.RemoveScannedItem(new RemoveScannedItemArgs(1, itemId));

            scanItem.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Product Name' should not be empty*")]
        [InlineData("", "*'Product Name' should not be empty*")]
        [InlineData(" ", "*'Product Name' should not be empty*")]
        [InlineData("milk", "*'Product Name' \"milk\" does not exist*")]
        [InlineData("lean ground beef", "*Product Name must correspond to an eaches product*")]
        public void ScanItem_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action scanItem = () => _checkoutService.ScanItem(
                new ScanItemArgs { OrderId = 1, ProductName = productName }
            );

            scanItem.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Product Name' should not be empty*")]
        [InlineData("", "*'Product Name' should not be empty*")]
        [InlineData(" ", "*'Product Name' should not be empty*")]
        [InlineData("milk", "*'Product Name' \"milk\" does not exist*")]
        [InlineData("can of soup", "*Product Name must correspond to a mass product*")]
        public void ScanWeightedItem_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action scanItem = () => _checkoutService.ScanWeightedItem(
                new ScanWeightedItemArgs { OrderId = 1, ProductName = productName, MassAmount = 1m }
            );

            scanItem.Should().Throw<ValidationException>().WithMessage(message);
        }
    }
}
