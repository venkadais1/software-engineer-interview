using FluentValidation;
using Zip.Installments.Core.Models;
using Zip.Installments.Validations.Base;

namespace Zip.Installments.Validations.Services
{
    /// <summary>
    ///     To set fluent validation for payment installments
    /// </summary>
    public class InstallmentValidator : BaseValidator<Installment>
    {
        /// <summary>
        ///     Set fluent validation rules for payment installments
        /// </summary>
        public InstallmentValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Invalid installment frequency amount");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now.Date)
                .WithMessage("Invalid installment frequency date");
        }
    }
}
