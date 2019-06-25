using FluentValidation;
using FluentValidation.Results;

namespace Demo.DDD.Shared.Validations
{
    public interface IValidation
    {
        bool Valid { get; }
        bool Invalid { get; }
        ValidationResult ValidationResult { get; }
        bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
    }

    public abstract class Validation: IValidation{
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