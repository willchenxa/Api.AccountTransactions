namespace Api.AccountTransactions
{
 public static class Constant
 {
  public static class TransactionResponseMessages
  {
   public static readonly string CreatedSuccessful = "Transaction created";
   public static readonly string TransactionExists = "Transaction already exists";
   public static readonly string UpdatedSucessful = "Transaction updated";
   public static readonly string TransactionDoesNotExists = "Transaction does not exist";
  }

  public static class ValidatorErrorMessages
  {
   public const string GetValidatorError = "'{PropertyName}' is required";
   public const string GetLengthValidatorErrorForCustomer = "'{PropertyName}' must not be less than 36 characters for Customer";
   public const string GetLengthValidatorErrorForTransaction = "'{PropertyName}' must not be less than 36 characters for Transaction";

        }
 }
}
