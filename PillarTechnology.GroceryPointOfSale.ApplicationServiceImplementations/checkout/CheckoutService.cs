using System;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CheckoutService : ICheckoutService
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ScanItemArgsValidator _scanItemArgsValidator;
        private ScanWeightedItemArgsValidator _scanWeightedItemArgsValidator;

        public CheckoutService(IOrderRepository orderRepository, IProductRepository productRepository, ScanItemArgsValidator scanItemArgsValidator, ScanWeightedItemArgsValidator scanWeightedItemArgsValidator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _scanItemArgsValidator = scanItemArgsValidator;
            _scanWeightedItemArgsValidator = scanWeightedItemArgsValidator;
        }

        public ScannedItem RemoveScannedItem(long orderId, int itemId)
        {
            var order = _orderRepository.FindOrder(orderId);

            var removedItem = order.RemoveScannedItem(itemId);
            _orderRepository.UpdateOrder(order);

            return removedItem;
        }

        public ScannedItem ScanItem(ScanItemArgs args)
        {
            _scanItemArgsValidator.ValidateAndThrow<ScanItemArgs>(args);

            return ScanItem(args.OrderId, args.ProductName, product => new ScannedItem(product));
        }

        public ScannedItem ScanWeightedItem(ScanWeightedItemArgs args)
        {
            _scanWeightedItemArgsValidator.ValidateAndThrow<ScanWeightedItemArgs>(args);

            return ScanItem(args.OrderId, args.ProductName, product => new ScannedWeightedItem(product, args.Weight));
        }

        private ScannedItem ScanItem(long orderId, string productName, Func<Product, ScannedItem> createScannedItem)
        {
            var order = _orderRepository.FindOrder(orderId);
            var product = _productRepository.FindProduct(productName);
            var scannedItem = createScannedItem(product);

            order.AddScannedItem(scannedItem);
            _orderRepository.UpdateOrder(order);

            return scannedItem;
        }
    }
}