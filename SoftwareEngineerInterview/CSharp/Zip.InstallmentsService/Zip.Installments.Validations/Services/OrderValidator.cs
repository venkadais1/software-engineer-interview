using FluentValidation;
using Zip.Installments.Core.Models;
using Zip.Installments.Validations.Base;

namespace Zip.Installments.Validations.Services
{
    public class OrderValidator : BaseValidator<Order>
    {
        public OrderValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.NumberOfInstallments)
                .GreaterThan(1)
                .WithMessage("Invalid Installments");

            RuleFor(x => x.Payment.PurchaseAmount)
               .GreaterThan(1M)
               .WithMessage("Invalid purchase amount");

            RuleFor(x => x)
                .Must(x => x.NumberOfInstallments.Equals(x.Payment.Installments.Count))
                .WithMessage("Installments not equal with installment input");

            RuleFor(x => x.Payment)
                .SetValidator(new PaymentPlanValidator());
        }
    }
}
