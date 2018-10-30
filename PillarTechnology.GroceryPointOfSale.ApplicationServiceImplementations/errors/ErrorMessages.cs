using System.Collections.Generic;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public static class ErrorMessages
    {
        private static IDictionary<Error, string> _errorMessages = new Dictionary<Error, string>
        {
            {Error.CannotCreateExistingProduct, "Product \"{PropertyValue}\" already exists"},
            {Error.ProductNameRequired, "Product name is required"},
            {Error.ProductRetailPriceCannotBeNegative, "Product retail price cannot be negative"},
            {Error.ProductRetailPriceRequired, "Product retail price is required"}
        };

        public static string GetMessage(Error error) => _errorMessages[error];
    }
}