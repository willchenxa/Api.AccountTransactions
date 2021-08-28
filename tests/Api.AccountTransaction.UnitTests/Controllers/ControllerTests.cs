using Api.AccountTransaction.UnitTests.MockData;
using Api.AccountTransactions.Controllers;
using Api.AccountTransactions.Dtos;
using Api.AccountTransactions.Exception;
using Api.AccountTransactions.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.AccountTransaction.UnitTests
{
    public class ControllerTests
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<AccountTransactionsController> _logger;
        private readonly AccountTransactionsController _accountTransactionsController;

        public ControllerTests()
        {
            _transactionService = Substitute.For<ITransactionService>();
            _logger = Substitute.For<ILogger<AccountTransactionsController>>();
            _accountTransactionsController = new AccountTransactionsController(_transactionService, _logger);
        }

        [Fact]
        public async Task should_return_all_transactions()
        {
            //arrange
            _transactionService.GetAccountTransactions()
                .ReturnsForAnyArgs(MockData.TransactionMockData.SuccessTransactions);

            // Act
            var response = await _accountTransactionsController.GetTransactions();

            // Assert
            var okResult = (OkObjectResult)response.Result;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task should_return_created_response_when_post_transaction()
        {
            //arrange
            _transactionService.CreateTransaction(TransactionMockData.NewTransactionRequest)
                .ReturnsForAnyArgs(new TransactionResponse { Message = TransactionMockData.CreateResponse });

            // Act
            var response = await _accountTransactionsController.CreateTransaction(TransactionMockData.NewTransactionRequest);

            // Assert
            var okResult = (OkObjectResult)response.Result;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            ((TransactionResponse)okResult.Value).Message.Should().Be("Transaction created");
        }

        [Fact]
        public async Task should_return_updated_response_when_update_transaction()
        {
            //arrange
            _transactionService.UpdateTransaction(TransactionMockData.ExistingTransactionRequest, TransactionMockData.ExistingTransactionId)
                .ReturnsForAnyArgs(new TransactionResponse { Message = TransactionMockData.UpdatedResponse });

            // Act
            var response = await _accountTransactionsController.UpdateTransaction(TransactionMockData.ExistingTransactionRequest,
                TransactionMockData.ExistingTransactionId);

            // Assert
            var okResult = (OkObjectResult)response.Result;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            ((TransactionResponse)okResult.Value).Message.Should().Be("Transaction updated");
        }

        [Fact]
        public async Task should_return_badrequest_response_when_transaction_already_exists()
        {
            //arrange
            _transactionService.CreateTransaction(TransactionMockData.ExistingTransactionRequest)
                .Throws(new HttpResponseException
                {
                    Status = 400,
                    Value = TransactionMockData.TransactionExists
                });

            // Act
            Func<Task> act = async () => await _accountTransactionsController.CreateTransaction(TransactionMockData.ExistingTransactionRequest);

            // Assert
            await act.Should().ThrowAsync<HttpResponseException>();
        }

        [Fact]
        public async Task should_return_badrequest_response_when_transaction_doesnot_exist()
        {
            //arrange
            _transactionService.UpdateTransaction(TransactionMockData.NewTransactionRequest, TransactionMockData.ExistingTransactionId)
                .Throws(new HttpResponseException
                {
                    Status = 400,
                    Value = TransactionMockData.TransactionDoesNotExists
                });

            // Act
            Func<Task> act = async () => await _accountTransactionsController.UpdateTransaction(TransactionMockData.NewTransactionRequest,
                TransactionMockData.ExistingTransactionId);

            // Assert
            await act.Should().ThrowAsync<HttpResponseException>();
        }
    }
}
