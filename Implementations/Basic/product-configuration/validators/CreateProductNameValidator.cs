using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateProductNameValidator : AbstractValidator<IUpsertProductArgs>
    {
        public CreateProductNameValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithName("Product Name")
                .Must(x => !productRepository.Exists(x)).WithMessage("'{PropertyName}' \"{PropertyValue}\" already exists");
        }
    }
}
