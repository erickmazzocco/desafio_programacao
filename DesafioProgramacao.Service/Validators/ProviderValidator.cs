using DesafioProgramacao.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioProgramacao.Service.Validators
{
    public class ProviderValidator : AbstractValidator<Provider>
    {
        public ProviderValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description can not be empty")
                .NotNull().WithMessage("Description can not be empty");

            RuleFor(c => c.Cnpj)
                .NotEmpty().WithMessage("Cnpj can not be empty")
                .NotNull().WithMessage("Cnpj can not be empty");
        }
    }
}
