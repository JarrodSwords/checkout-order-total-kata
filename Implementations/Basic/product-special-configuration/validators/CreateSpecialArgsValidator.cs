using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateSpecialArgsValidator : AbstractValidator<CreateSpecialArgs>
    {
        private readonly IProductRepository _productRepository;

        public CreateSpecialArgsValidator(
            IProductRepository productRepository,
            TemporalValidator temporalValidator
        )
        {
            _productRepository = productRepository;
            CreateRules();
            Include(temporalValidator);
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist");
        }
    }
}
