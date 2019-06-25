using Demo.DDD.Shared.Entitys;
using FluentValidation;

namespace Demo.DDD.Domain.ValueObjects
{
    public class Endereco : ValueObject
    {
        public Endereco(string rua, string bairro, string numero, string cidade, Enums.TipoEndereco tipoEndereco)
        {
            this.Rua = rua;
            this.Bairro = bairro;
            this.Numero = numero;
            this.Cidade = cidade;
            this.TipoEndereco = tipoEndereco;

            this.Validate(this, new EnderecoValidator());
        }

        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public Enums.TipoEndereco TipoEndereco { get; private set; }

    }

    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            this.RuleFor(x => x.Rua).NotEmpty();
            this.RuleFor(x => x.Bairro).NotEmpty();
            this.RuleFor(x => x.Cidade).NotEmpty();
        }
    }
}
