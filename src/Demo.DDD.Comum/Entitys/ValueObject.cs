using System.Diagnostics.CodeAnalysis;
using Demo.DDD.Shared.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Demo.DDD.Shared.Entitys
{
    [ExcludeFromCodeCoverage]
    public abstract class ValueObject : IValidation
    {
        public bool Valid { get; private set; }
        public bool Invalid => !this.Valid;
        public ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            this.ValidationResult = validator.Validate(model);
            return this.Valid = this.ValidationResult.IsValid;
        }
    }
}
