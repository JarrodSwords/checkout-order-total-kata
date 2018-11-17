using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateSpecialArgsValidator : AbstractValidator<CreateSpecialArgs>
    {
        private readonly IProductRepository _productRepository;

        public CreateSpecialArgsValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist");

            RuleFor(x => x.StartTime).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Special start time is required")
                .LessThan(x => x.EndTime).When(x => x.EndTime != null).WithMessage("Special start time must be less than end time");

            RuleFor(x => x.EndTime).NotNull().WithMessage("Special end time is required");
        }
    }
}
