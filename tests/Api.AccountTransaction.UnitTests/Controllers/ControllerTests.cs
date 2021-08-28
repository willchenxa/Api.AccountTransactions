using Api.AccountTransaction.UnitTests.MockData;
using Api.AccountTransactions.Controllers;
using Api.AccountTransactions.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            _transactionService.CreateTransaction(TransactionMockData.CreateOrUpdateTransactionRequest)
                .ReturnsForAnyArgs(TransactionMockData.CreateResponse);

            // Act
            var response = await _accountTransactionsController.CreateTransaction(TransactionMockData.CreateOrUpdateTransactionRequest);

            // Assert
            var okResult = (OkObjectResult)response.Result;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be("Transaction created");
        }

        [Fact]
        public async Task should_return_updated_response_when_update_transaction()
        {
            //arrange
            _transactionService.UpdateTransaction(TransactionMockData.CreateOrUpdateTransactionRequest, TransactionMockData.RequestTransactionId)
                .ReturnsForAnyArgs(TransactionMockData.UpdatedResponse);

            // Act
            var response = await _accountTransactionsController.UpdateTransaction(TransactionMockData.CreateOrUpdateTransactionRequest,
                "6976fe63-c665-445b-835c-42dabe9fa3b7");

            // Assert
            var okResult = (OkObjectResult)response.Result;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be("Transaction updated");
        }
    }
}
