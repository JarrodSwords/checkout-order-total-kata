using System;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class SellByWeightScanInputValidator : IScanInputValidator
    {
        private readonly Product _product;

        public SellByWeightScanInputValidator(Product product, decimal weight)
        {
            _product = product;
        }

        public void Validate()
        {
            if (_product.SellByType == SellByType.Unit)
                throw new ArgumentException("Cannot add an item sold by unit as an item sold by weight");
        }
    }
}