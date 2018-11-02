using System;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CheckoutService : ICheckoutService
    {
        #region Dependencies

        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private RemoveScannedItemArgsValidator _removeScannedItemArgsValidator;
        private ScanItemArgsValidator _scanItemArgsValidator;
        private ScanWeightedItemArgsValidator _scanWeightedItemArgsValidator;

        public CheckoutService(IOrderRepository orderRepository, IProductRepository productRepository, RemoveScannedItemArgsValidator removeScannedItemArgsValidator, ScanItemArgsValidator scanItemArgsValidator, ScanWeightedItemArgsValidator scanWeightedItemArgsValidator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _removeScannedItemArgsValidator = removeScannedItemArgsValidator;
            _scanItemArgsValidator = scanItemArgsValidator;
            _scanWeightedItemArgsValidator = scanWeightedItemArgsValidator;
        }

        #endregion Dependencies

        public ScannedItem RemoveScannedItem(RemoveScannedItemArgs args)
        {
            _removeScannedItemArgsValidator.ValidateAndThrow<RemoveScannedItemArgs>(args);

            var order = _orderRepository.FindOrder(args.OrderId);

            var removedItem = order.RemoveScannedItem(args.ItemId);
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

            return ScanItem(args.OrderId, args.ProductName, product => new WeightedScannedItem(product, args.Weight));
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