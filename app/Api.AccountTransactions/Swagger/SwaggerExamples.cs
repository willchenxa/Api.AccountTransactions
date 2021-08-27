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
    }
}
