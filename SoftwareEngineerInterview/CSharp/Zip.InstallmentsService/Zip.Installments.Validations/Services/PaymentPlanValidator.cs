using FluentValidation;
using Zip.Installments.Infrastructure.Models;
using Zip.Installments.Validations.Base;

namespace Zip.Installments.Validations.Services
{
    public class PaymentPlanValidator : BaseValidator<PaymentPlan>
    {
        public PaymentPlanValidator()
        {
            RuleFor(n => n.Installments)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid should not be null or empty");

            RuleForEach(n => n.Installments)
                .SetValidator(new InstallmentValidator());

            RuleFor(n => n)
                .Must(x => x.PurchaseAmount.Equals(x.Installments.Sum(x => x.Amount)))
                .WithMessage("Installment amount should equal with number of installments");
        }
    }
}
