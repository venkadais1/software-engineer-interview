using FluentValidation;

namespace Zip.Installments.Validations.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public BaseValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(model => model)
                .NotNull();
        }
    }
}
