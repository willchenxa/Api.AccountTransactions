using Api.AccountTransactions.Dtos;
using FluentValidation;

namespace Api.AccountTransactions.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.id).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(customer => customer.name).NotEmpty().WithMessage(GetValidatorError());
        }
        private string GetValidatorError()
        {
            return "'{PropertyName}' must not be empty for Customer.";
        }

    }


}
