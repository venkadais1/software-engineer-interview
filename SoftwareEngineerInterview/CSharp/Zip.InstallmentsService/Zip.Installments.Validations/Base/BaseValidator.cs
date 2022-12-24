using FluentValidation;
using FluentValidation.Results;
using System.Security.Cryptography.X509Certificates;

namespace Zip.Installments.Validations.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public BaseValidator()
        {
            this.ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(model => model)
                .NotNull();
        }
    }
}
