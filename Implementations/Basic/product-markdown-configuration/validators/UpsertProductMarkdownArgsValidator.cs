using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class UpsertProductMarkdownArgsValidator : AbstractValidator<UpsertProductMarkdownArgs>
    {
        private readonly IProductRepository _productRepository;

        public UpsertProductMarkdownArgsValidator(
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
                .NotEmpty().WithMessage("Markdown product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .DependentRules(() =>
                {
                    RuleFor(x => x.AmountOffRetail).Cascade(CascadeMode.StopOnFirstFailure)
                        .NotNull().WithMessage("Markdown amount off retail is required")
                        .GreaterThan(0).WithMessage("Markdown amount off retail must be greater than zero")
                        .Must((args, x) =>
                        {
                            var product = _productRepository.FindProduct(args.ProductName);

                            return product.GetType() == typeof(EachesProduct) ?
                                x.Value <= product.RetailPrice :
                                x.Value <= product.RetailPricePerUnit;
                        })
                        .WithMessage("Markdown amount off retail must be less than or equal to product retail price");
                });
        }
    }
}
