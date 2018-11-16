using System.Linq;
using FluentValidation;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class RemoveScannedItemArgsValidator : AbstractValidator<RemoveScannedItemArgs>
    {
        private IOrderRepository _orderRepository;

        public RemoveScannedItemArgsValidator(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ScannedItemId).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Scanned item id is required")
                .Must((args, x) => _orderRepository.FindOrder(args.OrderId.Value).ScannedItems.Select(y => y.Id).Contains(x.Value))
                .WithMessage("Scanned item id \"{PropertyValue}\" does not exist");
        }
    }
}