global using static Api.AccountTransactions.Constant;

namespace Api.AccountTransactions.Validator
{
 public class TransactionValidator : AbstractValidator<Transaction>
 {
  public TransactionValidator(IValidator<Customer> customerValidator)
  {
   RuleFor(transcation => transcation.id).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError).MinimumLength(36).WithMessage(ValidatorErrorMessages.GetLengthValidatorErrorForTransaction);
   RuleFor(transcation => transcation.fromAccount).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
   RuleFor(transcation => transcation.toAccount).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
   RuleFor(transcation => transcation.description).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
   RuleFor(transcation => transcation.amount).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
   RuleFor(transcation => transcation.date).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
   RuleFor(transcation => transcation.owner).NotEmpty().WithMessage(ValidatorErrorMessages.GetValidatorError);
   RuleFor(transcation => transcation.owner).SetValidator(customerValidator);
  }
 }
}
