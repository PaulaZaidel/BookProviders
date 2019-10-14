
using BookProviders.Business.Models;
using FluentValidation;

namespace BookProviders.Business.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(2, 200);

            RuleFor(p => p.Description)
                .NotEmpty()
                .Length(2, 1000);

            RuleFor(p => p.Price)
                .GreaterThan(0);
        }
    }
}
