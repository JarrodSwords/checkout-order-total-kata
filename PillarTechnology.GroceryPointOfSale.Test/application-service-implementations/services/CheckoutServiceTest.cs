using System;
using AutoMapper;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class CheckoutServiceTest : ICheckoutServiceTest
    {
        public CheckoutServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            _orderRepository = new InMemoryOrderRepositoryFactory().CreateSeededRepository();
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var removeScannedItemArgsValidator = new RemoveScannedItemArgsValidator(_orderRepository);
            var scanItemArgsValidator = new ScanItemArgsValidator(productRepository);
            var scanWeightedItemArgsValidator = new ScanWeightedItemArgsValidator(productRepository);

            _checkoutService = new CheckoutService(mapper, _orderRepository, productRepository, removeScannedItemArgsValidator, scanItemArgsValidator, scanWeightedItemArgsValidator);
        }

        [Theory]
        [InlineData(null, "Scanned item id is required")]
        [InlineData(0, "Scanned item id \"0\" does not exist")]
        public void RemoveScannedItem_WithInvalidProductName_ThrowsArgumentException(int? itemId, string message)
        {
            Action scanItem = () => _checkoutService.RemoveScannedItem(new RemoveScannedItemArgs(1, itemId));

            scanItem.Should().Throw<ArgumentException>().WithMessage(message);
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