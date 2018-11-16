using System.Collections.Generic;
using PointOfSale.ApplicationServices;
using PointOfSale.Domain;

namespace PointOfSale.ApplicationServiceImplementations
{
    public class ProductSpecialConfigurationServiceProvider
    {
        private readonly IDictionary<SpecialType, IProductSpecialConfigurationService> _productConfigurationServices =
            new Dictionary<SpecialType, IProductSpecialConfigurationService>();

        public ProductSpecialConfigurationServiceProvider(
            BuyNForXAmountConfigurationService buyNForXAmountConfigurationService,
            BuyNGetMAtXPercentOffConfigurationService buyNGetMAtXPercentOffConfigurationService,
            BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService
        )
        {
            _productConfigurationServices.Add(SpecialType.BuyNForXAmount, buyNForXAmountConfigurationService);
            _productConfigurationServices.Add(SpecialType.BuyNGetMAtXPercentOff, buyNGetMAtXPercentOffConfigurationService);
            _productConfigurationServices.Add(SpecialType.BuyNGetMOfEqualOrLesserValueAtXPercentOff, buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService);
        }

        public IProductSpecialConfigurationService GetConfigurationService(SpecialType specialType) => _productConfigurationServices[specialType];
    }
}