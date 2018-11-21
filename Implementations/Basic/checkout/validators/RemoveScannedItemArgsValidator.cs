using System.Linq;
using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class RemoveScannedItemArgsValidator : AbstractValidator<RemoveScannedItemArgs>
    {
        public RemoveScannedItemArgsValidator(
            IOrderRepository orderRepository,
            OrderMustExistValidator orderMustExistValidator
        )
        {
            Include(orderMustExistValidator);

            RuleFor(x => x.ScannedItemId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must((args, x) =>
                    orderRepository.FindOrder(args.OrderId.Value).ScannedItems.Select(y => y.Id).Contains(x.Value)
                )
                .WithMessage("{PropertyName} \"{PropertyValue}\" does not exist");
        }
    }
}
