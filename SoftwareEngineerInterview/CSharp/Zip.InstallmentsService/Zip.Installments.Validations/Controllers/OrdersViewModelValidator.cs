using FluentValidation;
using Zip.Installments.Core.Constants;
using Zip.Installments.Validations.Base;
using Zip.Installments.ViewModel.Orders;

namespace Zip.Installments.Validations.Controllers
{
    /// <summary>
    ///     To set fluent validation for order view model
    /// </summary>
    public class OrdersViewModelValidator : BaseValidator<OrdersViewModel>, IValidator<OrdersViewModel>
    {
        /// <summary>
        ///     Set fluent validation rules for order view model
        /// </summary>
        public OrdersViewModelValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage(ErrorMessage.InvalidProperty);

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(255)
                .WithMessage(ErrorMessage.InvalidPropertyLength);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MinimumLength(1)
                .MaximumLength(255)
                .WithMessage(ErrorMessage.InvalidPropertyLength);

            RuleFor(x => x.NumberOfInstallments)
                .GreaterThan(1)
                .WithMessage(ErrorMessage.InvalidProperty);

            RuleFor(x => x.PurchaseAmount)
                .GreaterThan(1M)
                .WithMessage(ErrorMessage.InvalidProperty);

            RuleFor(x => x.FirstPaymentDate)
                .GreaterThan(DateTime.Now.Date)
                .WithMessage(ErrorMessage.InvalidProperty);

            RuleFor(x => x.Frequency)
                .GreaterThan(1)
                .WithMessage("Payment frequency should more than one day");
        }
    }
}
