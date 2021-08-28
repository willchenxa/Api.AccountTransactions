using Api.AccountTransactions.Dtos;
using Api.AccountTransactions.Validator;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Api.AccountTransaction.UnitTests.Validators
{
    public class ValidatorTests
    {
        private readonly IValidator<Transaction> _transactionValidator;
        private readonly IValidator<Customer> _customerValidator;

        public ValidatorTests()
        {
            _customerValidator = new CustomerValidator();
            _transactionValidator = new TransactionValidator(_customerValidator);
        }

        [Theory]
        [MemberData(nameof(ValidTransactionModel))]
        public void Validate_For_Valid_Transaction(Transaction transaction)
        {
            // Arrange
            var result = _transactionValidator.Validate(transaction);

            // Assert
            using (new AssertionScope())
            {
                result.Errors.Count.Should().Be(0);
                result.IsValid.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(InvalidTransactionModel))]
        public void Validate_For_Invalid_Transaction(Transaction transaction, string errorMessage)
        {
            // Arrange
            var result = _transactionValidator.Validate(transaction);

            // Assert
            using (new AssertionScope())
            {
                result.IsValid.Should().BeFalse();
                result.Errors.Count.Should().Be(1);
                result.Errors.First().ErrorMessage.Should().BeEquivalentTo(errorMessage);
            }
        }

        public static IEnumerable<object[]> ValidTransactionModel =>
            new List<object[]>
            {
                new object[]
                {
                    new Transaction
                    {
                        id = Guid.NewGuid().ToString(),
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        amount = 123456.78,
                        date = DateTime.UtcNow.ToString(),
                        owner = new Customer
                        {
                            id = "78cf59a3-3e43-4897-9bad-bfdf30b41e84",
                            name = "John Smith"
                        }
                    }
                },
                new object[]
                {
                    new Transaction
                    {
                        id = Guid.NewGuid().ToString(),
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        amount = 123456.78,
                        date = DateTime.UtcNow.ToString(),
                        owner = new Customer
                        {
                            id = Guid.NewGuid().ToString(),
                            name = "John Smith"
                        }
                    }
                }
            };

        public static IEnumerable<object[]> InvalidTransactionModel =>
            new List<object[]>
            {
                new object[]
                {
                    new Transaction()
                    {
                        id = Guid.NewGuid().ToString(),
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        amount = 123456.78,
                        date = DateTime.UtcNow.ToString()
                    },
                    "'owner' is required"
                },
                new object[]
                {
                    new Transaction()
                    {
                        id = Guid.NewGuid().ToString(),
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        amount = 123456.78,
                        date = DateTime.UtcNow.ToString(),
                        owner = new Customer
                        {
                            id = "6976fe63-c665-445b",
                            name = "John Smith"
                        }
                    },
                    "'id' must not be less than 36 characters for Customer"
                },

                new object[]
                {
                    new Transaction()
                    {
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        amount = 123456.78,
                        date = DateTime.UtcNow.ToString(),
                        owner = new Customer
                        {
                            id = Guid.NewGuid().ToString(),
                            name = "John Smith"
                        }
                    },
                    "'id' is required"
                },
                new object[]
                {
                    new Transaction()
                    {
                        id = Guid.NewGuid().ToString(),
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        date = DateTime.UtcNow.ToString(),
                        owner = new Customer
                        {
                            id = Guid.NewGuid().ToString(),
                            name = "John Smith"
                        }
                    },
                    "'amount' is required"
                },
                new object[]
                {
                    new Transaction()
                    {
                        id = "6976fe63c665445b835c42dabe9fa3b7",
                        fromAccount = "123-456",
                        toAccount = "789-123",
                        description = "Example transaction",
                        amount = 123456.78,
                        date = DateTime.UtcNow.ToString(),
                        owner = new Customer
                        {
                            id = Guid.NewGuid().ToString(),
                            name = "John Smith"
                        }
                    },
                    "'id' must not less than 36 characters for Transaction"
                }
            };
    }
}
