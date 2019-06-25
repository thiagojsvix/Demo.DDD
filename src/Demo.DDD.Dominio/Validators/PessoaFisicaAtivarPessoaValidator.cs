using System.Collections.Generic;
using System.Linq;
using Demo.DDD.Domain.Entitys;
using Demo.DDD.Domain.ValueObjects;
using FluentValidation;

namespace Demo.DDD.Domain.Validators
{
    public class PessoaFisicaAtivarPessoaValidator : AbstractValidator<Cliente>
    {
        public PessoaFisicaAtivarPessoaValidator()
        {
            this.Include(new PessoaFisicaValidator());
            this.RuleFor(x => x.Endereco)
            .NotEmpty()
            .WithMessage("Para ativar a Pessoa Fisica é necessário ao menos um Endereço do Tipo Residencial")
            ;

            this.RuleFor(x => x.Endereco)
                .Must(this.EnderecoResidencialObrigatorio)
                .WithMessage("Para ativar a Pessoa Fisica é necessário ao menos um Endereço do Tipo Residencial")
                .WithErrorCode("PessoaFisicaEnderecoResidencial")
                .When(x => x.Endereco.Count > 0)
                ;
        }

        private bool EnderecoResidencialObrigatorio(IReadOnlyCollection<Endereco> enderecos) =>
            enderecos.Any(y => y.TipoEndereco == Enums.TipoEndereco.Residencial);
    }
}
