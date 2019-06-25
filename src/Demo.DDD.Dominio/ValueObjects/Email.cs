using Demo.DDD.Shared.Entitys;
using FluentValidation;

namespace Demo.DDD.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string value)
        {
            this.Endereco = value;
            this.Validate(this, new EmailValidator());
        }

        public string Endereco { get; }
    }

    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            this.RuleFor(x => x.Endereco)
                .EmailAddress()
                .WithMessage("E-mail Informado é inválido");
        }
    }
}
