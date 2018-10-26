using System;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CheckoutService : ICheckoutService
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;

        public CheckoutService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public Order Scan(long orderId, string productName)
        {
            var order = _orderRepository.FindOrder(orderId);
            var product = _productRepository.FindProduct(productName);
            var item = new Item(product);

            order.AddPurchasable(item);

            return _orderRepository.UpdateOrder(order);
        }
    }
}