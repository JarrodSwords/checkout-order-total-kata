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
        protected ICheckoutService _checkoutService;
        protected IOrderRepository _orderRepository;
        protected IProductRepository _productRepository;

        [Fact]
        public void ScanPurchasable_AddsPurchasableToOrder()
        {
            var product = new Product { Name = "can of soup" };
            _productRepository.CreateProduct(product);
            _orderRepository.CreateOrder(new Order());
            
            var order = _checkoutService.Scan(1, product.Name);

            order.ScannedItems.Should().Contain(x => x.Product == product);
        }
    }
}