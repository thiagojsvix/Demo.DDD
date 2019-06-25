using System;
using Demo.DDD.Shared.Commands;
using FluentValidation;

namespace Demo.DDD.Domain.Commands
{
    public class CriarClienteCommand : Command
    {
        public CriarClienteCommand() { }

        public CriarClienteCommand(string nome, string email, string documento, Enums.TipoDocumento tipoDocumento, Enums.Sexo sexo, DateTime dataNascimento, string rua, string bairro, string numero, string cidade, Enums.TipoEndereco tipoEndereco)
        {
            this.Nome = nome;
            this.Email = email;
            this.documento = documento;
            this.tipoDocumento = tipoDocumento;
            this.Sexo = sexo;
            this.DataNascimento = dataNascimento;
            this.Rua = rua;
            this.Bairro = bairro;
            this.Numero = numero;
            this.Cidade = cidade;
            this.TipoEndereco = tipoEndereco;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string documento { get; set; }
        public Enums.TipoDocumento tipoDocumento { get; set; }

        public Enums.Sexo Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public Enums.TipoEndereco TipoEndereco { get; set; }
    }

    public class CriarClienteCommandValidator : AbstractValidator<CriarClienteCommand>
    {
        public CriarClienteCommandValidator()
        {
            this.RuleFor(x => x.Email).EmailAddress();
        }
    }
}
