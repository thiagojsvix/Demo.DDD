using System.Collections.Generic;
using System.Linq;
using Demo.DDD.Domain.ValueObjects;
using FluentValidation;

namespace Demo.DDD.Domain.Entitys
{
    public class Fornecedor : Pessoa
    {
        public Fornecedor(string nome, long codigo, Documento documento, Email email, string inscricaoEstadual, string nomeFantasia) : base(nome, codigo, documento, email)
        {
            this.InscricaoEstadual = inscricaoEstadual;
            this.NomeFantasia = nomeFantasia;
            this.Validate(this, new PessoaJuridicaValidator());
        }

        public string InscricaoEstadual { get; }
        public string NomeFantasia { get; }

        public override void Ativar()
        {
            //Situação só pode Ativo, caso a Pessoa esteja valida
            this.Situacao = this.Valid ? Enums.Situacao.Ativo : Enums.Situacao.Inativo;
        }
    }

    public class PessoaJuridicaValidator : AbstractValidator<Fornecedor>
    {
        public PessoaJuridicaValidator()
        {
            this.RuleFor(x => x.Endereco)
                .Must(this.ValidarEndereco)
                .WithMessage("Deve existir ao menos um endereço do tipo Comercial");
        }

        private bool ValidarEndereco(IReadOnlyCollection<Endereco> enderecos) => enderecos.Any(x => x.TipoEndereco == Enums.TipoEndereco.Comercial);
    }
}
