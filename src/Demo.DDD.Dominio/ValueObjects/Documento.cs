using Demo.DDD.Shared.Entitys;
using Exiges.Core.Tools;
using FluentValidation;

namespace Demo.DDD.Domain.ValueObjects
{
    public class Documento : ValueObject
    {
        public Documento(string numero, Enums.TipoDocumento tipoDocumento)
        {
            this.Numero = numero;
            this.TipoDocumento = tipoDocumento;

            this.Validate(this, new DocumentoValidator());
        }

        public string Numero { get; }
        public Enums.TipoDocumento TipoDocumento { get; }
    }

    public class DocumentoValidator : AbstractValidator<Documento>
    {
        public DocumentoValidator()
        {
            this.RuleFor(x => x.Numero)
                .Must( this.ValidarCPF)
                .WithMessage("CPF inválido")
                .When(x => x.TipoDocumento == Enums.TipoDocumento.Cpf);

            this.RuleFor(x => x.Numero)
                .Must(this.ValidarCNPJ)
                .WithMessage("CNPJ inválido")
                .When(x => x.TipoDocumento == Enums.TipoDocumento.Cnpj);
        }

        private bool ValidarCPF(string value) => ExigesTools.ValidarCPF(value);
        private bool ValidarCNPJ(string value) => ExigesTools.ValidarCNPJ(value);
    }
}
