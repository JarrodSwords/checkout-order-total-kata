﻿using System;
using AutoMapper;
using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CheckoutService : ICheckoutService
    {
        #region Dependencies

        private IMapper _mapper;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private RemoveScannedItemArgsValidator _removeScannedItemArgsValidator;
        private ScanItemArgsValidator _scanItemArgsValidator;
        private ScanWeightedItemArgsValidator _scanWeightedItemArgsValidator;

        public CheckoutService(
            IMapper mapper,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            RemoveScannedItemArgsValidator removeScannedItemArgsValidator,
            ScanItemArgsValidator scanItemArgsValidator,
            ScanWeightedItemArgsValidator scanWeightedItemArgsValidator
        )
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _removeScannedItemArgsValidator = removeScannedItemArgsValidator;
            _scanItemArgsValidator = scanItemArgsValidator;
            _scanWeightedItemArgsValidator = scanWeightedItemArgsValidator;
        }

        #endregion Dependencies

        public ScannedItemDto RemoveScannedItem(RemoveScannedItemArgs args)
        {
            _removeScannedItemArgsValidator.ValidateAndThrow(args);

            var order = _orderRepository.FindOrder(args.OrderId.Value);

            var removedItem = _mapper.Map<ScannedItemDto>(order.RemoveScannedItem(args.ScannedItemId.Value));
            _orderRepository.UpdateOrder(order);

            return removedItem;
        }

        public ScannedItemDto ScanItem(ScanItemArgs args)
        {
            _scanItemArgsValidator.ValidateAndThrow<ScanItemArgs>(args);

            return _mapper.Map<ScannedItemDto>(ScanItem(args.OrderId.Value, args.ProductName, product => new ScannedItem(product)));
        }

        public ScannedItemDto ScanWeightedItem(ScanWeightedItemArgs args)
        {
            _scanWeightedItemArgsValidator.ValidateAndThrow<ScanWeightedItemArgs>(args);

            return _mapper.Map<WeightedScannedItemDto>(ScanItem(args.OrderId.Value, args.ProductName, product => new WeightedScannedItem(product, args.MassAmount.Value)));
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
