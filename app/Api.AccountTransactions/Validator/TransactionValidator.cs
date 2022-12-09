namespace Api.AccountTransactions.Validator
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator(IValidator<Customer> customerValidator)
        {
            RuleFor(transcation => transcation.id).NotEmpty().WithMessage(GetValidatorError()).MinimumLength(36).WithMessage(GetLengthValidatorError());
            RuleFor(transcation => transcation.fromAccount).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(transcation => transcation.toAccount).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(transcation => transcation.description).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(transcation => transcation.amount).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(transcation => transcation.date).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(transcation => transcation.owner).NotEmpty().WithMessage(GetValidatorError());
            RuleFor(transcation => transcation.owner).SetValidator(customerValidator);
        }

        private string GetValidatorError()
        {
            return "'{PropertyName}' is required";
        }

        private string GetLengthValidatorError()
        {
            return "'{PropertyName}' must not less than 36 characters for Transaction";
        }
    }
}
