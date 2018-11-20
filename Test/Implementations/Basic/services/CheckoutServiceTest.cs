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
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        [InlineData("lean ground beef", "*Product name \"lean ground beef\" cannot be sold as eaches*")]
        public void ScanItem_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action scanItem = () => _checkoutService.ScanItem(new ScanItemArgs(1, productName));

            scanItem.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        [InlineData("can of soup", "*Product name \"can of soup\" cannot be sold by mass*")]
        public void ScanWeightedItem_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action scanItem = () => _checkoutService.ScanWeightedItem(new ScanWeightedItemArgs(1, productName, 1m));

            scanItem.Should().Throw<ValidationException>().WithMessage(message);
        }
    }
}
