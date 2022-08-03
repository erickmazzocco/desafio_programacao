using DesafioProgramacao.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Service.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description can not be empty")
                .NotNull().WithMessage("Description can not be empty");

            RuleFor(c => c.ProviderId)
                .NotNull().WithMessage("ProviderId can not be null")
                .GreaterThan(0).WithMessage("ProviderId can not be less then 0");                

            RuleFor(c => c.ManufacturingDate)
                .NotEmpty().WithMessage("Manufacturing Date can not be null")
                .NotNull().WithMessage("Manufacturing Date can not be null")
                .LessThan(c => c.ValidationDate)
                .WithMessage("Manufacturing Date can not be greater then Validation Date");

            RuleFor(c => c.ValidationDate)
                .NotEmpty().WithMessage("Validation Date can not be null")
                .NotNull().WithMessage("Validation Date can not be null");
        }
    }
}
