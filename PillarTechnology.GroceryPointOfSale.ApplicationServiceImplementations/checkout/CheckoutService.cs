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

        public IScannable RemoveScannedItem(long orderId, int itemId)
        {
            var order = _orderRepository.FindOrder(orderId);

            var removedItem = order.RemoveScannedItem(itemId);
            _orderRepository.UpdateOrder(order);

            return removedItem;
        }

        public IScannable ScanItem(long orderId, string productName)
        {
            var product = _productRepository.FindProduct(productName);
            var validator = new SellByUnitScanInputValidator(product);
            var itemFactory = new ItemFactory(product);

            return ScanItem(orderId, product, validator, itemFactory);
        }

        public IScannable ScanItem(long orderId, string productName, decimal weight)
        {
            var product = _productRepository.FindProduct(productName);
            var validator = new SellByWeightScanInputValidator(product, weight);
            var weightedItemFactory = new WeightedItemFactory(product, weight);

            return ScanItem(orderId, product, validator, weightedItemFactory);
        }

        private IScannable ScanItem(long orderId, Product product, IScanInputValidator validator, ScannableFactory scannableFactory)
        {
            validator.Validate();

            var order = _orderRepository.FindOrder(orderId);
            var scannable = scannableFactory.CreateScannable();

            order.AddScannable(scannable);
            _orderRepository.UpdateOrder(order);

            return scannable;
        }
    }
}