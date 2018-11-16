using System;
using System.Linq;
using FluentValidation;

namespace PointOfSale.ApplicationServiceImplementations
{
    public static class IValidatorExtensions
    {
        public static void ValidateAndThrow<T>(this IValidator validator, T args)
        {
            var validationResult = validator.Validate(args);

            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.ToString());
        }
    }
}