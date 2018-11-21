using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Domain
{
    public class OrderMustExistValidator : AbstractValidator<IOrderIdArgs>
    {
        public OrderMustExistValidator(IOrderRepository orderRepository)
        {
            RuleFor(x => x.OrderId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(x => orderRepository.Exists(x.Value))
                .WithMessage("'{PropertyName}' \"{PropertyValue}\" does not exist");;
        }
    }
}
