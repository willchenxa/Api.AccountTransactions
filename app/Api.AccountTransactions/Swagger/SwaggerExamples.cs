using Api.AccountTransactions.Dtos;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Transaction = Api.AccountTransactions.Dtos.Transaction;

namespace Api.AccountTransactions.Swagger
{
    public class SwaggerExamples
    {
        public class ReturnTransactionsExample : IExamplesProvider<List<Transaction>>
        {
            public List<Transaction> GetExamples()
            {
                return new List<Transaction>
                {
                    new Transaction
                        {
                            id = "6976fe63-c665-445b-835c-42dabe9fa3b7",
                            fromAccount = "123456",
                            toAccount = "789123",
                            description = "First transaction description",
                            amount = 123456.78,
                            date = "2016-08-29T09:12:33.001Z",
                            owner = new Customer
                            {
                                id = "78cf59a3-3e43-4897-9bad-bfdf30b41e84",
                                name = "John Smith"
                            }

                        },
                    new Transaction
                        {
                            id = "05c17cad-9279-423c-b32b-cdb9e0e26d36",
                            fromAccount = "135790",
                            toAccount = "789123",
                            description = "Second transaction description",
                            amount = 123456.78,
                            date = "2016-08-29T09:12:33.001Z",
                            owner = new Customer
                            {
                                id = "3eb148e5-14de-4177-a3c5-e357be474712",
                                name = "Jane Citizen"
                            }

                        },
                    new Transaction
                        {
                            id = "6976fe63-c665-445b-835c-42dabe9fa3b7",
                            fromAccount = "123456",
                            toAccount = "789123",
                            description = "Third transaction description",
                            amount = 98765,
                            date = "2021-08-28T02:22:35.3041524Z",
                            owner = new Customer
                            {
                                id = "be9c4f94-0993-4b8f-a9bb-bf3b2ded22bc",
                                name = "Customer 1"
                            }
                        }
                };
            }
        }

        public class TransactionRequestExample : IExamplesProvider<Transaction>
        {
            public Transaction GetExamples()
            {
                return new Transaction
                {
                    id = "6976fe63-c665-445b-835c-42dabe9fa3b7",
                    fromAccount = "123-456",
                    toAccount = "789-123",
                    description = "Example transaction",
                    amount = 123456.78,
                    date = "2016-08-29T09:12:33.001Z",
                    owner = new Customer
                    {
                        id = "78cf59a3-3e43-4897-9bad-bfdf30b41e84",
                        name = "John Smith"
                    }

                };
            }
        }

        public class CreatedTransactionExample : IExamplesProvider<string>
        {
            public string GetExamples()
            {
                return Constant.TransactionResponse.CreatedSuccessful;
            }
        }

        public class UpdateTransactionExample : IExamplesProvider<string>
        {
            public string GetExamples()
            {
                return Constant.TransactionResponse.UpdatedSucessful;
            }
        }
    }
}
