using System;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class SellByUnitScanInputValidator : IScanInputValidator
    {
        private readonly Product _product;

        public SellByUnitScanInputValidator(Product product)
        {
            _product = product;
        }

        public void Validate()
        {
            if (_product.SellByType == SellByType.Weight)
                throw new ArgumentException("Cannot add an item sold by weight without a weight");
        }
    }
}