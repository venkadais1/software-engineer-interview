using FluentValidation;
using Zip.Installments.Validations.Base;
using Zip.Installments.ViewModel.Orders;

namespace Zip.Installments.Validations.Controllers
{
    public class OrdersViewModelValidator : BaseValidator<OrdersViewModel>
    {
        public OrdersViewModelValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid Email");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(255)
                .WithMessage("Invalid First Name");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MinimumLength(1)
                .MaximumLength(255)
                .WithMessage("Invalid Last Name");

            RuleFor(x => x.NumberOfInstallments)
                .GreaterThan(1)
                .WithMessage("Invalid Installments");

            RuleFor(x => x.PurchaseAmount)
                .GreaterThan(1M)
                .WithMessage("Invalid purchase amount");

            RuleFor(x => x.FirstPaymentDate)
                .GreaterThan(DateTime.Now.Date)
                .WithMessage("Invalid payment date");

            RuleFor(x => x.Frequency)
                .GreaterThan(1)
                .WithMessage("Payment frequency should more than one day");


        }
    }
}
