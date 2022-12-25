using FluentValidation;

namespace Zip.Installments.Validations.Base
{
    /// <summary>
    ///     The fluent validation base class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        /// <summary>
        ///     set base role for all fluent validation
        /// </summary>
        public BaseValidator()
        {
            this.ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(model => model)
                .NotNull();
        }
    }
}
