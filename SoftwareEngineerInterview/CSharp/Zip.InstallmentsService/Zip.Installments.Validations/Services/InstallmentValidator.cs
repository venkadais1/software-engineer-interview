﻿using FluentValidation;
using Zip.Installments.Infrastructure.Models;
using Zip.Installments.Validations.Base;

namespace Zip.Installments.Validations.Services
{
    public class InstallmentValidator : BaseValidator<Installment>
    {
        public InstallmentValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Invalid installment frequency amount");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now.Date)
                .WithMessage("Invalid installment frequency date");
        }
    }
}