﻿using System;
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

        public IScannable Scan(long orderId, string productName)
        {
            var order = _orderRepository.FindOrder(orderId);
            var product = _productRepository.FindProduct(productName);
            
            var item = new Item(product);
            order.AddScannable(item);
            _orderRepository.UpdateOrder(order);

            return item;
        }

        public IScannable Scan(long orderId, string productName, decimal weight)
        {
            var order = _orderRepository.FindOrder(orderId);
            var product = _productRepository.FindProduct(productName);
            
            var item = new WeightedItem(product, weight);
            order.AddScannable(item);
            _orderRepository.UpdateOrder(order);

            return item;
        }
    }
}