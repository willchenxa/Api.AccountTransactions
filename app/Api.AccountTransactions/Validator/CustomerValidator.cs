namespace Api.AccountTransactions.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {

        public CustomerValidator()
        {
            RuleFor(customer => customer.id).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError).MinimumLength(36).WithMessage(ValidatorErrorMessages.GetLengthValidatorErrorForCustomer);
            RuleFor(customer => customer.name).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
        }
    }
}
