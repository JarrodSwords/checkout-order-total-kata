using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class UpsertProductMarkdownArgsValidator : AbstractValidator<UpsertProductMarkdownArgs>
    {
        private readonly IProductRepository _productRepository;

        public UpsertProductMarkdownArgsValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Markdown product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .DependentRules(() =>
                {
                    RuleFor(x => x.AmountOffRetail)
                        .NotNull().WithMessage("Markdown amount off retail is required")
                        .GreaterThan(0).WithMessage("Markdown amount off retail must be greater than zero")
                        .LessThanOrEqualTo(x => _productRepository.FindProduct(x.ProductName).RetailPrice.Amount)
                        .WithMessage("Markdown amount off retail must be less than or equal to product retail price");
                });

            RuleFor(x => x.StartTime).NotNull().WithMessage("Markdown start time is required")
                .LessThan(x => x.EndTime).When(x => x.EndTime != null).WithMessage("Markdown start time must be less than end time");

            RuleFor(x => x.EndTime).NotNull().WithMessage("Markdown end time is required");
        }
    }
}
