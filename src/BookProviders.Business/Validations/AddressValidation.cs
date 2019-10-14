
using BookProviders.Business.Models;
using FluentValidation;

namespace BookProviders.Business.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("Required")
                .Length(2, 400);

            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("Required")
                .Length(8);

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("Required")
                .Length(2, 50);

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("Required")
                .Length(2, 100);

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("Required")
                .Length(2, 50);
        }
    }
}
