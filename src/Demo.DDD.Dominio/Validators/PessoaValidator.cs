using Demo.DDD.Domain.Entitys;
using FluentValidation;

namespace Demo.DDD.Domain.Validators
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            this.RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome não poder ser vazio");

            this.RuleFor(x => x.Documento)
                .NotNull()
                .WithMessage("Documento não pode ser nulo");

            this.RuleFor(x => x.Endereco)
                .NotNull()
                .WithMessage("Endereço não pode ser nulo");
        }
    }
}
