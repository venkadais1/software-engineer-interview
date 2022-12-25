using FluentValidation;
using Zip.Installments.Core.Models;
using Zip.Installments.Validations.Base;

namespace Zip.Installments.Validations.Services
{
    /// <summary>
    ///     To set fluent validation for payment plan
    /// </summary>
    public class PaymentPlanValidator : BaseValidator<PaymentPlan>
    {
        /// <summary>
        ///     Set fluent validation rules for payment plan
        /// </summary>
        public PaymentPlanValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
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
