using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductMustNotExistValidator : AbstractValidator<IProductNameArgs>
    {
        public ProductMustNotExistValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.ProductName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(x => !productRepository.Exists(x))
                .WithMessage("'{PropertyName}' \"{PropertyValue}\" already exists");
        }
    }
}
