using System.Collections.Generic;
using System.Linq;
using Demo.DDD.Domain.Entitys;
using Demo.DDD.Domain.ValueObjects;
using FluentValidation;

namespace Demo.DDD.Domain.Validators
{
    public class PessoaAdicionarEnderecoValidator : AbstractValidator<Pessoa>
    {
        public PessoaAdicionarEnderecoValidator()
        {
            this.Include(new PessoaValidator());

            this.RuleFor(x => x.Endereco)
                .Must(this.ValidarEnderecoEntrega)
                .WithMessage("Não é possivel ter mais de um Endereço de Entrega")
                .WithErrorCode("EnderecoEntregaDuplicado")
                ;

            this.RuleFor(x => x.Endereco)
                .Must(this.ValidarEnderecoComercial)
                .WithMessage("Não é possivel ter mais de um Endereço de Comercial")
                .WithErrorCode("EnderecoComercialDuplicado")
                ;

            this.RuleFor(x => x.Endereco)
                .Must(this.ValidarEnderecoResidencial)
                .WithMessage("Não é possivel ter mais de um Endereço de Residencial")
                .WithErrorCode("EnderecoResidenciaDuplicado")
                ;
        }

        private bool ValidarEnderecoEntrega(IReadOnlyCollection<Endereco> enderecos) => enderecos.Count(x => x.TipoEndereco == Enums.TipoEndereco.Entrega) <= 1;
        private bool ValidarEnderecoComercial(IReadOnlyCollection<Endereco> enderecos) => enderecos.Count(x => x.TipoEndereco == Enums.TipoEndereco.Comercial) <= 1;
        private bool ValidarEnderecoResidencial(IReadOnlyCollection<Endereco> enderecos) => enderecos.Count(x => x.TipoEndereco == Enums.TipoEndereco.Residencial) <= 1;
    }
}
