
using BookProviders.Business.Models;
using BookProviders.Business.Validations.Documents;
using FluentValidation;

namespace BookProviders.Business.Validations
{
    public class CatererValidation : AbstractValidator<Caterer>
    {
        public CatererValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Field {propertyName} required")
                .Length(2, 100);

            When(c => c.CatererType == CatererType.Person, () => {
                RuleFor(c => c.Document.Length).Equal(ValidateCPF.cpfSize)
                .WithMessage("The field must be {ComparisonValue} characters and has {PropertyValue}");

                RuleFor(c => ValidateCPF.Validate(c.Document)).Equal(true)
                .WithMessage("Invalid CPF");
            });

            When(c => c.CatererType == CatererType.Company, () => {
                RuleFor(c => c.Document.Length).Equal(ValidateCNPJ.cnpjSize)
                    .WithMessage("The field must be {ComparisonValue} characters and has {PropertyValue}");

                RuleFor(c => ValidateCNPJ.Validate(c.Document)).Equal(true)
                .WithMessage("Invalid CNPJ");
            });
        }
    }
}
