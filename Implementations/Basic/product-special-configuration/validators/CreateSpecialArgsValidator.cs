using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateSpecialArgsValidator : AbstractValidator<CreateSpecialArgs>
    {
        public CreateSpecialArgsValidator(
            ProductNameExistsValidator productNameExistsValidator,
            TemporalValidator temporalValidator
        )
        {
            Include(productNameExistsValidator);
            Include(temporalValidator);
        }
    }
}
