using FluentValidation;
using System.Net;
using Zip.Installments.Core.Constants;
using Zip.Installments.Validations.Base;
using Zip.Installments.ViewModel.Orders;

namespace Zip.Installments.Validations.Controllers
{
    public class OrdersViewModelValidator : BaseValidator<OrdersViewModel>
    {
        public OrdersViewModelValidator()
        {
            //this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                //.WithState(x => new InvalidOperationException(ErrorMessage.InvalidProperty));
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
