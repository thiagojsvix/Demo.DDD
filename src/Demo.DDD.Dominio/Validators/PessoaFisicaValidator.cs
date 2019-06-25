using System;
using Demo.DDD.Domain.Entitys;
using FluentValidation;
using FluentValidation.Results;

namespace Demo.DDD.Domain.Validators
{

    public class PessoaFisicaValidator : AbstractValidator<Cliente>
    {
        public PessoaFisicaValidator()
        {
            this.RuleFor(x => x.DataNascimento)
                .Custom((birthdate, act) =>
                {
                    var today = DateTime.Today;
                    var age = today.Year - birthdate.Year;
                    if (birthdate.Date > today.AddYears(-age)) age--;
                    if (age < 18) act.AddFailure(new ValidationFailure("DataNascimento", "Pessoa deve ser maior de 18 anos") { ErrorCode = "PessoaMenor18Anos" });
                });
        }
    }
}
